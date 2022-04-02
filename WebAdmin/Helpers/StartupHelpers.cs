using EntityFramework.Web.DBContext;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using WebAdmin.Repository;
using WebAdmin.Repository.Interfaces;
using WebAdmin.Services;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Helpers
{
    public static class StartupHelpers
    {
        #region Localization
        private static string[] LanguageSupport = new[] { "vi" };//, "en-US"
        public static void AddServiceLanguage(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(LanguageSupport[0])
                    .AddSupportedCultures(LanguageSupport)
                    .AddSupportedUICultures(LanguageSupport);
            });
        }

        public static void UseConfigLanguage(this IApplicationBuilder app)
        {
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(LanguageSupport[0])
                .AddSupportedCultures(LanguageSupport)
                .AddSupportedUICultures(LanguageSupport);

            app.UseRequestLocalization(localizationOptions);
        }
        #endregion

        #region Authorization Policy
        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddSingleton<IDiscoveryCache>(r =>
            {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(Constants.Authority, () => factory.CreateClient());
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "WebAdmin";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Constants.Authority;
                    options.ClientId = "WebAdmin";
                    options.ClientSecret = "secretWebAdmin";
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.RequireHttpsMetadata = false;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    //options.Scope.Add("address");
                    //options.Scope.Add("phone");

                    // not mapped by default
                    options.ClaimActions.MapJsonKey("role", "role", "role");

                    // keeps id_token smaller
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };
                });
        }

        public static void AddAuthorizationByPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("role", "Admin"));
                options.AddPolicy("ModPolicy", policy => policy.RequireClaim("role", "Mod", "Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireClaim("role", "User", "Mod", "Admin"));
            });
        }
        #endregion

        #region DbContext
        public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration, string migrationsAssembly)
        {
            // Build the intermediate service provider
            var sp = services.BuildServiceProvider();

            // This will succeed.
            var decryptor = sp.GetService<IDecryptorProvider>();
            services.RegisterDbContexts<AppDbContext>(configuration, decryptor, migrationsAssembly);
            //services.RegisterDbContexts<OrderDbContext>(configuration, decryptor);

            services.AddIdSHealthChecks<AppDbContext>(configuration, decryptor);
            //services.AddIdSHealthChecks<OrderDbContext>(configuration, decryptor);
        }
        #endregion

        #region Bussiness Services
        public static void RegisterAppServices(this IServiceCollection services)
        {
            // Services business
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleServices, ArticleServices>();
            services.AddScoped<ICategoriesServices, CategoriesServices>();
            services.AddScoped<IMenuMainFooterServices, MenuMainFooterServices>();
            services.AddScoped<IMenuSubFooterServices, MenuSubFooterServices>();
            services.AddScoped<INewsCategoriesServices, NewsCategoriesServices>();
            services.AddScoped<IParamSettingServices, ParamSettingServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IAboutServices, AboutServices>();
            services.AddScoped<IAdvServices, AdvServices>();
            services.AddScoped<IServiceServices, ServiceServices>();
            services.AddScoped<IFAQServices, FAQServices>();
        }
        #endregion
    }
}
