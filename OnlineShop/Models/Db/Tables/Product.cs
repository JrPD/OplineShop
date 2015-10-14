using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{

	public class Product
	{
		[Key]
		public long Pr_Id { get; set; }

		//public long Pr_Cat_Id { get; set; }
		[Required(ErrorMessage = "Enter a name for the product.")]
		public string Pr_Name { get; set; }

		public double Pr_Price { get; set; }

		public virtual  Category Pr_Category { get; set; }
		
		public bool  Pr_IsAviable { get; set; }

		//todo	public long Pr_Description { get; set; }
		//todo це поле не відображається
		public  ICollection<Image> Pr_Images { get; set; }

		public virtual Description Pr_Description { get; set; }

		public Product()
		{
			Pr_Images = new HashSet<Image>();
			Pr_Description = new Description();
		}
	}

}