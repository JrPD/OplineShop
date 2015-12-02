using OnlineShop.Models.Db.Tables;
using System.Data.Entity.ModelConfiguration;

namespace OnlineShop.Models.Db.Map
{
    public class DescriptionMap : EntityTypeConfiguration<Description>
    {
        public DescriptionMap()
        {
            HasKey(d => d.Desc_Id);//PK
            Property(d => d.Desc_Path)//path for local description in file
                .IsRequired();//is required
        }
    }
}