using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Entities;

namespace WebAdmin.DBContext.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Categories> Categoriess { get; set; }
        public DbSet<NewsCategories> NewsCategoriess { get; set; }
        public DbSet<MenuMainFooter> MenuMainFooters { get; set; }
        public DbSet<MenuSubFooter> MenuSubFooters { get; set; }
        public DbSet<ParamSetting> ParamSettings { get; set; }
    }
}
