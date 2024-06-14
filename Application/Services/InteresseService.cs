using Application.Interfaces;
using Application.ViewModels;
using Domain.Anuncio.Contracts;
using Domain.Dtos.Interesse;
using Domain.Interesse.Contracts;
using Domain.Mensagem.Contracts;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAnuncioRepository _anuncioRepository;
        #endregion

        #region Construtor
        public InteresseService(IInteresseRepository interesseRepository, IAnuncioRepository anuncioRepository)
        {
            _interesseRepository = interesseRepository;
            _anuncioRepository = anuncioRepository;
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

        public List<InteresseDto> List(int usuarioId)
        {
            return _interesseRepository.LoadAll(
                predicate: x => x.UsuarioId == usuarioId,
                include: x => x.Include(i => i.Anuncio).Select(s => );

            return [];
        } 
        #endregion
    }
}
