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
                CategoriaId = anuncio.CategoriaId 

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
            anuncioExistente.EstadoAnuncio = anuncioViewModel.EstadoAnuncio;

            _anuncioRepository.Update(anuncioExistente);
        }

        public void Delete(int anuncioId)
        {
            var anuncio = _anuncioRepository.LoadById(anuncioId);

            if (anuncio == null)
                throw new Exception("Anúncio não encontrado.");
            anuncio.EstadoAnuncio = Domain.Anuncio.Enums.EstadoAnuncio.Inativo;
            _anuncioRepository.Update(anuncio);
        }

        public List<Anuncio> LoadByUsuario(int usuarioId)
        {
            return _anuncioRepository.LoadAll(x => x.UsuarioId == usuarioId).ToList();
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
    }

    #endregion

}
