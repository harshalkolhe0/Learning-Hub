using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace LearningHubApi2
{
    public class CustomAuthenticationFilter2 : AuthorizeAttribute, IAuthenticationFilter
    {
        
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = "";
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            if(authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Authorization Header", request);
                return;
            }
            if(authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Invalid Authorization Schema", request);
                return;
            }
            string[] tokenanduser = authorization.Parameter.Split(":");
            string token = tokenanduser[0];
            string user = tokenanduser[1];

            if (String.IsNullOrEmpty(token))
            {
                context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Token", request);
                return;
            }
            string validun = TokenManager.ValidateToken(token);
            if(validun != user)
            {
                context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Invalid token for user", request);
                return;
            }
            context.Principal = TokenManager.GetPrincipal(token);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(scheme: "Basic", parameter: "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
        
    }
    
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }
        public AuthenticationFailureResult(string reasonPhrase,HttpRequestMessage req)
        {
            ReasonPhrase = reasonPhrase;
            Request = req;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhrase;
            return responseMessage;
        }
    }
    
}
