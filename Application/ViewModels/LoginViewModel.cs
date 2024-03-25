using Domain.Login.Enums;

namespace Application.ViewModels
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
