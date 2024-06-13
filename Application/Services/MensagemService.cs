using Application.Interfaces;
using Domain.Dtos.Mensagem;
using Domain.Mensagem;
using Domain.Mensagem.Contracts;
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
        public List<MensagemDto> List(int anuncioId, int usuarioId) {
            return _mensagemRepository.LoadAll(
                predicate: p => p.UsuarioId == usuarioId && p.AnuncioId == anuncioId,
                selector: s =>new Mensagem() {
                    Texto = s.Texto,
                    DataCriacao = s.DataCriacao
                }
                ).ToList();
            //return [];
        }
        #endregion

    }
}
