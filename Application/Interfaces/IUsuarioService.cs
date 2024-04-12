using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> AdicionarUsuarioAsync(UsuarioViewModel usuario);
        string Logar(UsuarioViewModel Usuario);
    }
}
