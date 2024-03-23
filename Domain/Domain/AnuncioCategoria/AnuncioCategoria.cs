namespace Domain.Domain.AnuncioCategoria
{
    public class AnuncioCategoria
    {
        #region Atributos
        public int AnuncioId { get; set; }
        public int CategoriaId { get; set; }
        #endregion

        #region Construtor
        public AnuncioCategoria() { }
        #endregion

        #region Relacionamentos
        public virtual Anuncio.Anuncio Anuncio { get; set; }
        public virtual Categoria.Categoria Categoria { get; set; }
        #endregion
    }
}
