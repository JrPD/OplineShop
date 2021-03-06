﻿using OnlineShop.Models.Db.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace OnlineShop.Models.Db.Map
{
    public class CategoryMap : EntityTypeConfiguration<Category>

    {
        public CategoryMap()
        {
            HasKey(c => c.Cat_Id);//PK
            Property(c => c.Cat_Name)//Name of category
                .HasMaxLength(200)//max length
                .IsRequired()//is required
                .HasColumnAnnotation(//is unique field
                    IndexAnnotation.AnnotationName, new IndexAnnotation(
                        new IndexAttribute("Cat_Name_UN", 1) { IsUnique = true }));
            Property(c => c.Cat_Level)//category level can be from 1(top category) to 5(end subcategory)
                .IsRequired();//is required
            Property(c => c.Cat_Parent_Cat_Id)//parent id if level not 1
                .IsRequired();//is required
            Property(c => c.Cat_HasChild)//is any child
                .IsRequired();//is required
            HasOptional<Image>(c => c.Image)//one-to-many relation
               .WithMany(i => i.Categories)//from description to products
               .HasForeignKey(i => i.Cat_Img_Id);//by this FK
            Property(c => c.Cat_Img_Id)// FK for Image.Img_Id
                .IsOptional(); //can be null
            HasMany<Link>(p => p.Links)//many-to-many
               .WithMany(l => l.Categories)//from Link to Products
               .Map(pl =>
               {
                   pl.MapLeftKey("Cat_Id");//for product
                   pl.MapRightKey("Link_Id");//for link
                   pl.ToTable("CategoriesLinks");//in this new table for our relations
               });
        }
    }
}