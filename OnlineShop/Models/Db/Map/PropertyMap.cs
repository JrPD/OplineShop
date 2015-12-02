using OnlineShop.Models.Db.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace OnlineShop.Models.Db.Map
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            HasKey(p => p.Prop_Id);//PK
            Property(p => p.Prop_Name)//name or value of property
                .IsRequired()//is required
                .HasMaxLength(200)//max length of field
                .HasColumnAnnotation(//is unique
                    IndexAnnotation.AnnotationName, new IndexAnnotation(
                        new IndexAttribute("Prop_Name_UN", 1) { IsUnique = true }));
            HasRequired<Link>(p => p.Link)//one-to-name relation
                .WithMany(l => l.Properties)//from links to properties
                .HasForeignKey(p => p.Prop_Link_Id);//by this FK
        }
    }
}