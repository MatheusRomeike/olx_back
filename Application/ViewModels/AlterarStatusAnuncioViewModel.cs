using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AlterarStatusAnuncioViewModel
    {
        public int AnuncioId { get; set; }
        public EstadoAnuncio Estado { get; set; }
    }
}
