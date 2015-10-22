using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db.Map
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(p => p.Pr_Id);

            Property(p => p.Pr_Name).HasMaxLength(200).IsRequired();

            Property(p => p.Pr_Price).IsRequired();

            Property(p => p.Pr_IsAviable).IsRequired();

            Property(p => p.Pr_Count).IsOptional();

            HasRequired<Category>(p => p.Category).WithMany(p => p.Products)
                .HasForeignKey(p => p.Pr_Cat_Id);

            HasMany<Image>(p => p.Images).WithMany(p => p.Products)
                .Map(pi =>
            {
                pi.MapLeftKey("Pr_Id");
                pi.MapRightKey("Img_Id");
                pi.ToTable("ProductsImages");
            });

            HasOptional<Description>(p => p.Description)
                .WithMany(p => p.Products).HasForeignKey(p => p.Pr_Descr_Id);

            Property(p => p.Pr_Descr_Id).IsOptional();


            HasMany<Cart>(p => p.Carts).WithMany(p => p.Products)
               .Map(pc =>
               {
                   pc.MapLeftKey("Pr_Id");
                   pc.MapRightKey("Cart_Id");
                   pc.ToTable("ProductsCarts");
               });
        }
    }
}