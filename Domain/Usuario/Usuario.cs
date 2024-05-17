namespace Domain.Usuario
{
    public class Usuario
    {
        #region Atributos
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        #endregion

        #region Construtor
        public Usuario() { }
        #endregion

        #region Relacionamentos
        public virtual List<Anuncio.Anuncio> Anuncios { get; set; }
        public virtual List<Mensagem.Mensagem> Mensagens { get; set; }
        public virtual List<Interesse.Interesse> Interesses { get; set; }
        #endregion
    }
}
