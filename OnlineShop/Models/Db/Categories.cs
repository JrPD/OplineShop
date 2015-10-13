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

	public class Product
	{
		[Key]
		public long Pr_Id { get; set; }
		//public long Pr_Cat_Id { get; set; }
		[Required(ErrorMessage = "Enter a name for the product.")]
		public string Pr_Name { get; set; }
		public double Pr_Price { get; set; }
		//public long Pr_Charact { get; set; }
		public virtual  Category Pr_Category { get; set; }
//todo		public long Pr_Description { get; set; }

		//todo це поле не відображається
		public  ICollection<Image> Pr_Images { get; set; }
		public virtual Description Pr_Description { get; set; }

		public Product()
		{
			Pr_Images = new HashSet<Image>();
			Pr_Description = new Description();
		}
	}

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

	public class Image
	{
		[Key]
		public long Im_Id { get; set; }
		public string Im_Path { get; set; }
	}

	public class Description
	{
		[Key]
		public long Desc_Id { get; set; }
		public string Desc_Path { get; set; }
	}
}