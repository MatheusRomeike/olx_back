using Application.Token;
using Application.ViewModels;
using Domain.Dtos.Autenticacao;
using Domain.Usuario.Contracts;
using Domain.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Anuncio.Contracts;
using Domain.Anuncio;
using Application.Interfaces;
using Domain.Dtos.Anuncio;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Domain.Dtos.Usuario;
using Domain.FotoAnuncio.Contracts;
using Domain.Categoria.Contracts;
using Domain.Dtos.Categoria;
using Domain.Categoria;
using System.Linq.Expressions;
using Data.Repository;

namespace Application.Services
{
    public class AnuncioService : IAnuncioService
    {
        #region Atributos
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IFotoAnuncioService _fotoAnuncioService;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IFotoAnuncioRepository _fotoAnuncioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        #endregion

        #region Construtor
        public AnuncioService(IAnuncioRepository anuncioRepository, IFotoAnuncioService fotoAnuncioService, IAmazonS3Service amazonS3Service, IFotoAnuncioRepository fotoAnuncioRepository, IUsuarioRepository usuarioRepository, ICategoriaRepository categoriaRepository)
        {
            _anuncioRepository = anuncioRepository;
            _fotoAnuncioService = fotoAnuncioService;
            _amazonS3Service = amazonS3Service;
            _fotoAnuncioRepository = fotoAnuncioRepository;
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriaRepository;
        }
        #endregion

        #region Métodos
        public async Task<int> Add(AnuncioViewModel anuncioViewModel)
        {
            try
            {
                var anuncio = new Anuncio
                {
                    Titulo = anuncioViewModel.Titulo,
                    Descricao = anuncioViewModel.Descricao,
                    Preco = anuncioViewModel.Preco,
                    EstadoAnuncio = Domain.Anuncio.Enums.EstadoAnuncio.Ativo,
                    DataCriacao = DateTime.Now,
                    UsuarioId = anuncioViewModel.UsuarioId,
                    CategoriaId = anuncioViewModel.CategoriaId

                };

                _anuncioRepository.Add(anuncio);

                return anuncio.AnuncioId;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        private List<IFormFile> AgruparFotos(AnuncioViewModel model)
        {
            List<IFormFile> files = new List<IFormFile>();

            var properties = typeof(AnuncioViewModel).GetProperties()
                             .Where(p => p.PropertyType == typeof(IFormFile));

            foreach (var property in properties)
            {
                var file = (IFormFile)property.GetValue(model);
                if (file != null)
                {
                    files.Add(file);
                }
            }

            return files;
        }

        public async Task<IEnumerable<AnuncioDto>> List(FiltrarAnuncioViewModel model, int usuarioId)
        {
            var anunciosDto = new List<AnuncioDto>();
            Expression<Func<Anuncio, bool>> predicate = x => x.EstadoAnuncio == Domain.Anuncio.Enums.EstadoAnuncio.Ativo;
            Func<IQueryable<Anuncio>, IOrderedQueryable<Anuncio>> orderBy = x => x.OrderByDescending(x => x.DataCriacao);

            if (model.Titulo.HasValue)
                orderBy = model.Titulo.Value == Domain.Anuncio.Enums.Ordem.DESC ? x => x.OrderByDescending(y => y.Titulo) : x => x.OrderBy(z => z.Titulo);

            if (model.Preco.HasValue)
                orderBy = model.Preco.Value == Domain.Anuncio.Enums.Ordem.DESC ? x => x.OrderByDescending(y => y.Preco) : x => x.OrderBy(z => z.Preco);

            predicate = AndAlsoWhen(predicate, x => x.CategoriaId == model.CategoriaId, () => model.CategoriaId.HasValue);
            predicate = AndAlsoWhen(predicate, x => x.Preco >= model.PrecoMin, () => model.PrecoMin.HasValue);
            predicate = AndAlsoWhen(predicate, x => x.Preco <= model.PrecoMax, () => model.PrecoMax.HasValue);
            predicate = AndAlsoWhen(predicate, x => x.UsuarioId != usuarioId, () => true);

            var anuncios = _anuncioRepository.LoadAll(predicate: predicate, orderBy: orderBy, include: i => i.Include(x => x.Usuario));

            if (anuncios == null || anuncios.Count() == 0)
                return anunciosDto;

            foreach (var anuncio in anuncios)
            {
                anunciosDto.Add(new AnuncioDto()
                {
                    UsuarioId = anuncio.UsuarioId,
                    Descricao = anuncio.Descricao,
                    Preco = anuncio.Preco,
                    AnuncioId = anuncio.AnuncioId,
                    Titulo = anuncio.Titulo,
                    Fotos = new List<string>()
                    {
                        "https://olx-bucket-free.s3.amazonaws.com/adimages/0/1",
                        //$"https://olx-bucket-free.s3.amazonaws.com/adimages/{anuncio.AnuncioId}/1"
                    },
                    Usuario = anuncio.Usuario,
                    DescricaoCategoria = anuncio.CategoriaId != 0 ? _categoriaRepository.LoadFirstBy(x => x.CategoriaId == anuncio.CategoriaId).Descricao : null
                });
            }



            //anuncio.FotosAnuncio = _fotoAnuncioRepository.LoadAll(x => x.AnuncioId == anuncioId).ToList();
            //foreach (var item in anuncio.FotosAnuncio)
            //{
            //    var byteArray = await _amazonS3Service.GetFileAsync($"adimages/{anuncioId}/{item.SequenciaFotoAnuncio}");
            //    var foto = byteArray == null ? null : $"data:image/jpeg;base64,{Convert.ToBase64String(byteArray)}";
            //    retorno.Fotos.Add(foto);
            //}

            return anunciosDto;
        }

        public Expression<Func<T, bool>> AndAlsoWhen<T>(
     Expression<Func<T, bool>> baseCondition,
    Expression<Func<T, bool>> andAlsoCondition,
    Func<bool> whenCondition)
        {
            if (whenCondition.Invoke())
            {
                var body = Expression.AndAlso(baseCondition.Body, Expression.Invoke(andAlsoCondition, baseCondition.Parameters[0]));
                baseCondition = Expression.Lambda<Func<T, bool>>(body, baseCondition.Parameters[0]);
            }
            return baseCondition;
        }

        public async Task<AnuncioDto> LoadByIdAsync(int anuncioId, int usuarioId)
        {

            var anuncio = _anuncioRepository.LoadFirstBy(x => x.AnuncioId == anuncioId && x.UsuarioId == usuarioId, include: j => j.Include(u => u.Usuario));
            if (anuncio == null)
                throw new Exception("Anúncio não encontrado.");

            var retorno = new AnuncioDto()
            {
                DataCriacao = anuncio.DataCriacao,
                Descricao = anuncio.Descricao,
                Preco = anuncio.Preco,
                AnuncioId = anuncioId,
                EstadoAnuncio = anuncio.EstadoAnuncio,
                Titulo = anuncio.Titulo,
                UsuarioId = usuarioId,
                Fotos = new List<string>(),
                Usuario = anuncio.Usuario,
                DescricaoCategoria = anuncio.CategoriaId != 0 ? _categoriaRepository.LoadFirstBy(x => x.CategoriaId == anuncio.CategoriaId).Descricao : null,
                CategoriaId = anuncio.CategoriaId,
            };

            anuncio.FotosAnuncio = _fotoAnuncioRepository.LoadAll(x => x.AnuncioId == anuncioId).ToList();
            foreach (var item in anuncio.FotosAnuncio)
            {
                var byteArray = await _amazonS3Service.GetFileAsync($"adimages/{anuncioId}/{item.SequenciaFotoAnuncio}");
                var foto = byteArray == null ? null : $"data:image/jpeg;base64,{Convert.ToBase64String(byteArray)}";
                retorno.Fotos.Add(foto);
            }

            return retorno;
        }

        public void Update(AnuncioViewModel anuncioViewModel)
        {
            if (anuncioViewModel.AnuncioId == null)
                throw new Exception("Anúncio não encontrado.");

            var anuncioExistente = _anuncioRepository.LoadById((int)anuncioViewModel.AnuncioId);

            if (anuncioExistente == null)
                throw new Exception("Anúncio não encontrado.");

            anuncioExistente.Titulo = anuncioViewModel.Titulo;
            anuncioExistente.Descricao = anuncioViewModel.Descricao;
            anuncioExistente.Preco = anuncioViewModel.Preco;
            anuncioExistente.EstadoAnuncio = Domain.Anuncio.Enums.EstadoAnuncio.Ativo;

            _anuncioRepository.Update(anuncioExistente);
        }

        public void AlterarStatus(AlterarStatusAnuncioViewModel model, int usuarioId)
        {
            var anuncio = _anuncioRepository.LoadFirstBy(x => x.AnuncioId == model.AnuncioId && x.UsuarioId == usuarioId);

            if (anuncio == null)
                throw new Exception("Anúncio não encontrado.");
            anuncio.EstadoAnuncio = model.Estado;
            _anuncioRepository.Update(anuncio);
        }

        public List<Anuncio> LoadByUsuario(int usuarioId)
        {
            return _anuncioRepository.LoadAll(x => x.UsuarioId == usuarioId && x.EstadoAnuncio != Domain.Anuncio.Enums.EstadoAnuncio.Inativo).ToList();
        }

        public List<RelatorioVendasDto> RelatorioVendasAnuncio(RelatorioVendasViewModel model, int usuarioId)
        {
            return _anuncioRepository.LoadAll(
                predicate: p => p.UsuarioId == usuarioId && p.DataCriacao >= model.DataInicial && p.DataCriacao <= model.DataFinal,
                selector: s => new Anuncio()
                {
                    Titulo = s.Titulo,
                    EstadoAnuncio = s.EstadoAnuncio,
                }).Select(x => new RelatorioVendasDto()
                {
                    Titulo = x.Titulo,
                    EstadoAnuncio = x.EstadoAnuncio
                }).ToList();
        }

        public List<CategoriaDto> LoadCategorias()
        {
            return _categoriaRepository.LoadAll()
                                       .Select(x => new CategoriaDto
                                       {
                                           Descricao = x.Descricao,
                                           Id = x.CategoriaId
                                       }).ToList();
        }

        public async Task InserirFotoAsync(AnuncioViewModel anuncioViewModel)
        {
            try
            {
                await _fotoAnuncioService.AddArchiveAsync((int)anuncioViewModel.AnuncioId, anuncioViewModel.Foto, (int)anuncioViewModel.SequenciaFoto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public string GetTituloAnuncio(int anuncioId)
        {
            return _anuncioRepository.LoadFirstBy(x => x.AnuncioId == anuncioId, selector: s => new Anuncio() { Titulo = s.Titulo })?.Titulo;
        }
    }

    #endregion

}
