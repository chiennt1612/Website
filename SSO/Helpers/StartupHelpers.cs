using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SSO.Services;
using SSO.Services.Interface;
using System;

namespace SSO.Helpers
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
        public static void AddAuthenticationServices<TIdentityDbContext, TUserIdentity, TUserIdentityRole>(this IServiceCollection services, IConfiguration configuration)
                where TIdentityDbContext : DbContext
               where TUserIdentity : class
               where TUserIdentityRole : class
        {
            var loginConfiguration = GetLoginConfiguration(configuration);
            var registrationConfiguration = GetRegistrationConfiguration(configuration);
            var identityOptions = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();

            services
                .AddSingleton(registrationConfiguration)
                .AddSingleton(loginConfiguration)
                .AddSingleton(identityOptions)
                .AddScoped<UserResolver<TUserIdentity>>()
                .AddIdentity<TUserIdentity, TUserIdentityRole>(options => configuration.GetSection(nameof(IdentityOptions)).Bind(options))
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.Secure = CookieSecurePolicy.SameAsRequest;
                options.OnAppendCookie = cookieContext =>
                    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            //var authenticationBuilder = services.AddAuthentication();

            //AddExternalProviders(authenticationBuilder, configuration);

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });
        }

        public static void AddExternalIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var g = configuration.GetSection("ExternalIdentity:Google:Authority").Value;
            if (!String.IsNullOrEmpty(g))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("Google", "Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:Google:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:Google:ClientId").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:Google:CallbackPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                });
            }

            var f = configuration.GetSection("ExternalIdentity:Facebook:Authority").Value;
            if (!String.IsNullOrEmpty(f))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("Facebook", "Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:Facebook:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:Facebook:ClientId").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:Facebook:CallbackPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                });
            }

            var t = configuration.GetSection("ExternalIdentity:Twitter:Authority").Value;
            if (!String.IsNullOrEmpty(t))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("Twitter", "Twitter", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:Twitter:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:Twitter:ClientId").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:Twitter:CallbackPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                });
            }

            var gi = configuration.GetSection("ExternalIdentity:GitHub:Authority").Value;
            if (!String.IsNullOrEmpty(gi))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("GitHub", "GitHub", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:GitHub:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:GitHub:ClientId").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:GitHub:CallbackPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                });
            }

            var s = configuration.GetSection("ExternalIdentity:StackExchange:Authority").Value;
            if (!String.IsNullOrEmpty(s))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("StackExchange", "StackExchange", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:StackExchange:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:StackExchange:ClientId").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:StackExchange:CallbackPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                });
            }

            var i = configuration.GetSection("ExternalIdentity:demoidsrv:Authority").Value;
            if (!String.IsNullOrEmpty(i))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("demoidsrv", "demoidsrv", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:demoidsrv:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:demoidsrv:ClientId").Value;

                    options.ResponseType = configuration.GetSection("ExternalIdentity:demoidsrv:ResponseType").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:demoidsrv:CallbackPath").Value;
                    options.SignedOutCallbackPath = configuration.GetSection("ExternalIdentity:demoidsrv:SignedOutCallbackPath").Value;
                    options.RemoteSignOutPath = configuration.GetSection("ExternalIdentity:demoidsrv:RemoteSignOutPath").Value;

                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            }

            var aad = configuration.GetSection("ExternalIdentity:AzureAD:Authority").Value;
            if (!String.IsNullOrEmpty(aad))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("aad", "Azure AD", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:AzureAD:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:AzureAD:ClientId").Value;

                    options.ResponseType = configuration.GetSection("ExternalIdentity:AzureAD:ResponseType").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:AzureAD:CallbackPath").Value;
                    options.SignedOutCallbackPath = configuration.GetSection("ExternalIdentity:AzureAD:SignedOutCallbackPath").Value;
                    options.RemoteSignOutPath = configuration.GetSection("ExternalIdentity:AzureAD:RemoteSignOutPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            }

            var ADFS = configuration.GetSection("ExternalIdentity:ADFS:Authority").Value;
            if (!String.IsNullOrEmpty(ADFS))
            {
                services.AddAuthentication()
                .AddOpenIdConnect("adfs", "ADFS", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = configuration.GetSection("ExternalIdentity:ADFS:Authority").Value;//"https://accounts.google.com/";
                    options.ClientId = configuration.GetSection("ExternalIdentity:ADFS:ClientId").Value;

                    options.ResponseType = configuration.GetSection("ExternalIdentity:ADFS:ResponseType").Value;

                    options.CallbackPath = configuration.GetSection("ExternalIdentity:ADFS:CallbackPath").Value;
                    options.SignedOutCallbackPath = configuration.GetSection("ExternalIdentity:ADFS:SignedOutCallbackPath").Value;
                    options.RemoteSignOutPath = configuration.GetSection("ExternalIdentity:ADFS:RemoteSignOutPath").Value;
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            }
        }

        public static IIdentityServerBuilder AddIdentityServer<TConfigurationDbContext, TPersistedGrantDbContext, TUserIdentity>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TPersistedGrantDbContext : DbContext
            where TConfigurationDbContext : DbContext
            where TUserIdentity : class
        {
            var advancedConfiguration = configuration.GetSection(nameof(AdvancedConfiguration)).Get<AdvancedConfiguration>();


            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                if (!string.IsNullOrEmpty(advancedConfiguration.IssuerUri))
                {
                    options.IssuerUri = advancedConfiguration.IssuerUri;
                }
            })
                //.AddSigningCredential(Certificates.Certificate.Get())
                //.AddUserStore<AppUserStore>()
                //.AddRoleStore<ApplicationRoleStore>()
                .AddConfigurationStore()
                .AddOperationalStore()
                .AddAspNetIdentity<TUserIdentity>()
                .AddProfileService<ProfileService>()
                ;
            ;

            //services.AddTransient<IProfileService, ProfileService>();

            //builder.AddCustomSigningCredential(configuration);
            //builder.AddCustomValidationKey(configuration);
            builder.AddSigningCredential();

            // not recommended for production - you need to store your key material somewhere secure
            //builder.AddDeveloperSigningCredential();

            return builder;
        }

        public static void AddSigningCredential(this IIdentityServerBuilder builder)
        {
            var Cert = Certificates.Certificate.Get();
            if (Cert == null) Cert = Certificates.Certificate.GetFromMachine();
            if (Cert == null)
                builder.AddDeveloperSigningCredential();
            else
                builder.AddSigningCredential(Cert);
        }

        //public static void AddAuthorizationPolicies(this IServiceCollection services)
        //{
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
        //            policy => policy.RequireRole(AuthorizationConsts.AdministrationPolicy));
        //    });
        //}

        public static void AddIdSHealthChecks<TConfigurationDbContext, TPersistedGrantDbContext, TIdentityDbContext, TDataProtectionDbContext>
            (this IServiceCollection services, string identityConnectionString, string configurationConnectionString,
            string persistedGrantConnectionString, string dataProtectionConnectionString)
            where TConfigurationDbContext : DbContext
            where TPersistedGrantDbContext : DbContext
            where TIdentityDbContext : DbContext
            where TDataProtectionDbContext : DbContext, IDataProtectionKeyContext
        {
            var healthChecksBuilder = services.AddHealthChecks()
                 .AddDbContextCheck<TConfigurationDbContext>()
                 .AddDbContextCheck<TPersistedGrantDbContext>()
                 .AddDbContextCheck<TIdentityDbContext>()
                 .AddDbContextCheck<TDataProtectionDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var configurationTableName = DbContextHelpers.GetEntityTable<TConfigurationDbContext>(scope.ServiceProvider);
                var persistedGrantTableName = DbContextHelpers.GetEntityTable<TPersistedGrantDbContext>(scope.ServiceProvider);
                var identityTableName = DbContextHelpers.GetEntityTable<TIdentityDbContext>(scope.ServiceProvider);
                var dataProtectionTableName = DbContextHelpers.GetEntityTable<TDataProtectionDbContext>(scope.ServiceProvider);

                healthChecksBuilder
                            .AddSqlServer(configurationConnectionString, name: "ConfigurationDb",
                                healthQuery: $"SELECT TOP 1 * FROM dbo.[{configurationTableName}]")
                            .AddSqlServer(persistedGrantConnectionString, name: "PersistentGrantsDb",
                                healthQuery: $"SELECT TOP 1 * FROM dbo.[{persistedGrantTableName}]")
                            .AddSqlServer(identityConnectionString, name: "IdentityDb",
                                healthQuery: $"SELECT TOP 1 * FROM dbo.[{identityTableName}]")
                            .AddSqlServer(dataProtectionConnectionString, name: "DataProtectionDb",
                                healthQuery: $"SELECT TOP 1 * FROM dbo.[{dataProtectionTableName}]");
            }
        }

        private static LoginConfiguration GetLoginConfiguration(IConfiguration configuration)
        {
            var loginConfiguration = configuration.GetSection(nameof(LoginConfiguration)).Get<LoginConfiguration>();

            // Cannot load configuration - use default configuration values
            if (loginConfiguration == null)
            {
                return new LoginConfiguration();
            }

            return loginConfiguration;
        }

        private static RegisterConfiguration GetRegistrationConfiguration(IConfiguration configuration)
        {
            var registerConfiguration = configuration.GetSection(nameof(RegisterConfiguration)).Get<RegisterConfiguration>();

            // Cannot load configuration - use default configuration values
            if (registerConfiguration == null)
            {
                return new RegisterConfiguration();
            }

            return registerConfiguration;
        }

        public static IServiceCollection AddAdminApiCors(this IServiceCollection services, UseCors adminUseCors)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        if (adminUseCors.CorsAllowAnyOrigin)
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            builder.WithOrigins(adminUseCors.CorsAllowOrigins);
                        }
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            return services;
        }
        public static void AddSenders(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfiguration = configuration.GetSection(nameof(SmtpConfiguration)).Get<SmtpConfiguration>();
            var smsConfiguration = configuration.GetSection(nameof(SMSoptions)).Get<SMSoptions>();

            if (smtpConfiguration != null && !string.IsNullOrWhiteSpace(smtpConfiguration.Host))
            {
                services.AddSingleton(smtpConfiguration);
                services.AddTransient<IEmailSender, SmtpEmailSender>();
            }
            else
            {
                services.AddSingleton<IEmailSender, EmailSender>();
            }

            if (smsConfiguration != null && !string.IsNullOrWhiteSpace(smsConfiguration.SMSAccountIdentification))
            {
                services.AddSingleton(smsConfiguration);
                services.AddTransient<ISmsSender, TwilioSmsService>();
            }
            else
            {
                services.AddSingleton<ISmsSender, SmsSender>();
            }
        }
    }
}
