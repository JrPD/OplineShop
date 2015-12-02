using OnlineShop.Models.Db.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace OnlineShop.Models.Db.Map
{
    public class LinkMap : EntityTypeConfiguration<Link>
    {
        public LinkMap()
        {
            HasKey(l => l.Link_Id);//PK
            Property(i => i.Link_Name)//name for view of link
                .IsRequired()//is required
                .HasMaxLength(200)//max length of field
                .HasColumnAnnotation(//is unique
                    IndexAnnotation.AnnotationName, new IndexAnnotation(
                        new IndexAttribute("Link_Name_UN", 1) { IsUnique = true }));
        }
    }
}