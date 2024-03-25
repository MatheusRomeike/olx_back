namespace Domain.Mensagem
{
    public class Mensagem
    {
        #region Atributos
        public int UsuarioId { get; set; }
        public int AnuncioId { get; set; }
        public int SequenciaMensagem { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        #endregion

        #region Construtores
        public Mensagem()
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
