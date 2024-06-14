using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Interesse
{
    public class InteresseDto
    {
        public int AnuncioId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo {  get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; } 

    }
}
