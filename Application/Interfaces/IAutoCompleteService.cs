using Domain.Domain.Dtos.AutoComplete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAutoCompleteService
    {
        /// <summary>
        /// Método responsável por trazer uma lista de usuario e loginId.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<AutoCompleteDto> AutoCompleteLogin(string search);
    }
}
