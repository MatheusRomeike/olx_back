using System.IdentityModel.Tokens.Jwt;

namespace Application.Token
{
    public class TokenJWT
    {
        #region Atributos
        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);

        private JwtSecurityToken token;
        #endregion

        #region Construtor
        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }
        #endregion
    }
}
