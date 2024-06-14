using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Autenticacao
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string NomeUsuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
