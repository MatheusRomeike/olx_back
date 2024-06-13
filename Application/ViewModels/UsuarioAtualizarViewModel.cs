using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class UsuarioAtualizarViewModel
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public IFormFile? Foto { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set;}
    }
}
