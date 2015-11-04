using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
	public class ProductController : Controller
	{
		// GET: Product
		public ActionResult Index()
		{
			var resProductView = new List<ProductView>();//collection 
			List<Product> products = MvcApplication.ContextRepository.Select<Product>().ToList();

			foreach (var product in products)//mapping
			{
				var viewProd = (ProductView)MvcApplication.Mapper.Map(product,
				   typeof(Product), typeof(ProductView));
				resProductView.Add(viewProd);
			}
			return View(resProductView);
		}

		// GET: Product/Details/5
		public ActionResult Details(int id)
		{
			var product = MvcApplication.ContextRepository.Select<Product>().
				FirstOrDefault(p => p.Pr_Id == id);
			return View();
		}

		// GET: Product/Create
		[HttpGet]
		public ActionResult Create()
		{
			ViewBag.CategoriesID = new SelectList(MvcApplication.ContextRepository.Select<Category>(), "Cat_id", "Cat_Name");

				
			ViewBag.SubCatID = ViewBag.CategoriesID;
			
			return View();
		}

		// POST: Product/Create
		[HttpPost]
		public ActionResult Create(ProductView product)
		{
			ViewBag.CategoriesID = new SelectList(MvcApplication.ContextRepository.Select<Category>(), "Cat_id", "Cat_Name");

			try
			{
				if (!ModelState.IsValid)
				{
					
				}
				//Product produc
				var mapProduct = (Product) MvcApplication.Mapper.Map(product, typeof(ProductView), typeof(Product));
				MvcApplication.ContextRepository.Insert<Product>(mapProduct, true);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Product/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Product/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Product/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Product/Delete/5
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

		public ActionResult GetSubCategories(int? PrCatId)
		{
			//if (String.IsNullOrEmpty(Cat_Id))
			//{
			//	throw new ArgumentNullException("countryId");
			//}
			int id = 0;
			var subCats = MvcApplication.ContextRepository.Select<Category>().Where(c => c.Cat_Parent_Cat_Id == PrCatId);
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
