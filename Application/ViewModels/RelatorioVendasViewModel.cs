using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RelatorioVendasViewModel
    {
        [DefaultValueAttribute("2024-06-01T00:00:00.000Z")]
        public DateTime DataInicial { get; set; }
        [DefaultValueAttribute("2024-06-30T00:00:00.000Z")]
        public DateTime DataFinal { get; set; }
    }
}
