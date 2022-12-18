using Domain.Domain.Login;
using Domain.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Método para cadastro de login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool AdicionarLogin(LoginViewModel login);

        /// <summary>
        /// Método reponsável por gerar token JWT
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        string GerarToken(LoginViewModel login);
    }
}
