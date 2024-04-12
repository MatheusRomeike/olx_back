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

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Atributos
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager ;

        #endregion

        #region Construtor
        public UsuarioService(IUsuarioRepository usuarioRepository, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Métodos
        public async Task<bool> AdicionarUsuarioAsync(UsuarioViewModel usuario)
        {
            var novoUsuario = new Usuario()
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento
            };
            var result = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

            return result.Succeeded;
        }

        //public async Task<string> Logar(UsuarioViewModel Usuario)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(Usuario.Email, Usuario.Senha, isPersistent: false, lockoutOnFailure: false);

        //    if (result.Succeeded)
        //    {
        //        var usuario = await _userManager.FindByEmailAsync(Usuario.Email);
        //        var token = await GerarToken(usuario);
        //        return token;
        //    }

        //    throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        //}

        //private string GerarToken(Usuario usuario)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var chave = Encoding.ASCII.GetBytes(_chaveSecreta);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, usuario.UsuarioId.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7), // Define o tempo de expiração do token
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public string Logar(UsuarioViewModel Usuario)
        {
            var usuario = _usuarioRepository.LoadFirstBy(predicate: p => p.Email == Usuario.Email && p.Senha == Usuario.Senha);
            if (usuario != null)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Token")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("UsuarioId", usuario.UsuarioId.ToString())
                    .AddExpiry(120)
                    .Builder();
                return token.Value;
            }
            throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        }
        #endregion
    }
}