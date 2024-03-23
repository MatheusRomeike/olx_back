namespace Domain.Domain.Login
{
    public class Login
    {
        #region Atributos
        public int LoginId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        #endregion

        #region Construtor
        public Login() { }
        #endregion

        #region Relacionamentos
        public virtual Usuario.Usuario Usuario { get; set; }
        #endregion
    }
}
