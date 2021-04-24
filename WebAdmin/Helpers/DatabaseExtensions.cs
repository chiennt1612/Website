using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAdmin.Helpers
{
    public static class DatabaseExtensions
    {
        public static void RegisterDbContexts<TDbContext>
                   (this IServiceCollection services, IConfiguration configuration, IDecryptorProvider decryptor)
               where TDbContext : DbContext
        {
            string connectionString = decryptor.Decrypt(configuration.GetConnectionString("DefaultConnection"));
            //string connectionString = decryptor.Decrypt(Environment.GetEnvironmentVariable("CONNECTION_STRING"));

            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;

            // Config DB for identity
            services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
