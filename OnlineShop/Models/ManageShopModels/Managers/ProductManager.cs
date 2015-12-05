using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Models.ManageShopModels.Managers
{
    public static class ProductManager
    {
        public const long DefaultProductId = -1;

        /// <summary>
        /// add new created product to db
        /// </summary>
        /// <param name="product"></param>
        public static void SaveNewProduct(Product product)
        {
            App.Rep.Insert<Product>(product, true);
        }

        /// <summary>
        /// Get some ProductView model by product Id
        /// Using for see some Details or  Edit
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>ProductView model searched by incoming Id</returns>
		public static ProductView GetProductById(long id)
        {
            if (id == DefaultProductId || id == 0)
                return null;
            var dbProduct = App.Rep.Select<Product>()
              .FirstOrDefault(p => p.Pr_Id == id);
            if (dbProduct != null)
            {
                return (ProductView)App.Mapper.Map(dbProduct,
                    typeof(Product), typeof(ProductView));
            }
            else
                return null;
        }

        /// <summary>
        /// Map Product model into ProductView using AutoMapper
        /// </summary>
        /// <param name="product">model for converting</param>
        /// <returns>Converted by AutoMapper ProductView model</returns>
		public static ProductView MapToProductView(Product product)
        {
            if (product != null)
                return (ProductView)App.Mapper.Map(product,
                   typeof(Product), typeof(ProductView));
            return null;
        }

        /// <summary>
        /// UnMap from ProductView into Product DB model
        /// </summary>
        /// <param name="product">ProductView model for converting</param>
        /// <returns>Product DB model</returns>
		public static Product MapToProduct(ProductView product)
        {
            if (product != null)
                return (Product)App.Mapper.Map(product,
                       typeof(ProductView), typeof(Product));
            return null;
        }

        public static void UpdateProduct(ProductView product)
        {                                             //todo може і bool треба вертити а не void 
            Category cat = App.Rep.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Id == product.CatId);
            product.Category = cat;

            App.Rep.Update<Product>(ProductManager.MapToProduct(product),true); //todo може бути Exception, ЧОМУ?????
        }

        public static Category GetParentCategory(Category current)
        {
            return App.Rep.Select<Category>()
                .FirstOrDefault(c => current != null && c.Cat_Id == current.Cat_Parent_Cat_Id);
        }

        public static IEnumerable<Category> GetListOfCategotiesWithSameRootAsParent(Category current)
        {
            return App.Rep.Select<Category>()
                .Where(c => current != null && c.Cat_Parent_Cat_Id == current.Cat_Parent_Cat_Id);
        }

        public static IEnumerable<Category> GetRootCategories()
        {
            return App.Rep
                .Select<Category>().Where(c => c.Cat_Level == 1);
        }

        public static Category GetCategoryByProductId(long id)
        {
            return App.Rep
                .Select<Category>().FirstOrDefault(c => c.Cat_Id == id);
        }

        public static IEnumerable<Category> GetCategoriesByLevel(Category parent)
        {
            return App.Rep.Select<Category>()
                .Where(c => parent != null && c.Cat_Level == parent.Cat_Level);
        }

        /// <summary>
        /// Return IEnumerable collection with All founded Products converted to ProductView class
        /// </summary>
        public static IEnumerable<ProductView> GetAllProducts()
        {
            var resProductView = new List<ProductView>();//collection
            List<Product> products = App.Rep.Select<Product>().ToList();

            foreach (var product in products)//mapping
            {
                var viewProduct = MapToProductView(product);
                if (viewProduct != null)
                    resProductView.Add(viewProduct);
            }
            return resProductView;
        }
    }
}