using EntityFramework.Web.DBContext;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using WebAdmin.Helpers;

namespace WebAdmin
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
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddHttpClient();

            services.AddServiceLanguage();
            services.AddAuthenticationServices();
            services.AddAuthorizationByPolicy();

            services.AddTransient<IDecryptorProvider, DecryptorProvider>();
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.RegisterDbContexts(Configuration, migrationsAssembly);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegisterAppServices();

            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.Use(async (context, next) =>
            // {
            //     await next();
            //     if (context.Response.StatusCode == 404)
            //     {
            //         context.Request.Path = "/Home/Error404";
            //         await next();
            //     }
            //     else if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 599)
            //     {
            //         context.Request.Path = "/Home/Error";
            //         await next();
            //     }
            // });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseConfigLanguage();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }).RequireAuthorization();
            });
        }
    }
}
