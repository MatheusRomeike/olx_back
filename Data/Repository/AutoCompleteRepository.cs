using Domain.AutoComplete;
using Domain.AutoComplete.Contracts;
using Domain.Login.Contracts;

namespace Data.Repository
{
    public class AutoCompleteRepository : BaseRepository<AutoCompleteDto>, IAutoCompleteRepository
    {
        #region Atributos
        private readonly ILoginRepository _loginRepository;
        #endregion

        #region Construtor
        public AutoCompleteRepository(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        #endregion

        #region Metodos
        #endregion
    }
}
