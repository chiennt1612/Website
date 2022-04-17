using Decryptor;
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
using Paygate.OnePay;
using System.Net.Http;
using WebNuoc.Repository;
using WebNuoc.Repository.Interfaces;
using WebNuoc.Services;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Helpers
{
    public static class StartupHelpers
    {
        #region Localization
        private static string[] LanguageSupport = new[] { "vi", "en-US" };
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
                    options.Cookie.Name = "WebNuoc";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Constants.Authority;
                    options.ClientId = "WebNuoc";
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
                    options.ClaimActions.MapJsonKey("role", "role");
                    options.ClaimActions.MapJsonKey("phone_number", "phone_number");
                    options.ClaimActions.MapJsonKey("address", "address");
                    options.ClaimActions.MapJsonKey("website", "website");
                    options.ClaimActions.MapJsonKey("oldid", "oldid");
                    options.ClaimActions.MapJsonKey("username", "username");

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
        public static void RegisterPaygateService(this IServiceCollection services, IConfiguration configuration)
        {
            // Paygate config
            var paygateInfo = configuration.GetSection(nameof(PaygateInfo)).Get<PaygateInfo>();

            if (paygateInfo == null)
            {
                paygateInfo = new PaygateInfo();
            }

            services.AddSingleton(paygateInfo);
            services.AddSingleton<IVPCRequest, VPCRequest>();
        }
        public static void RegisterAppServices(this IServiceCollection services)
        {
            // Services business
            //services.AddSingleton<IArticleRepository, ArticleRepository>();
            //services.AddSingleton<ICategoriesRepository, CategoriesRepository>();
            //services.AddSingleton<IMenuMainFooterRepository, MenuMainFooterRepository>();
            //services.AddSingleton<IMenuSubFooterRepository, MenuSubFooterRepository>();
            //services.AddSingleton<INewsCategoriesRepository, NewsCategoriesRepository>();
            //services.AddSingleton<IParamSettingRepository, ParamSettingRepository>();
            //services.AddSingleton<IProductRepository, ProductRepository>();
            //services.AddSingleton<IAboutRepository, AboutRepository>();
            //services.AddSingleton<IContactRepository, ContactRepository>();
            //services.AddSingleton<IAdvRepository, AdvRepository>();
            //services.AddSingleton<IServiceRepository, ServiceRepository>();
            //services.AddSingleton<IFAQRepository, FAQRepository>();
            //services.AddSingleton<IOrderStatusRepository, OrderStatusRepository>();

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
            services.AddScoped<IServiceServices, ServiceServices>();
            services.AddScoped<IFAQServices, FAQServices>();
            services.AddScoped<IAllService, AllService>();
            services.AddScoped<IInvoiceServices, InvoiceServices>();
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
