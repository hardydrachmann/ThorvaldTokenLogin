using System.Collections.Generic;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    public class Config
    {
        //Returns the ApiResources, teaching the IdentityServer about the API's associated with it.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API") //"Api-Id" and "easy to read" name of the Api used for easy recognition.
                {
                    ApiSecrets = { new Secret("$2y$10$g.rNgAOXbwWWHN3.cKqWqeVmrozhctBnhVtsuMmbrQTySrrMucUXi") },
                },
               // add more API resources here if needed.
            };
        }

        //Returns IdentityResources, used to teach the IdentityServer about the UserResources available.
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        //Returns the clients, used to configure all the client and teach the IdentityServer about client permissions.
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvcClient", 
                    ClientName = "MVC Client", //Human readable name
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials, //Specifies the grant types used to generate our Id-token and Access Token
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


                    //All Token configuration settings, edit this if your token has special needs.
                    //// TOKEN EXPIRATION SETTINGS:
                    //AccessTokenLifetime = 3600,
                    //AccessTokenType = AccessTokenType.Jwt,

                    //AbsoluteRefreshTokenLifetime = 3600,
                    //RefreshTokenExpiration = TokenExpiration.Absolute,
                    //IdentityTokenLifetime = 3600,
                    //SlidingRefreshTokenLifetime = 3600,
                    //RefreshTokenUsage = TokenUsage.OneTimeOnly, // use reUse
                    //UpdateAccessTokenClaimsOnRefresh = true,
                },
                // add more Clients here if needed.
            };
        }
    }
}
