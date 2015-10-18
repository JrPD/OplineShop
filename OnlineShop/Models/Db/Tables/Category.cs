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

		//[Range(0, 5,  ErrorMessage = "Category level up to 5")]
		public byte Cat_Level { get; set; }

		public long Cat_Parent_Cat_Id { get; set; }

		//[Required(ErrorMessage = "Enter a name for the category.")]
		//[StringLength(200)]
		public string Cat_Name  { get; set; }

		public bool IsAvailable { get; set; }// todo ???

		public virtual ICollection<Product> Products { get; set; }//one to many

		public Category()
		{
			Products = new HashSet<Product>();
		}
	}

	

}