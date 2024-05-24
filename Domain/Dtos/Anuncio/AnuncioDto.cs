using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Anuncio
{
    public class AnuncioDto
    {
        public int AnuncioId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<string> Fotos { get; set; } 
    }
}
