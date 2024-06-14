using Application.Interfaces;
using Application.ViewModels;
using Domain.Dtos.Mensagem;
using Domain.Mensagem;
using Domain.Mensagem.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MensagemService : IMensagemService
    {
        #region Atributos
        private readonly IMensagemRepository _mensagemRepository;
        #endregion

        #region Construtor
        public MensagemService(IMensagemRepository mensagemRepository)
        {
            _mensagemRepository = mensagemRepository;
        }
        #endregion

        #region Métodos
        public List<MensagemDto> List(int anuncioId, int usuarioInteressadoId, int usuarioId)
        {
            return _mensagemRepository.LoadAll(
                predicate: p => p.UsuarioId == usuarioInteressadoId && p.AnuncioId == anuncioId,
                selector: s => new Mensagem()
                {
                    Texto = s.Texto,
                    DataCriacao = s.DataCriacao,
                    Usuario = new Domain.Usuario.Usuario() { Nome = s.Usuario.Nome },
                    UsuarioAutorId = s.UsuarioAutorId,
                    Anuncio = new Domain.Anuncio.Anuncio()
                    {
                        Usuario = new Domain.Usuario.Usuario()
                        {
                            Nome = s.Anuncio.Usuario.Nome
                        }
                    }
                },
                include: i => i.Include(x => x.Usuario).Include(x => x.Anuncio).ThenInclude(x => x.Usuario)
                ).Select(x => new MensagemDto()
                {
                    Texto = x.Texto,
                    DataCriacao = x.DataCriacao,
                    Tipo = x.UsuarioAutorId == usuarioId ? "enviado" : "recebido",
                    Autor = x.UsuarioAutorId == usuarioInteressadoId ? x.Usuario.Nome : x.Anuncio.Usuario.Nome
                }).OrderBy(x => x.DataCriacao).ToList();
            //return [];
        }
        public void Create(MensagemViewModel model, int usuarioLogadoId)
        {
            var sequenciaMensagem = _mensagemRepository.LoadLastBy(
                predicate: p => p.UsuarioId == model.UsuarioId && p.AnuncioId == model.AnuncioId,
                selector: s => new Mensagem()
                {
                    SequenciaMensagem = s.SequenciaMensagem,
                },
                orderBy: o => o.OrderByDescending(x => x.SequenciaMensagem)
                )?.SequenciaMensagem ?? 0;

            var mensagem = new Mensagem()
            {
                Texto = model.Texto,
                AnuncioId = model.AnuncioId,
                UsuarioId = model.UsuarioId,
                UsuarioAutorId = usuarioLogadoId,
                DataCriacao = DateTime.Now,
                SequenciaMensagem = sequenciaMensagem + 1
            };

            _mensagemRepository.Add(mensagem);
        }

        public List<ConversasDto> GetConversas(int usuarioId)
        {
            return _mensagemRepository.GetConversas(usuarioId);
        }
        #endregion

    }
}
