using Application.ViewModels;
using Domain.Anuncio;
using Domain.Dtos.Anuncio;
using Domain.Dtos.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAnuncioService
    {
        Task<int> Add(AnuncioViewModel anuncioViewModel);
        Task<AnuncioDto> LoadByIdAsync(int anuncioId, int usuarioId, int usuarioLogadoId);

        void Update(AnuncioViewModel anuncioViewModel);

        void AlterarStatus(AlterarStatusAnuncioViewModel model, int usuarioId);

        List<RelatorioVendasDto> RelatorioVendasAnuncio(RelatorioVendasViewModel model, int usuarioId);

        Task<IEnumerable<AnuncioDto>> List(FiltrarAnuncioViewModel model, int usuarioId);

        List<Anuncio> LoadByUsuario(int usuarioId);
        List<CategoriaDto> LoadCategorias();
        Task InserirFotoAsync(AnuncioViewModel anuncioViewModel);


        string GetTituloAnuncio(int anuncioId);

    }
}
