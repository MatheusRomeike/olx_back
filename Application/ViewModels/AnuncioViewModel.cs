using Domain.Anuncio.Enums;
using Microsoft.AspNetCore.Http;
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
        public int CategoriaId { get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; }
        public DateTime DataCriacao { get; set; }
        public IFormFile? Foto { get; set; }
        public int? SequenciaFoto { get; set; }

    }
}
