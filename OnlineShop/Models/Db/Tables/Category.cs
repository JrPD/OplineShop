using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{
    public class Category
    {
        public long Cat_Id { get; set; }

        public byte Cat_Level { get; set; }

        public long Cat_Parent_Cat_Id { get; set; }

        public string Cat_Name { get; set; }

        public bool Cat_HasChild { get; set; }

        public long Cat_Img_Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Link> Links { get; set; }

        public virtual Image Image { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
            Links = new HashSet<Link>();
        }
    }
}