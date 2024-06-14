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
using Domain.Anuncio;
using Data.Contracts;
using Domain.Dtos.Usuario;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Atributos
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Construtor
        public UsuarioService(IUsuarioRepository usuarioRepository, IAmazonS3Service amazonS3Service, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _amazonS3Service = amazonS3Service;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Métodos
        public bool AdicionarUsuario(UsuarioViewModel usuario)
        {
            if (EmailCadastrado(usuario.Email))
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
            var senhaEncrypt = Encrypt.EncriptyPassword(model.Senha);
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
                    ExpiresIn = 3600,
                    NomeUsuario = usuario.Nome,
                    UsuarioId = usuario.UsuarioId
                };

                return authenticationResult;
            }
            throw new UnauthorizedAccessException("Usuário e/ou senha incorretos.");
        }

        public async Task<bool> AtualizarUsuarioAsync(UsuarioAtualizarViewModel usuario, int usuarioId)
        {
            using (var transaction = _unitOfWork.EFBeginTransaction())
            {
                var usuarioAtual = _usuarioRepository.LoadFirstBy(predicate: p => p.UsuarioId == usuarioId);
                if (usuarioAtual == null)
                    throw new Exception("Usuário não encontrado.");

                if (usuario.Email != usuarioAtual.Email && EmailCadastrado(usuario.Email))
                    throw new Exception("Já existe um usuário cadastrado com este e-mail.");
                var key = $"userimages/{usuarioId}";
                try
                {
                    usuarioAtual.Nome = usuario.Nome;
                    usuarioAtual.DataNascimento = usuario.DataNascimento;
                    usuarioAtual.Email = usuario.Email;
                    _usuarioRepository.Update(usuarioAtual);

                    if (usuario.Foto != null)
                    {
                        var uploadFile = await _amazonS3Service.UploadFileAsync(key, usuario.Foto);
                        if (!uploadFile)
                            throw new Exception("Erro ao fazer upload do arquivo.");
                    }
                    else
                    {
                        await _amazonS3Service.DeleteFileAsync(key);
                    }
                    _unitOfWork.EFCommit();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    await _amazonS3Service.DeleteFileAsync(key);
                    throw new Exception($"Erro ao atualizar usuário. {ex.Message}");
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task<UsuarioDto> ObterAsync(int usuarioId)
        {
            var usuario = _usuarioRepository.LoadFirstBy(predicate: p => p.UsuarioId == usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var byteArray = await _amazonS3Service.GetFileAsync($"userimages/{usuarioId}");

            var usuarioViewModel = new UsuarioDto()
            {
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento,
                Email = usuario.Email,
                Foto = byteArray == null ? null : $"data:image/jpeg;base64,{Convert.ToBase64String(byteArray)}"
            };

            return usuarioViewModel;
        }

        private bool EmailCadastrado(string email)
        {
            var emailCadastrado = _usuarioRepository.LoadFirstBy(predicate: p => p.Email == email, selector: s => new Usuario() { UsuarioId = s.UsuarioId });
            return emailCadastrado != null;
        }
        #endregion
    }
}