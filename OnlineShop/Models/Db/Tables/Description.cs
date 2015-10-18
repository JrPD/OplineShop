using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{
	
	public class Description
	{
		public long Desc_Id { get; set; }

		public string Desc_Path { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Description()
        {
            Products = new HashSet<Product>();
        }
    }
}