using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db.Map
{
    public class CartMap : EntityTypeConfiguration<Cart>
    {
        public CartMap()
        {
            HasKey(c => c.Cart_Id);
            Property(c => c.Cart_Count).IsRequired();
            Property(c => c.Cart_DataCreation).IsOptional();
            Property(c => c.Cart_Pr_Id).IsOptional();
            Property(c => c.User).IsOptional();
        }

    }
}