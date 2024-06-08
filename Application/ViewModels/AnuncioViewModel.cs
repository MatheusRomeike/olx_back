using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AnuncioViewModel
    {
        public int? AnuncioId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
