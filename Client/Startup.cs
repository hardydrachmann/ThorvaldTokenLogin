using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // add authentication services (using cookies)
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc"; // oidc = open id connect
            })
            // add cookies to handler
                .AddCookie("Cookies")
                // configure open id connect handler
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = "http://localhost:5000"; // trust identity server (port 5000 = our server)
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvcClient";
                    options.ClientSecret = "$2y$10$g.rNgAOXbwWWHN3.cKqWqeVmrozhctBnhVtsuMmbrQTySrrMucUXi";
                    options.ResponseType = "code id_token";


                    options.SaveTokens = true; // allow saving tokens in cookies
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("api");
                    options.Scope.Add("offline_access");

                    options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name", RoleClaimType = "role" };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
