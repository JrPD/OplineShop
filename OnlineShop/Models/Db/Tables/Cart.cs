using System;
using System.Collections.Generic;

namespace OnlineShop.Models.Db.Tables
{
    public class Cart
    {
        public long Cart_Id { get; set; }

        public long Cart_Pr_Id { get; set; }

        public byte Cart_Count { get; set; }

        public DateTime Cart_DataCreation { get; set; }//todo ???

        public string User { get; set; } //todo ???

        public virtual ICollection<Product> Products { get; set; }

        public Cart()
        {
            Products = new HashSet<Product>();
        }
    }
}