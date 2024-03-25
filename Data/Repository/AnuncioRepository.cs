using Domain.Anuncio;
using Domain.Anuncio.Contracts;
using Domain.Login;
using Domain.Login.Contracts;

namespace Data.Repository
{
    public class AnuncioRepository : BaseRepository<Anuncio>, IAnuncioRepository
    {
    }
}
