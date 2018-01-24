using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        private static async Task MainAsync()
        {
            // discover endpoint
            var discoveryClientResponse = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (discoveryClientResponse.IsError)
            {
                Console.WriteLine("'ThorvaldLogin' response error: " + discoveryClientResponse.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(discoveryClientResponse.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");

            if (tokenResponse.IsError)
            {
                Console.WriteLine("Tokenresponse error: " + tokenResponse.Error);
                return;
            }
            Console.WriteLine("Tokenresponse: " + tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/identity");

            if (!response.IsSuccessStatusCode)
                Console.WriteLine("'api' response error: " + response.StatusCode);
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("'api' response success: " + JArray.Parse(content));
            }
        }
    }
}
