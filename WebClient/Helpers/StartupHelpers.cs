using EntityFramework.Web.DBContext;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using WebClient.Repository;
using WebClient.Repository.Interfaces;
using WebClient.Services;
using WebClient.Services.Interfaces;

namespace WebClient.Helpers
{
    public static class StartupHelpers
    {
        #region Localization
        private static string[] LanguageSupport = new[] { "vi" }; //, "en-US"
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
                    options.Cookie.Name = "WebClient";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Constants.Authority;
                    options.ClientId = "WebClient";
                    options.ClientSecret = "secretWebAdmin";
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.RequireHttpsMetadata = false;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("address");
                    options.Scope.Add("phone");

                    // not mapped by default
                    options.ClaimActions.MapJsonKey("role", "role", "role");
                    options.ClaimActions.MapJsonKey("phone_number", "phone_number", "phone_number");
                    options.ClaimActions.MapJsonKey("address", "address", "address");
                    options.ClaimActions.MapJsonKey("website", "website", "website");
                    options.ClaimActions.MapJsonKey("username", "username");

                    // keeps id_token smaller
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    NameClaimType = JwtClaimTypes.Name,
                    //    RoleClaimType = JwtClaimTypes.Role,
                    //};
                });
        }

        public static void AddAuthorizationByPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomerPolicy", policy => policy.RequireClaim("Customer"));
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
            services.AddIdSHealthChecks<AppDbContext>(configuration, decryptor);
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
            services.AddScoped<IContactServices, ContactServices>();
            services.AddScoped<IAdvServices, AdvServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IAllService, AllService>();
        }

        public static void AddSenders(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfiguration = configuration.GetSection(nameof(SmtpConfiguration)).Get<SmtpConfiguration>();

            if (smtpConfiguration != null && !string.IsNullOrWhiteSpace(smtpConfiguration.Host))
            {
                services.AddSingleton(smtpConfiguration);
                services.AddSingleton<IEmailSender, SmtpEmailSender>();
            }
            else
            {
                services.AddSingleton<IEmailSender, EmailSender>();
            }
        }
        #endregion
    }
}
