using Domain.Domain.Dtos.AutoComplete;
using Domain.Domain.Dtos.AutoComplete.Contracts;
using Domain.Domain.Login;
using Domain.Domain.Login.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AutoCompleteRepository : BaseRepository<AutoCompleteDto>, IAutoCompleteRepository
    {
        #region Atributos
        private readonly IAnuncioCategoriaRepository _loginRepository;
        #endregion

        #region Construtor
        public AutoCompleteRepository(IAnuncioCategoriaRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        #endregion

        #region Metodos
        #endregion
    }
}
