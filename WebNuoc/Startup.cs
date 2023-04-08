using Decryptor;
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
using WebNuoc.Helpers;
using WebNuoc.Services;

namespace WebNuoc
{
    public class Startup
    {
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds services required for using options.
            services.AddOptions();
            var _LoggingProvider = Configuration.GetSection("LoggingProvider");
            // Register the IConfiguration instance
            services.Configure<LoggingProvider>(_LoggingProvider);

            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddHttpClient();
            services.AddServiceLanguage();
            services.AddAuthenticationServices(Configuration);
            services.AddAuthorizationByPolicy();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDecryptorProvider, DecryptorProvider>();
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.RegisterDbContexts(Configuration, migrationsAssembly);

            //// Paygate config
            //var identitySettingsSection = Configuration.GetSection("PaygateInfo");
            //services.Configure<PaygateInfo>(identitySettingsSection);
            //services.AddSingleton<IPaygateInfo, PaygateInfo>();
            //services.RegisterPaygateService(Configuration);

            services.AddSenders(Configuration);
            services.RegisterAppServices();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      builder =>
            //                      {
            //                      builder.WithOrigins("https://chatngay-api.glee.vn");// Configuration.GetSection("Origins").Get<string[]>());
            //                      });
            //});

            services.AddResponseCaching();

            services.AddDistributedMemoryCache();
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = Configuration.GetConnectionString(
            //        "DistCache_ConnectionString");
            //    options.SchemaName = "dbo";
            //    options.TableName = "WebCache";
            //}); 
            //#region snippet1
            services.AddHostedService<TimerBackgroundServive>();
            //#endregion
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
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Request.Path = "/Home/Error404";
            //        await next();
            //    }
            //    else if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 599)
            //    {
            //        context.Request.Path = "/Home/Error";
            //        await next();
            //    }
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //add for jwt
            app.UseCookiePolicy();
            app.UseSession();
            //Add JWToken to all incoming HTTP Request Header
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseConfigLanguage();

            app.UseRouting();

            //app.UseCors(MyAllowSpecificOrigins);

            app.UseResponseCaching();

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
                });
            });
        }
    }
}
