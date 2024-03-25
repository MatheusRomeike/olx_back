namespace Domain.FotoAnuncio
{
    public class FotoAnuncio
    {
        #region Atributos
        public int AnuncioId { get; set; }
        public int SequenciaFotoAnuncio { get; set; }
        public string Foto { get; set; }
        #endregion

        #region Construtor
        public FotoAnuncio() { }
        #endregion

        #region Relacionamentos
        public virtual Anuncio.Anuncio Anuncio { get; set; }
        #endregion
    }
}
