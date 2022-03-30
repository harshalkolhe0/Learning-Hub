using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningHubApi2
{
    public class TokenManager
    {
        public static string secret = "64A6315311C149199133EFAF99A9B456";
        //public static string user;
        //public static string getUser()
        //{
        //    return user;
        //}
        public static string GenerateToken(string un,string role)
        {
            byte[] key = Convert.FromBase64String(secret);
            SymmetricSecurityKey securityKey = new  SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims: new[] 
                { 
                    new Claim(type: ClaimTypes.Name, value: un) ,
                    new Claim(type: ClaimTypes.Role, value:role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                                        algorithm: SecurityAlgorithms.HmacSha256)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            string s = handler.WriteToken(token);
            return s;
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    JwtSecurityToken jwttoken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                    if (jwttoken == null) return null;
                }
                catch(Exception exp)
                {
                    string s=exp.Message;

                }
                byte[] key = Convert.FromBase64String(secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,//later true
                    ValidateAudience = false,//later true
                    //ValidIssuer = "https://localhost:7203/api",
                    //ValidAudience = "https://localhost:7178/",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                    parameters,out securityToken);
                return principal;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static string ValidateToken(String token)
        {
            string un = "";
            ClaimsPrincipal principal=GetPrincipal(token);
            if(principal == null)return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch(Exception exp)
            {
                return null;
            }
            Claim userclaim=identity.FindFirst(type:ClaimTypes.Name);
            un = userclaim.Value;
            //user = un;
            return un;
        }
    }
}
