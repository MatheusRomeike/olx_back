using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class FiltrarAnuncioViewModel
    {
        public Ordem? Titulo {  get; set; }
        public Ordem? Preco {  get; set; }
        public int? CategoriaId { get; set; }
        public decimal? PrecoMax { get; set; }
        public decimal? PrecoMin { get; set; }
    }
}
