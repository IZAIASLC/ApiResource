using System;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Threading;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.Filters;

namespace ApiResource 
{
    public class TokenAuthenticationAttribute :
    Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }
        // The AuthenticateAsync and ChallengeAsync methods go here

        public System.Threading.Tasks.Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            var req = context.Request;
            
           
                var creds = req.Headers.Authorization.Parameter;
                
            return Task.FromResult(creds);
        }

        public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        public class ResultWithChallenge : IHttpActionResult
        {
            private readonly IHttpActionResult next;
            public ResultWithChallenge(IHttpActionResult next)
            {
                this.next = next;
            }
            public async Task<HttpResponseMessage> ExecuteAsync(
              CancellationToken cancellationToken)
            {
                var response = await next.ExecuteAsync(cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(
                      new AuthenticationHeaderValue("somescheme", "somechallenge"));
                }
                return response;
            }


        }
    }
}