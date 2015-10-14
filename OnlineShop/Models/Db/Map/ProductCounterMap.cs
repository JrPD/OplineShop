using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Map
{
    public class ProductCounterMap : EntityTypeConfiguration<ProductCounter>
    {
        public ProductCounterMap()
        {
            HasKey(pc=>pc.Pr_Id);
            Property(pc=>pc.Pr_Count).IsRequired();
        }

    }
}