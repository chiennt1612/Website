using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.DBContext.Interfaces;
using WebAdmin.Entities;
using WebAdmin.Helpers;

namespace WebAdmin.DBContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Categories> Categoriess { get; set; }
        public DbSet<NewsCategories> NewsCategoriess { get; set; }
        public DbSet<MenuMainFooter> MenuMainFooters { get; set; }
        public DbSet<MenuSubFooter> MenuSubFooters { get; set; }
        public DbSet<ParamSetting> ParamSettings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureLogContext(builder);
        }

        private void ConfigureLogContext(ModelBuilder builder)
        {
            builder.Entity<Product>(log =>
            {
                log.ToTable(TableConsts.Product);
                log.HasKey(x => x.Id);
            });
            builder.Entity<Article>(log =>
            {
                log.ToTable(TableConsts.Article);
                log.HasKey(x => x.Id);
            });
            builder.Entity<Categories>(log =>
            {
                log.ToTable(TableConsts.Categories);
                log.Property(x => x.ParentId).IsRequired(false);
                log.HasKey(x => x.Id);
            });
            builder.Entity<NewsCategories>(log =>
            {
                log.ToTable(TableConsts.NewsCategories);
                log.Property(x => x.ParentId).IsRequired(false);
                log.HasKey(x => x.Id);
            });
            builder.Entity<MenuMainFooter>(log =>
            {
                log.ToTable(TableConsts.MenuMainFooter);
                log.HasKey(x => x.Id);
            });
            builder.Entity<MenuSubFooter>(log =>
            {
                log.ToTable(TableConsts.MenuSubFooter);
                log.HasKey(x => x.Id);
            });
            builder.Entity<ParamSetting>(log =>
            {
                log.ToTable(TableConsts.ParamSetting);
                log.HasKey(x => x.Id);
            });
        }
    }
}
