using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.DBContext
{
    public class DataProtectionDbContext : DbContext, IDataProtectionKeyContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DataProtectionDbContext(DbContextOptions<DataProtectionDbContext> options)
            : base(options) { }
    }
}
