using Domain.Domain.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Dtos.AutoComplete.Contracts
{
    public interface IAutoCompleteRepository : IBaseRepository<AutoCompleteDto>
    {
        /// <summary>
        /// Método responsável por trazer uma lista de usuario e loginId.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<AutoCompleteDto> AutoCompleteLogin(string search);
    }
}
