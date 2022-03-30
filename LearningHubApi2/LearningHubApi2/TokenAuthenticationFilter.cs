using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;


namespace LearningHubApi2
{
    public class TokenAuthenticationFilter : Attribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            if(!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                result = false;
            }
            
            string tokenanduser=string.Empty;
            if(result)
            {
                tokenanduser = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                string[] p = tokenanduser.Split(" ");
                tokenanduser = p[1];
                string[] parts = tokenanduser.Split(":");
                string token = parts[0];
                string user = parts[1];
                string userintoken = TokenManager.ValidateToken(token);
                if(user!=userintoken)
                {
                    result=false;
                }
            }
            if(!result)
            {
                context.ModelState.AddModelError("Unauthorized","You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
