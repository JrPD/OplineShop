using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
	public class Cart
	{
		public long Cart_Id { get; set; }

		public long Cart_Pr_Id { get; set; }

		public byte Cart_Count { get; set; }

		public DateTime Cart_DataCreation { get; set; }//todo ???

		public virtual ICollection<Product> Products { get; set; }

		public string User { get; set; } //todo ???

		public Cart()
		{
			Products = new HashSet<Product>();
		}
	}
}