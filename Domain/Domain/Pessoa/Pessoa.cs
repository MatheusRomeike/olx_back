using Domain.Domain.Pessoa.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Pessoa
{
    public class Pessoa
    {
        #region Atributos
        public string PessoaId { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        #endregion

        #region Construtor
        public Pessoa() { }
        #endregion
    }
}
