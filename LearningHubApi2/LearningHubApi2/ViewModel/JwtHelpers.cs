using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningHubApi2.ViewModel
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts)
        {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userAccounts.UserName),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.Role, userAccounts.Role.ToString()),
                    //new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        /*
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts)
        {
            return GetClaims(userAccounts);
        }
        */
        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                //Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.UserName = model.UserName;
                UserToken.Role = model.Role; 
                //UserToken.Id = model.UserName;
                //UserToken.GuidId = Id;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
