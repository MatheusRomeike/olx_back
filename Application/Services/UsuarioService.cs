using Application.Interfaces;
using Application.Token;
using Domain.Usuario.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Domain.Usuario;
using Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Dtos.Autenticacao;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Atributos
        private readonly IUsuarioRepository _usuarioRepository;


        #endregion

        #region Construtor
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        #endregion

        #region Métodos
        public bool AdicionarUsuario(UsuarioViewModel usuario)
        {
            var emailCadastrado = _usuarioRepository.LoadFirstBy(predicate: p => p.Email == usuario.Email, selector: s => new Usuario() { UsuarioId = s.UsuarioId });
            if (emailCadastrado != null)
                throw new Exception("Já existe um usuário cadastrado com este e-mail.");

            var senhaEncrypt = Encrypt.EncriptyPassword(usuario.Senha);
            var novoUsuario = new Usuario()
            {
                Email = usuario.Email,
                Senha = senhaEncrypt,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento,
            };

            _usuarioRepository.Add(novoUsuario);

            return true;
        }

        public TokenDto Logar(LoginViewModel model)
        {
            var senhaEncrypt = Encrypt.EncriptyPassword(model.Password);
            var usuario = _usuarioRepository.LoadFirstBy(predicate: p => p.Email == model.Email && p.Senha == senhaEncrypt);
            if (usuario != null)
            {
                var token = new TokenJWTBuilder()
                 .AddSecurityKey(JwtSecurityKey.Create("b7e94be513e96e8c45cd23d162275e5a12ebde9100a425c4ebcdd7fa4dcd897c"))
                 .AddSubject("Token")
                 .AddIssuer("Teste.Security.Bearer")
                 .AddAudience("Teste.Security.Bearer")
                 .AddClaim("UsuarioId", usuario.UsuarioId.ToString())
                 .AddExpiry(60)
                 .Builder();

                var authenticationResult = new TokenDto
                {
                    AccessToken = token.Value,
                    ExpiresIn = 3600
                };

                return authenticationResult;
            }
            throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        }
        #endregion
    }
}