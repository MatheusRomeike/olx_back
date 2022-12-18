using Domain.Domain.Login.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Login
{
    public class Login
    {
        #region Atributos
        public int LoginId { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public TipoLogin TipoLogin { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        #endregion

        #region Construtor
        public Login() { }
        #endregion
    }
}
