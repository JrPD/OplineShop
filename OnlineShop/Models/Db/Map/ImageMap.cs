using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db.Map
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            HasKey(i => i.Img_Id);
            Property(i => i.Img_Path).IsRequired();
        }

    }
}