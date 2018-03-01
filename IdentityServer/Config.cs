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
                    ApiSecrets = { new Secret("$2y$10$g.rNgAOXbwWWHN3.cKqWqeVmrozhctBnhVtsuMmbrQTySrrMucUXi") },
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
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    // secret for authentication
                    ClientSecrets = {new Secret("$2y$10$g.rNgAOXbwWWHN3.cKqWqeVmrozhctBnhVtsuMmbrQTySrrMucUXi".Sha256()) },
                    // where to re-direct to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // where to re-direct to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    // scopes that client has access to
                    AllowedScopes = {StandardScopes.OpenId, StandardScopes.Profile, "api" },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    EnableLocalLogin = true,
                    RequireConsent = false,
                    AlwaysSendClientClaims = true,
                },
                // add more Clients here if needed.
            };
        }
    }
}
