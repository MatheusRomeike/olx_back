using Application.Interfaces;
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
    public class MensagemService: IMensagemService
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
        public List<MensagemDto> List(int anuncioId, int usuarioInteressadoId, int usuarioId) {
            return _mensagemRepository.LoadAll(
                predicate: p => p.UsuarioId == usuarioInteressadoId && p.AnuncioId == anuncioId,
                selector: s => new Mensagem() {
                    Texto = s.Texto,
                    DataCriacao = s.DataCriacao,
                    Usuario = new Domain.Usuario.Usuario() { Nome = s.Usuario.Nome },
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
                    Tipo = usuarioInteressadoId == usuarioId ? "received" : "sent",
                    Autor = usuarioInteressadoId == usuarioId ? x.Usuario.Nome : x.Anuncio.Usuario.Nome
                }).ToList();
            //return [];
        }
        #endregion

    }
}
