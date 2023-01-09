using Domain.Domain.ViewModels;
using Application.Interfaces;
using Application.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Login;
using Domain.Domain.Login.Contracts;
using System.Net;
using System.Net.Mail;

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
            var novoLogin = new Login()
            {
                Usuario = login.Usuario,
                Senha = login.Senha,
                TipoLogin = login.TipoLogin,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };
            _loginRepository.Add(novoLogin);
            return true;
        }

        public string Logar(LoginViewModel login)
        {
            var usuario = _loginRepository.LoadFirstBy(predicate: p => p.Usuario == login.Usuario && p.Senha == login.Senha);
            if (usuario != null)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Token")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("LoginId", usuario.LoginId.ToString())
                    .AddExpiry(120)
                    .Builder();
                return token.Value;
            }
            throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        }
        #endregion
    }
}
