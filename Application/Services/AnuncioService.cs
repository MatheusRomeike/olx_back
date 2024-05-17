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

namespace Application.Services
{
    public class AnuncioService : IAnuncioService
    {
        #region Atributos
        private readonly IAnuncioRepository _anuncioRepository;


        #endregion

        #region Construtor
        public AnuncioService(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }
        #endregion

        #region Métodos
        public void Add(AnuncioViewModel anuncioViewModel)
        {
            var anuncio = new Anuncio
            {
                Titulo = anuncioViewModel.Titulo,
                Descricao = anuncioViewModel.Descricao,
                Preco = anuncioViewModel.Preco,
                EstadoAnuncio = Domain.Anuncio.Enums.EstadoAnuncio.Ativo,
                DataCriacao = DateTime.Now,
                UsuarioId = anuncioViewModel.UsuarioId

            };

            _anuncioRepository.Add(anuncio);
        }

        public Anuncio LoadById(int anuncioId, int usuarioId)
        {
            return _anuncioRepository.LoadFirstBy(x => x.AnuncioId == anuncioId && x.UsuarioId == usuarioId);
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
    }

    #endregion

}
