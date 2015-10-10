using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
	public class Category
	{
		public long Cat_Id { get; set; }
		public byte Cat_Level { get; set; }
		public long Cat_Parent_Cat_Id { get; set; }
		public string Cat_Name  { get; set; }
		public virtual ICollection<Product> ProductsCollection { get; set; }//ont to many

		public Category()
		{
			ProductsCollection = new HashSet<Product>();
		}
	}

	public class Product
	{
		public long Pr_Id { get; set; }
		public long Pr_Cat_Id { get; set; }
		public string Pr_Name { get; set; }
		public double Pr_Price { get; set; }
		public long Pr_Charact { get; set; }
		public virtual  Category Pr_Category { get; set; }
		public long Pr_Description { get; set; }
		
		public virtual ICollection<Cart> Carts { get; set; }

		public Product()
		{
			Carts = new HashSet<Cart>();
		}
	}

	public class Cart
	{
		public long Pr_Id { get; set; }
		public long Cart_Pr_Id { get; set; }
		public byte Cart_Count { get; set; }
		public DateTime Cart_DataCreation { get; set; }
		public string User { get; set; } //???

	}

	public class Images
	{
		public long Im_Id { get; set; }
		public string Im_Path { get; set; }
		//todo many-to-many product
	}

	public class Description
	{
		public long Desc_Id { get; set; }
		public string Desc_Path { get; set; }
		//todo many-to-many product
	}


}