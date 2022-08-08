using EntityFramework.Web.Entities;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Web.DBContext.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> Status { get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Categories> Categoriess { get; set; }
        public DbSet<NewsCategories> NewsCategoriess { get; set; }
        public DbSet<MenuMainFooter> MenuMainFooters { get; set; }
        public DbSet<MenuSubFooter> MenuSubFooters { get; set; }
        public DbSet<ParamSetting> ParamSettings { get; set; }
        public DbSet<InvoiceSave> InvoiceSaves { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<AdvPosition> AdvPositions { get; set; }
    }
}
