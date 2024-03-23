namespace Domain.Domain.Interesse
{
    public class Interesse
    {
        #region Atributos
        public int UsuarioId { get; set; }
        public int AnuncioId { get; set; }
        public DateTime DataCriacao { get; set; }
        #endregion

        #region Construtor
        public Interesse()
        {
            DataCriacao = DateTime.Now;
        }
        #endregion

        #region Relacionamentos
        public virtual Usuario.Usuario Usuario { get; set; }
        public virtual Anuncio.Anuncio Anuncio { get; set; }
        #endregion
    }
}
