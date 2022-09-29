using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RedArbor_Jmendez.Infraestructure
{
    public class CredentialsToken: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.Password));

            string user = ConfigurationManager.AppSettings["APIUSER"];
            string password = ConfigurationManager.AppSettings["APIPASSWORD"];
            if ((context.UserName == user && context.Password == password))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "API"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("User or password is not correct.");
            }              
        }
    }
}