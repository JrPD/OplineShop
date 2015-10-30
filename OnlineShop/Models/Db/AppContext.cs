using System.Data.Entity;
using OnlineShop.Models.Db.Map;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db
{
    /// <summary>
    /// Context for connect to DB by EF
    /// </summary>
    public class AppContext : DbContext
    {

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Description> Descriptions { get; set; }


        public AppContext()//our default context
            : base("DataBaseConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder
            modelBuilder)
        {//adding here our custom maps for EF
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new DescriptionMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new CartMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new LinkMap());
        }
    }
}