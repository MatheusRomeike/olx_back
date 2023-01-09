using Application.Interfaces;
using Domain.Domain.Dtos.AutoComplete;
using Domain.Domain.Dtos.AutoComplete.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<AutoCompleteDto> AutoCompleteLogin(string search)
        {
            return _autoCompleteRepository.AutoCompleteLogin(search);
        }
        #endregion
    }
}
