using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using SSO.DBContext.SeekData;
using SSO.Helpers;
using System;
using System.IO;
using System.Linq;

namespace SSO
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                   .MinimumLevel.Override("System", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                   .Enrich.FromLogContext()
                   // uncomment to write to Azure diagnostics stream
                   .WriteTo.File(
                       Directory.GetCurrentDirectory() + @"\Log\SSO.txt",
                       fileSizeLimitBytes: 10000000,
                       rollOnFileSizeLimit: true,
                       rollingInterval: RollingInterval.Hour,
                       shared: true,
                       flushToDiskInterval: TimeSpan.FromSeconds(1),
                       outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine})")
                   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                   .CreateLogger();
            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }

                var host = CreateHostBuilder(args).Build();
                //seed = true;
                if (seed)
                {
                    Log.Information("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    IDecryptorProvider decryptor = host.Services.GetRequiredService<IDecryptorProvider>();
                    //var connectionString = config.GetConnectionString("DefaultConnection");
                    SeedData.EnsureSeedData(config, decryptor);
                    Log.Information("Done seeding database.");
                    return 0;
                }

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("https://*:5001");
                });
    }
}
