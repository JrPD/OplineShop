using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
	public class Category
	{
		[Key]
		public long Cat_Id { get; set; }

		[Range(0, 5,  ErrorMessage = "Category level up to 5")]
		public byte Cat_Level { get; set; }

		public long Cat_Parent_Cat_Id { get; set; }

		[Required(ErrorMessage = "Enter a name for the category.")]
		[StringLength(200)]
		public string Cat_Name  { get; set; }

		public string Availability { get; set; }

		public virtual ICollection<Product> Cat_Products { get; set; }//one to many

		public Category()
		{
			Cat_Products = new HashSet<Product>();
		}
	}

	

}