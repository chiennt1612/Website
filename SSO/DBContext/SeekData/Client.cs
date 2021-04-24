﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SSO.DBContext.SeekData
{
    public static partial class Config
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc1",
                    ClientSecrets = { new Secret("secretchatapi".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5006/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5006/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api2"
                    }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "ID_API",
                    ClientSecrets = { new Secret("secret_ID_API".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5008/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5008/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api2"
                    }
                },
                
                //// interactive ASP.NET chat MVC client
                //new Client
                //{
                //    ClientId = "chat.api",
                //    ClientSecrets = { new Secret("secret.chat.api".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,
                    
                //    // where to redirect to after login
                //    RedirectUris = { "https://localhost:5006/signin-oidc" },

                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "https://localhost:5006/signout-callback-oidc" },

                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        IdentityServerConstants.StandardScopes.Address,
                //        IdentityServerConstants.StandardScopes.Phone,
                //        IdentityServerConstants.StandardScopes.OfflineAccess,
                //        "api2"
                //    }
                //}
            };
    }
}
