using Domain.Anuncio.Enums;

namespace Domain.Anuncio
{
    public class Anuncio
    {
        #region Atributos
        public int AnuncioId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public EstadoAnuncio EstadoAnuncio { get; set; }
        public DateTime DataCriacao { get; set; }
        #endregion

        #region Construtor
        public Anuncio()
        {
            DataCriacao = DateTime.Now;
        }
        #endregion

        #region Relacionamentos
        public virtual Usuario.Usuario Usuario { get; set; }
        public virtual List<FotoAnuncio.FotoAnuncio> FotosAnuncio { get; set; }
        public virtual List<AnuncioCategoria.AnuncioCategoria> AnuncioCategorias { get; set; }
        public virtual List<Interesse.Interesse> Interesses { get; set; }
        public virtual List<Mensagem.Mensagem> Mensagens { get; set; }
        #endregion
    }
}
