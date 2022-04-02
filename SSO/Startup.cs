using HealthChecks.UI.Client;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSO.DBContext;
using SSO.Entities;
using SSO.Extensions;
using SSO.Helpers;
using SSO.Services;
using SSO.Services.Interface;
using System.Reflection;

namespace SSO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllersWithViews();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }
        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllerRoute(
        //            name: "default",
        //            pattern: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
        public void ConfigureServices(IServiceCollection services)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //string connectionString = DecryptorProvider.Decrypt(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var adminUseCors = Configuration.GetSection(nameof(UseCors)).Get<UseCors>();
            if (adminUseCors == null)
            {
                adminUseCors = new UseCors() { CorsAllowAnyOrigin = true };
            }
            services.AddControllersWithViews();

            services.Configure<SMSoptions>(Configuration);
            services.Configure<SmtpConfiguration>(Configuration);

            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddTransient<ISmsSender, TwilioSmsService>();
            services.AddTransient<IDecryptorProvider, DecryptorProvider>();

            services.AddSenders(Configuration);

            services.AddServiceLanguage();

            // Register DbContexts for IdentityServer and Identity
            RegisterDbContexts(services);

            RegisterAuthentication(services);

            // Add authorization policies for MVC
            //services.AddAuthorizationPolicies();

            services.AddAdminApiCors(adminUseCors);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseConfigLanguage();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseCors();
            //app.Use(async (context, next) =>
            //{
            //    if (!context.User.Identity.IsAuthenticated)
            //    {
            //        context.Response.Redirect("/Account/Login");
            //    }
            //    else
            //    {
            //        await next.Invoke();
            //    }
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        #region helper
        public virtual void RegisterDbContexts(IServiceCollection services)
        {
            // Build the intermediate service provider
            var sp = services.BuildServiceProvider();

            // This will succeed.
            var decryptor = sp.GetService<IDecryptorProvider>();
            services.RegisterDbContexts<AppDbContext, DataProtectionDbContext>(Configuration, decryptor);
            services.AddIdSHealthChecks<ConfigurationDbContext, PersistedGrantDbContext, AppDbContext, DataProtectionDbContext>(Configuration, decryptor);
        }

        public virtual void RegisterAuthentication(IServiceCollection services)
        {
            services.AddAuthenticationServices<AppDbContext, AppUser, AppRole>(Configuration);
            services.AddIdentityServer<ConfigurationDbContext, PersistedGrantDbContext, AppUser>(Configuration);
            services.AddExternalIdentityServices(Configuration);
        }
        #endregion
    }
}
