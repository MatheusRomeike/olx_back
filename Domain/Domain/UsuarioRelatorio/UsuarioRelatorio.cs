namespace Domain.Domain.UsuarioRelatorio
{
    public class UsuarioRelatorio
    {
        #region Atributos
        public int UsuarioId { get; set; }
        public int SequenciaRelatorio { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        #endregion

        #region Construtor
        public UsuarioRelatorio()
        {
            DataCadastro = DateTime.Now;
        }
        #endregion

        #region Relacionamentos
        public virtual Usuario.Usuario Usuario { get; set; }
        #endregion
    }
}
