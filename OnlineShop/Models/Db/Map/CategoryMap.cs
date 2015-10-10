using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Map
{
	public class CategoryMap : EntityTypeConfiguration<Category>

	{
		public CategoryMap()
		{
			HasMany(c => c.ProductsCollection)
				.WithRequired()
				.HasForeignKey(p => p.Pr_Id);
		}
	}
}