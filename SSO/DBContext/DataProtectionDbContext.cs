using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SSO.DBContext
{
    public class DataProtectionDbContext : DbContext, IDataProtectionKeyContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
            : base(options) { }
    }
}
