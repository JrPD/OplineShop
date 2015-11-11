using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Managers
{
	public static class ProductManager
	{
		/// <summary>
		/// add new created product to db
		/// </summary>
		/// <param name="product"></param>
		public static void SaveNewProduct(Product product)
		{
			MvcApplication.ContextRepository.Insert<Product>(product, true);
		}

		public static ProductView GetProductById(int id)
		{
			var dbProduct = MvcApplication.ContextRepository.Select<Product>()
			  .FirstOrDefault(p => p.Pr_Id == id);
			if (dbProduct != null)
			{
				var catView = (ProductView) MvcApplication.Mapper.Map(dbProduct,
					typeof (Product), typeof (ProductView));

				return catView;
			}
			else
			{
				throw new Exception("null product");
			}
		}

		public static ProductView MapToProductView(Product product)
		{
			return (ProductView)MvcApplication.Mapper.Map(product,
				   typeof(Product), typeof(ProductView));
		}

		public static Product MapToProduct(ProductView product)
		{
			return (Product)MvcApplication.Mapper.Map(product,
				   typeof(ProductView), typeof(Product));
		}

		public static bool UpdateProduct(Product product)
		{
			try
			{
				MvcApplication.ContextRepository.Update<Product>(product, true);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public  static Category GetParentCategory(Category current)
		{
			return MvcApplication.ContextRepository.Select<Category>()
				.FirstOrDefault(c => current != null && c.Cat_Id == current.Cat_Parent_Cat_Id);
		}

		public static IEnumerable<Category> GetListOfCategotiesWithSameRootAsParent(Category current)
		{
			return MvcApplication.ContextRepository.Select<Category>()
				.Where(c => current != null && c.Cat_Parent_Cat_Id == current.Cat_Parent_Cat_Id);
		}

		public static IEnumerable<Category> GetRootCategories()
		{
			return MvcApplication.ContextRepository
				.Select<Category>().Where(c => c.Cat_Level == 1);
		}

		public static Category GetCategoryByProductId(long id)
		{
			return MvcApplication.ContextRepository
				.Select<Category>().FirstOrDefault(c => c.Cat_Id == id);
		}

		public static IEnumerable<Category> GetCategoriesByLevel(Category parent)
		{
			return MvcApplication.ContextRepository.Select<Category>()
				.Where(c => parent != null && c.Cat_Level == parent.Cat_Level);
		}
	}
}