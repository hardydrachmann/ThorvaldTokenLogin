using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.DAL
{
    public class TokenManager
    {
        private HttpClient client;

        public TokenManager()
        {
            // discover endpoint (ThorvaldLogin - IdentityServer4)
            var discoveryClientResponse = DiscoveryClient.GetAsync("http://localhost:5000").Result;

            if (discoveryClientResponse.IsError) { }
            else
            {
                // request token
                var tokenClient = new TokenClient(discoveryClientResponse.TokenEndpoint, "mvcClient", "secret");
                var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("hd", "hd", "api").Result;

                if (tokenResponse.IsError) { }
                else
                {
                    // call api (Api - IdentityServer4.AccessTokenValidation)
                    client = new HttpClient();
                    client.SetBearerToken(tokenResponse.AccessToken);
                }
            }
        }

        public HttpClient GetTokenManager()
        {
            return client;
        }
    }
}
