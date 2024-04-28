using Application.ViewModels;
using Domain.Dtos.Autenticacao;
using Domain.Dtos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        bool AdicionarUsuario(UsuarioViewModel usuario);
        TokenDto Logar(LoginViewModel Usuario);
        Task<bool> AtualizarUsuarioAsync(UsuarioAtualizarViewModel usuario, int usuarioId);
        Task<UsuarioDto> ObterAsync(int usuarioId);
    }
}
