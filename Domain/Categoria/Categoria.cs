namespace Domain.Categoria
{
    public class Categoria
    {
        #region Atributos
        public int CategoriaId { get; set; }
        public string Descricao { get; set; }
        #endregion

        #region Construtor
        public Categoria() { }
        #endregion

        #region Relacionamentos
        public virtual ICollection<AnuncioCategoria.AnuncioCategoria> AnunciosCategorias { get; set; }
        #endregion
    }
}
