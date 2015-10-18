using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{
	
	public class Image
	{
		public long Img_Id { get; set; }

		public string Img_Path { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Image()
        {
            Products = new HashSet<Product>();
        }
    }

}