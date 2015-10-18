using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db.Map
{
    public class CategoryMap : EntityTypeConfiguration<Category>

    {
        public CategoryMap()
        {
            HasKey(c => c.Cat_Id);
            Property(c => c.Cat_Name).HasMaxLength(200).IsRequired();
            Property(c => c.Cat_Level).IsRequired();
            Property(c => c.Cat_Parent_Cat_Id).IsOptional();
            Property(c => c.IsAvailable).IsRequired();
        }
    }
}