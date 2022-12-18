using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Token
{
    public class TokenJWTBuilder
    {
        #region Atributos
        private SecurityKey SecurityKey;
        private string Subject = "";
        private string Issuer = "";
        private string Audience = "";
        private Dictionary<string, string> Claims = new Dictionary<string, string>();
        private int ExpiryInMinutes = 5;
        #endregion

        #region Métodos
        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.SecurityKey = securityKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            this.Subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            this.Issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            this.Audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaim(string type, string value)
        {
            this.Claims.Add(type, value);
            return this;
        }

        public TokenJWTBuilder AddExpiry(int timeInMinutes)
        {
            this.ExpiryInMinutes = timeInMinutes;
            return this;
        }

        private void EnsureArguments()
        {
            if (this.SecurityKey == null)
                throw new ArgumentException("Security Key");
            if (string.IsNullOrEmpty(this.Subject))
                throw new ArgumentException("Subject");
            if (string.IsNullOrEmpty(this.Issuer))
                throw new ArgumentException("Issuer");
            if (string.IsNullOrEmpty(this.Audience))
                throw new ArgumentException("Audience");
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.Claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.Issuer,
                audience: this.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiryInMinutes),
                signingCredentials: new SigningCredentials(
                                                    this.SecurityKey,
                                                    SecurityAlgorithms.HmacSha256)
                );

            return new TokenJWT(token);
        }
        #endregion
    }
}
