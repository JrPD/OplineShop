using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Map
{
	public class ProductMap:EntityTypeConfiguration<Product>
	{
		public ProductMap()
		{
			HasOptional(p => p.Pr_Category)
					.WithMany(t => t.ProductsCollection)
					.Map(c => c.MapKey("Pr_Id"));
		}
	}
}