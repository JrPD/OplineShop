using System.Collections.Generic;

namespace OnlineShop.Models.Db.Tables
{
    public class Image
    {
        public long Img_Id { get; set; }

        public string Img_Path { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public Image()
        {
            Products = new HashSet<Product>();
            Categories = new HashSet<Category>();
        }
    }
}