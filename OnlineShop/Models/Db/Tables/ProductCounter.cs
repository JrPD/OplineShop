using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{

	public class ProductCounter
	{
		public long Pr_Id { get; set; }

        public int Pr_Count { get; set; }

        public virtual Product Product { get; set; }

        public ProductCounter()
        {
            Product = new Product();
        }
    }

}