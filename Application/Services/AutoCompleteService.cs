using Application.Interfaces;
using Domain.AutoComplete;
using Domain.AutoComplete.Contracts;

namespace Application.Services
{
    public class AutoCompleteService : IAutoCompleteService
    {
        #region Atributos
        private readonly IAutoCompleteRepository _autoCompleteRepository;
        #endregion

        #region Construtor
        public AutoCompleteService(IAutoCompleteRepository autoCompleteRepository)
        {
            _autoCompleteRepository = autoCompleteRepository;
        }
        #endregion

        #region Métodos

        #endregion
    }
}
