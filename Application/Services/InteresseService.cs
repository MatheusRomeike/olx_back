using Application.Interfaces;
using Application.ViewModels;
using Domain.Interesse.Contracts;
using Domain.Mensagem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InteresseService : IInteresseService
    {
        #region Atributos
        private readonly IInteresseRepository _interesseRepository;
        #endregion

        #region Construtor
        public InteresseService(IInteresseRepository interesseRepository)
        {
            _interesseRepository = interesseRepository;
        }
        #endregion

        #region Métodos
        public void Toggle(InteresseViewModel model, int usuarioLogadoId)
        {
            var interesse = _interesseRepository.LoadFirstBy(
                x => x.AnuncioId == model.AnuncioId && x.UsuarioId == usuarioLogadoId
                );
            if ( interesse != null )
            {
                _interesseRepository.Delete( interesse );
            } 
            else
            {
                _interesseRepository.Add(new Domain.Interesse.Interesse()
                {
                    AnuncioId = model.AnuncioId,
                    UsuarioId = usuarioLogadoId
                });
            }
        }
        #endregion
    }
}
