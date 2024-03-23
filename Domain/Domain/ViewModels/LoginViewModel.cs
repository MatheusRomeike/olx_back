using Domain.Domain.Login.Enums;

namespace Domain.Domain.ViewModels
{
    public class LoginViewModel
    {
        #region Atributos
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public TipoLogin TipoLogin { get; set; }
        #endregion
    }
}
