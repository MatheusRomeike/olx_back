using Application.ViewModels;

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
        string Logar(LoginViewModel login);
    }
}
