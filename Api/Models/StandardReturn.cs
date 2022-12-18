namespace Api.Models
{
    public class StandardReturn<T>
    {
        #region Atributos
        public string Status { get; set; }

        public T Data { get; set; }
        #endregion

        #region Construtor
        public StandardReturn(ReturnStatus status, T data)
        {
            Status = status.ToString();
            Data = data;
        }
        #endregion
    }
}
