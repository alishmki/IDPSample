// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),


                new IdentityResource(
                    name: "country",
                    displayName: "The country you're living in",
                    claimTypes: new List<string> { "country" }),

                new IdentityResource(
                    name:"customprofile",
                    displayName:"Custom Profile",
                    claimTypes:new string[]{ "name","family" })
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                 // simple API with a single scope (in this case the scope name is the same as the api name)
                // new ApiResource("api1","api1des")
                new ApiResource()
                {
                    Name="api1",
                    Description="des",
                    UserClaims=new List<string>{"country" },
                    DisplayName="displaname",
                   Scopes=
                   {
                       new Scope()
                       {
                           Name="api1",
                           DisplayName="api1",

                       },
                        new Scope()
                       {
                           Name="api1.country",
                           DisplayName="countryyy",

                       }
                   }
                }




            };

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
                    RequireConsent = false,
                    RequirePkce = true,
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "country"
                    },



                    UpdateAccessTokenClaimsOnRefresh = true,





                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent=false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "country",
                        "api1"
                    },

                    AllowOfflineAccess = true,

                    IdentityTokenLifetime = 30,// ... // defaults to 300 seconds / 5 minutes
                    AuthorizationCodeLifetime = 30,// ... // defaults to 300 seconds / 5 minutes
                    AccessTokenLifetime = 60, // defaults to 3600 seconds / 1 hour
                }
            };
    }
}