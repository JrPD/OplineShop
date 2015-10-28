using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.Db.Map
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            HasKey(p=>p.Prop_Id);//PK
            Property(p=>p.Prop_Name)//name or value of property
                .IsRequired();//is required
            HasRequired<Link>(p => p.Link)//one-to-name relation
                .WithMany(l => l.Properties)//from links to properties
                .HasForeignKey(p => p.Prop_L_Id);//by this FK
        }

    }
}