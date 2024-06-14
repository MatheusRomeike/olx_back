using Application.Interfaces;
using Domain.FotoAnuncio.Contracts;
using Microsoft.AspNetCore.Http;
using Data.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<bool> AddArchiveAsync(int anuncioId, IFormFile file, int sequencia)
        {
            var key = $"adimages/{anuncioId}/{sequencia}";
            try
            {
                // Verificar e desanexar entidade existente
                var existingEntity = await _fotoAnuncioRepository.FindAsync(anuncioId, sequencia);
                if (existingEntity != null)
                {
                    _fotoAnuncioRepository.Delete(existingEntity);
                   await  _amazonS3Service.DeleteFilesAsync([key]);
                }

                var uploadFile = await _amazonS3Service.UploadFileAsync(key, file);

                _fotoAnuncioRepository.Add(new Domain.FotoAnuncio.FotoAnuncio()
                {
                    AnuncioId = anuncioId,
                    SequenciaFotoAnuncio = sequencia
                });

                return true;
            }
            catch (Exception ex)
            {

                await _amazonS3Service.DeleteFileAsync(key);
                throw new Exception($"Erro ao salvar foto do anúncio. {ex.Message}");
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
