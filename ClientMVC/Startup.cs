using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClientMVC
{
    public class Startup
    {
        public Startup()
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.ClaimActions.Remove("amr");
                    options.ClaimActions.DeleteClaim("sid");
                    options.ClaimActions.DeleteClaim("idp");

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("country");
                    //options.Scope.Add("offline_access");

                    options.ClaimActions.MapUniqueJsonKey(claimType: "country", jsonKey: "country");
                 
                    //options.ClaimActions.DeleteClaim("country");

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
