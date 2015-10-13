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
		[Key]
		public long Cart_Id { get; set; }
		public long Cart_Pr_Id { get; set; }
		public byte Cart_Count { get; set; }
		public DateTime Cart_DataCreation { get; set; }
		public virtual ICollection<Product> Cart_Products { get; set; }
		public string User { get; set; } //todo ???

		public Cart()
		{
			Cart_Products = new HashSet<Product>();
		}
	}
}