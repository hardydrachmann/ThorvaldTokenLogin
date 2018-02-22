using System.Collections.Generic;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                },
               // add more API resources here if needed.
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvcClient",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    EnableLocalLogin = true,
                    RequireConsent = false,
                    // where to re-direct to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // where to re-direct to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string> {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "api",
                    },
                    AlwaysSendClientClaims = true,
                    ClientSecrets = new List<Secret>() {new Secret("secret".Sha256()) }
                },
                // add more Clients here if needed.
            };
        }
    }
}
