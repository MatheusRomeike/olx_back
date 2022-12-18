using Domain.Domain.Login.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.ViewModels
{
    public class LoginViewModel
    {
        #region Atributos
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public TipoLogin TipoLogin { get; set; }
        #endregion
    }
}
