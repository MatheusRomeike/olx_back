using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Anuncio
{
    public class RelatorioVendasDto
    {
        [DefaultValueAttribute("Computador usado")]
        public string Titulo { get; set; }
        [DefaultValueAttribute(EstadoAnuncio.Ativo)]
        public EstadoAnuncio EstadoAnuncio { get; set; }
    }
}
