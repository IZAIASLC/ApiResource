using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;

namespace ApiResource 
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOauth(app);
            Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }
        public void ConfigureOauth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                
            });
        }

        public void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}