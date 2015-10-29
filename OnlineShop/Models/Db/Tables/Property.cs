using System.Collections.Generic;

namespace OnlineShop.Models.Db.Tables
{
    public class Property
    {
        public long Prop_Id { get; set; }

        public string Prop_Name { get; set; }

        public long Prop_Link_Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual Link Link { get; set; }

        public Property()
        {
            Products = new HashSet<Product>();
            Link = new Link();
        }
    }
}