using Application.Interfaces;
using Domain.FotoAnuncio.Contracts;
using Microsoft.AspNetCore.Http;
using Data.Contracts;
using System.Linq;

namespace Application.Services
{
    public class FotoAnuncioService : IFotoAnuncioService
    {
        #region Atributos
        private readonly IFotoAnuncioRepository _fotoAnuncioRepository;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Construtor
        public FotoAnuncioService(
            IFotoAnuncioRepository fotoAnuncioRepository,
            IAmazonS3Service amazonS3Service,
            IUnitOfWork unitOfWork)
        {
            _fotoAnuncioRepository = fotoAnuncioRepository;
            _amazonS3Service = amazonS3Service;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Métodos
        public async Task<bool> AddArchiveAsync(int anuncioId, IFormFile file)
        {
            using (var transaction = _unitOfWork.EFBeginTransaction())
            {
                var sequenciaFotoAnuncio = _fotoAnuncioRepository.LoadLastBy(
                       predicate: p => p.AnuncioId == anuncioId,
                       selector: s => new Domain.FotoAnuncio.FotoAnuncio()
                       {
                           SequenciaFotoAnuncio = s.SequenciaFotoAnuncio
                       },
                       orderBy: o => o.OrderBy(x => x.SequenciaFotoAnuncio))?.SequenciaFotoAnuncio ?? 1;

                var key = $"adimages/{anuncioId}/{sequenciaFotoAnuncio}";
                try
                {
                    var uploadFile = await _amazonS3Service.UploadFileAsync(key, file);

                    if (!uploadFile)
                        throw new Exception("Erro ao fazer upload do arquivo.");

                    _fotoAnuncioRepository.Add(new Domain.FotoAnuncio.FotoAnuncio()
                    {
                        AnuncioId = anuncioId,
                        SequenciaFotoAnuncio = sequenciaFotoAnuncio
                    });

                    return _unitOfWork.EFCommit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    await _amazonS3Service.DeleteFileAsync(key);
                    throw new Exception($"Erro ao salvar foto do anúncio. {ex.Message}");
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task<List<byte[]>> GetArchivesAsync(int anuncioId)
        {
            var keys = _fotoAnuncioRepository.LoadAll(
                predicate: p => p.AnuncioId == anuncioId,
                selector: s => new Domain.FotoAnuncio.FotoAnuncio()
                {
                    SequenciaFotoAnuncio = s.SequenciaFotoAnuncio
                }).Select(x => $"adimages/{anuncioId}/{x.SequenciaFotoAnuncio}").ToList();

            var files = await _amazonS3Service.GetFilesAsync(keys);

            return files;
        }
        #endregion
    }
}
