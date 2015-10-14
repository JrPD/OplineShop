using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Map
{
    public class DescriptionMap : EntityTypeConfiguration<Description>
    {
        public DescriptionMap()
        {
            HasKey(d => d.Desc_Id);
            Property(d => d.Desc_Path).IsRequired();
        }

    }
}