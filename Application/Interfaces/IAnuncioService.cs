using Application.ViewModels;
using Domain.Anuncio;
using Domain.Dtos.Anuncio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAnuncioService
    {
        void Add(AnuncioViewModel anuncioViewModel);
        Task<AnuncioDto> LoadByIdAsync(int anuncioId, int usuarioId);

        void Update(AnuncioViewModel anuncioViewModel);

        void Delete(int anuncioId);

        List<RelatorioVendasDto> RelatorioVendasAnuncio(RelatorioVendasViewModel model, int usuarioId);

        Task<IEnumerable<AnuncioDto>> List(FiltrarAnuncioViewModel model, int usuarioId);

        List<Anuncio> LoadByUsuario(int usuarioId);

    }
}
