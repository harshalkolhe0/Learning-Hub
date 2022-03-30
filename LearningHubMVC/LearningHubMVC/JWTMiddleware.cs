using LearningHubMVC.ServiceRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    public async Task Invoke(HttpContext context)
    {
        //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        //var token = context.Request.Headers.Authorization.FirstOrDefault()?.ToString();

        var token = context.Request.Headers["Authorization"].FirstOrDefault();//["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImhrIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImhrIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2V4cGlyYXRpb24iOiJKYW4gU2F0IDI5IDIwMjIgMTQ6MzI6NTAgUE0iLCJuYmYiOjE2NDMzODAzNzEsImV4cCI6MTY0MzQ0Njk3MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIwMy9hcGkiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTc4LyJ9.8CSQSn1EEMyVLD5PsOAr4OYMGofZ9zW0XkBjvCDPupU";
        if (token != null)
        {
            attachAccountToContext(context, token);
        }

        await _next(context);
    }
    private void attachAccountToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:ValidIssuer"],
                ValidAudience = _configuration["Jwt:ValidAudience"],
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var Id = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //get the user name and role from the JWT token.
            var username = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var role = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            
            
            //var userClaims = new List<Claim>()
            // {
            //     new Claim("UserName", username),
            //     new Claim("Role", role)
            //  };
            //var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
            //var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

            // attach account to context on successful jwt validation 
            //var user = new MVCWebApplication.Data.User();
            //user.UserName = "aa@hotmail.com";
            //context.Items["User"] = user;
            // context.SignInAsync(userPrincipal);
        }
        catch (Exception ex)
        {
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
            throw new Exception(ex.Message);
        }
    }
}