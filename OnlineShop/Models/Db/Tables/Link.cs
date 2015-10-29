using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{
    public class Link
    {
        public long Link_Id { get; set; }
                  
        public string Link_Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public Link()
        {
            Products = new HashSet<Product>();
            Categories = new HashSet<Category>();
            Properties = new HashSet<Property>();
        }
    }
}