using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client.DAL
{
    public class TokenManager
    {
        private HttpClient client;

        public TokenManager()
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (disco.IsError)
            { return; }
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "mvcClient", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;

            if (tokenResponse.IsError)
            { return; }

            // call api
            client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = client.GetAsync("http://localhost:5001").Result;
            if (!response.IsSuccessStatusCode)
            { }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }

        public HttpClient GetTokenManager()
        {
            return client;
        }
    }
}
