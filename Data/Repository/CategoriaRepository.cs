using Domain.Anuncio;
using Domain.Anuncio.Contracts;
using Domain.AnuncioCategoria;
using Domain.AnuncioCategoria.Contracts;
using Domain.Categoria;
using Domain.Categoria.Contracts;
using Domain.Login;
using Domain.Login.Contracts;

namespace Data.Repository
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
    }
}
