using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace ApiResource.Controllers
{
    [Authorize]
    [RoutePrefix("api/protected")] 
    public class ProtectedController : ApiController
    {
        [Route("")]
        public string Get()
        {

            var bearer = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault().Substring(0, 7);

            var token = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault().Replace(bearer,"");

            return token;
        }
    }
}