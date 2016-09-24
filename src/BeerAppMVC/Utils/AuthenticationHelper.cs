using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppMVC.Utils
{
    public static class AuthenticationHelper
    {
        public static async Task<string> GetAuthTokenAsync(this HttpContext ctx, string audience)
        {
            string userObjectID = (ctx.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;
            AuthenticationContext authContext = new AuthenticationContext(Startup.Authority, new NaiveSessionCache(userObjectID, ctx.Session));
            ClientCredential credential = new ClientCredential(Startup.ClientId, Startup.ClientSecret);
            var result = await authContext.AcquireTokenSilentAsync(audience, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
            return result.AccessToken;
        }
    }
}
