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
			var products = MvcApplication.ContextRepository.Select<Product>();

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
			return View();
		}

		// GET: Product/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Product/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

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
	}
}
