using System.Data.Entity;
using OnlineShop.Models.Db.Map;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db
{

    public class AppContext : DbContext
    {

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Description> Descriptions { get; set; }


        public AppContext()
            : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder
            modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new DescriptionMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new CartMap());
        }
    }


}