using OnlineShop.Models.Db.Tables;
using System.Data.Entity.ModelConfiguration;

namespace OnlineShop.Models.Db.Map
{
    public class CartMap : EntityTypeConfiguration<Cart>
    {
        public CartMap()
        {
            HasKey(c => c.Cart_Id);//PK
            Property(c => c.Cart_Count)//count of items in cart
                .IsRequired();//is required
            Property(c => c.Cart_DataCreation)//when was created
                .IsOptional();//can be null
            Property(c => c.Cart_Pr_Id)//product id for cart
                .IsRequired();//is required
            Property(c => c.User)//cart user
                .IsOptional();//can be null
        }
    }
}