using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Dtos.AutoComplete
{
    public class AutoCompleteDto
    {
        #region Atributos
        public string Text { get; set; }
        public string SubText { get; set; }
        #endregion

        #region Construtor
        public AutoCompleteDto() { }
        #endregion
    }
}
