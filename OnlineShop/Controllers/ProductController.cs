using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;

namespace OnlineShop.Controllers
{
	public class ProductController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			
			return View(ProductManager.GetAllProducts());
		}

		public ActionResult Details(long id = ProductManager.DefaultProductId)
		{
            if (id == ProductManager.DefaultProductId)
                return RedirectToAction("Index", "Product");
            var model = ProductManager.GetProductById(id);
            if (model != null)
                return View(model);
            return RedirectToAction("Index", "Product");
		}

		[HttpGet]
		public ActionResult Create()
		{
			ViewBag.CategoriesID = new SelectList(CategoryManager.GetAllCategories(CategoryManager.DefaultParentCategoryId), "Cat_id", "Cat_Name");

			ViewBag.SubCatID = ViewBag.CategoriesID;
			
			return View(new ProductView());
		}

		[HttpPost]
		public ActionResult Create(ProductView product)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception("Model is not valid!!!");
			}

			try
			{
				Category cat = App.Rep.Select<Category>()
					.FirstOrDefault(c => c.Cat_Id == product.CatId);
				product.Category = cat;
				
				var mapProduct = ProductManager.MapToProduct(product);
				ProductManager.SaveNewProduct(mapProduct);

				return RedirectToAction("Index");
			}
			catch(Exception ex)
			{
				return View(ex.ToString());
			}
		}
		
		[HttpGet]
		public ActionResult Edit(int id)
		{
			Product product = App.Rep.
				Select<Product>().FirstOrDefault(p => p.Pr_Id==id);

			SetSelectedValues(product);

			var editProduct=ProductManager.GetProductById(id);

			return View(editProduct);
		}

		/// <summary>
		/// fill in dropdown needed values. from category - parent category - root category
		/// all products must have 3-level category!!!
		/// </summary>
		/// <param name="product"></param>
		private void SetSelectedValues(Product product)
		{                                                                               
			if (product.Pr_Cat_Id<0)
			{
				ViewBag.CategoriesL1 = new  SelectList(ProductManager.GetRootCategories(), "Cat_id", "Cat_Name");
				ViewBag.CategoriesL2 = new  SelectList(new List<Category>());
				ViewBag.CategoriesL3 = ViewBag.CategoriesL2;
				return;
			}


			// get list of categories from product category
			Category current = ProductManager.GetCategoryByProductId(product.Pr_Cat_Id);

			var l3 = ProductManager.GetListOfCategotiesWithSameRootAsParent(current);

			Category parent = ProductManager.GetParentCategory(current);

			// get list of categories of parent category
			var l2 = ProductManager.GetCategoriesByLevel(parent);
			
			// get root category list
			var l1 = ProductManager.GetRootCategories();

			Category root = ProductManager.GetParentCategory(parent);

			// set selected category to dropdown list
			var level1 = new SelectList(l1, "Cat_id", "Cat_Name");
			var selected1 = level1.FirstOrDefault(x => root != null && x.Value == root.Cat_Id.ToString());
			if (selected1 != null)
			{
				level1 = new SelectList(l1, "Cat_id", "Cat_Name", selectedValue: selected1.Value);
			}

			var level2 = new SelectList(l2, "Cat_id", "Cat_Name");

			var selected2 = level2.FirstOrDefault(x => parent != null && x.Value == parent.Cat_Id.ToString());
			if (selected2 != null)
			{
				level2 = new SelectList(l2, "Cat_id", "Cat_Name", selectedValue: selected2.Value);
			}

			var level3 = new SelectList(l3, "Cat_id", "Cat_Name");
			var selected3 = level3.FirstOrDefault(x => current != null && x.Value == current.Cat_Id.ToString());
			if (selected3 != null)
			{
				level3 = new SelectList(l3, "Cat_id", "Cat_Name", selected3.Value);
			}

			ViewBag.CategoriesL1 = level1;
			ViewBag.CategoriesL2 = level2;
			ViewBag.CategoriesL3 = level3;
		}

		[HttpPost]
		public ActionResult Edit(ProductView product)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception("Model is not valid!!!");
			}

			try
			{
				Category cat = App.Rep.Select<Category>()
					.FirstOrDefault(c => c.Cat_Id == product.CatId);
				product.Category = cat;

				Product mapProduct = ProductManager.MapToProduct(product);
				bool res  =ProductManager.UpdateProduct(mapProduct);

				if (!res)
				{
					throw new Exception("Cannot update product");
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View(ex.ToString()+ ex.Message);
			}
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// get child categories by category id
		public ActionResult GetSubCategories(long? option)
		{
			var subCats = App.Rep.Select<Category>()
				.Where(c => c.Cat_Parent_Cat_Id == option);

			// todo WFT???
			var result = (from c in subCats
						  select new
						  {
							  id = c.Cat_Id,
							  name = c.Cat_Name
						  }).ToList();
			return Json(result, JsonRequestBehavior.AllowGet);
		} 
	}
}
