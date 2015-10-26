using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;
using System.ComponentModel.DataAnnotations.Schema;

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
                .IsOptional();//can be null
            Property(c => c.IsAvailable)//is available cat
                .IsRequired();//is required
        }
    }
}