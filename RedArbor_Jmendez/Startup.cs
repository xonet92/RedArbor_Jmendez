using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RedArbor_Jmendez.Infraestructure;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(RedArbor_Jmendez.Startup))]

namespace RedArbor_Jmendez
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigureOauth(app);
            app.UseWebApi(config);
        }

        public void ConfigureOauth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions opcionesautorizacion =
               new OAuthAuthorizationServerOptions()
               {
                   AllowInsecureHttp = true,
                   TokenEndpointPath = new PathString("/GetToken"),
                   AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                   Provider = new CredentialsToken()
               };
            app.UseOAuthAuthorizationServer(opcionesautorizacion);
            OAuthBearerAuthenticationOptions opcionesoauth =
                new OAuthBearerAuthenticationOptions()
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
                };

            app.UseOAuthBearerAuthentication(opcionesoauth);
        }


    }
}
