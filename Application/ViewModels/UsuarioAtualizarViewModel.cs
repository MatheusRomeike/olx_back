using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class UsuarioAtualizarViewModel
    {
        [DefaultValueAttribute("matheus")]
        public string Nome { get; set; }
        [DefaultValueAttribute("01/01/2000")]
        public string DataNascimento { get; set; }
        [DefaultValueAttribute("matheus@gmail.com")]
        public string Email { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
