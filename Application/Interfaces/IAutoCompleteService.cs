using Domain.Domain.Dtos.AutoComplete;

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
