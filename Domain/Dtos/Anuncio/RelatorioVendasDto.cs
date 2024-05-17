using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Anuncio
{
    public class RelatorioVendasDto
    {
        public string Titulo { get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; }
    }
}
