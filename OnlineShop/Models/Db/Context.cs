using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{

    public class Context : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Description> Descriptions { get; set; }
            

        public Context()
            : base("DefaultConnection")
        {
        }
        //protected override void OnModelCreating(DbModelBuilder
        //	modelBuilder)
        //{
        //	modelBuilder.Configurations.Add(new ProductMap());
        //	modelBuilder.Configurations.Add(new CategoryMap());
        //}
    }


}