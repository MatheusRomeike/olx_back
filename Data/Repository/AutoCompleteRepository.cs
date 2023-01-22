using Domain.Domain.Login.Contracts;
using Domain.Domain.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Dtos.AutoComplete.Contracts;
using Domain.Domain.Dtos.AutoComplete;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<AutoCompleteDto> AutoCompleteLogin(string search)
        {
            return _loginRepository.LoadAll(predicate: p => p.Usuario.Contains(search),
                                            limit: 10,
                                            selector: s => new Login()
                                            {
                                                Usuario = s.Usuario,
                                                DataCadastro = s.DataCadastro,
                                                LoginId = s.LoginId
                                            }).Select(s => new AutoCompleteDto()
                                            {
                                                Text = s.Usuario,
                                                SubText = s.DataCadastro.ToString(),
                                                Id = s.LoginId
                                            });
        }
    }
}
