using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models.Db.Map
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(p => p.Pr_Id);//PK

            Property(p => p.Pr_Name)//name of product
                .HasMaxLength(200)//max length
                .IsRequired()//is required to fill
                .HasColumnAnnotation(//is unique
                    IndexAnnotation.AnnotationName,new IndexAnnotation(
                        new IndexAttribute("Pr_Name_UN", 1) { IsUnique = true }));
            

            Property(p => p.Pr_Price)//price of product if
                .IsRequired();//is required to fill

            Property(p => p.Pr_IsAvailable)//show is our product is available now
                .IsRequired();//is required to fill

            Property(p => p.Pr_Count)//count of available products
                .IsOptional();//can be null

            HasRequired<Category>(p => p.Category)//one-to-many relation
                .WithMany(p => p.Products)//from category to product
                .HasForeignKey(p => p.Pr_Cat_Id);//by this FK

            HasMany<Image>(p => p.Images)//many-to-many relation
                .WithMany(p => p.Products)//from images to products
                .Map(pi =>
            {
                pi.MapLeftKey("Pr_Id");//for product
                pi.MapRightKey("Img_Id");//for image
                pi.ToTable("ProductsImages");//in this new table for our relations
            });

            HasOptional<Description>(p => p.Description)//one-to-many relation
                .WithMany(p => p.Products)//from description to products
                .HasForeignKey(p => p.Pr_Descr_Id);//by this FK

            Property(p => p.Pr_Descr_Id)
                .IsOptional();//this FK can be null


            HasMany<Cart>(p => p.Carts)//many-to-many relation
               .WithMany(p => p.Products)//from carts to products
               .Map(pc =>
               {
                   pc.MapLeftKey("Pr_Id");//for product
                   pc.MapRightKey("Cart_Id");//for cart
                   pc.ToTable("ProductsCarts");//in this new table for our relations
               });

            HasMany<Link>(p => p.Links)//many-to-many
                .WithMany(l => l.Products)//from Link to Products
                .Map(pl =>
                {
                    pl.MapLeftKey("Pr_Id");//for product
                    pl.MapRightKey("Link_Id");//for link
                    pl.ToTable("ProductsLinks");//in this new table for our relations
                });

            HasMany<Property>(prod => prod.Properties)//many-to-many
                .WithMany(prop => prop.Products)//from Property to Product
                .Map(pp => 
                {
                    pp.MapLeftKey("Pr_Id");//for product
                    pp.MapRightKey("Prop_Id");//for property
                    pp.ToTable("ProductsProperties");//in this table for our relations
                });
        }
    }
}