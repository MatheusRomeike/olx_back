using Application.Interfaces;
using Application.Token;
using Application.ViewModels;
using Domain.Login;
using Domain.Login.Contracts;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        #region Atributos
        private readonly ILoginRepository _loginRepository;
        #endregion

        #region Construtor
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        #endregion

        #region Métodos
        public bool AdicionarLogin(LoginViewModel login)
        {

            return true;
        }

        public string Logar(LoginViewModel login)
        {
            //if (usuario != null)
            //{
            //    var token = new TokenJWTBuilder()
            //        .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
            //        .AddSubject("Token")
            //        .AddIssuer("Teste.Security.Bearer")
            //        .AddAudience("Teste.Security.Bearer")
            //        .AddClaim("LoginId", usuario.LoginId.ToString())
            //        .AddExpiry(120)
            //        .Builder();
            //    return token.Value;
            //}
            throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        }
        #endregion
    }
}
