using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ManageShopController : Controller
	{
		private CategoryManager catManager;

		public ManageShopController()
		{
			catManager = new CategoryManager();
		}


		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult EditCategories(long parentId = CategoryManager.DefParentId)
		{
			ViewBag.ParentName = catManager.GetNameFromId(parentId);
            SetParentCookie(ViewBag.ParentName);
            ViewBag.IsLastLevel = catManager.IsNextLastLevel(parentId);
            return View(catManager.GetAllCategories(parentId));
		}

        public void SetParentCookie(string parentName)
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("ParentName"))
            {
                this.ControllerContext.HttpContext.Request.Cookies["ParentName"].Value = parentName;
            }
            else
            {
                HttpCookie cookie = new HttpCookie("ParentName");
                cookie.Value = parentName;
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        [HttpGet]
		public ActionResult EditProducts()
		{
			return RedirectToAction("Index","Product");
		}

		[HttpGet]
		public ActionResult EditSomeCategory(long id = CategoryManager.DefParentId)
		{
            ViewBag.ParentName = catManager.GetParentName(id);
            SetParentCookie(ViewBag.ParentName);
            if (id == CategoryManager.DefParentId)
				return AddNewCategory(ViewBag.ParentName);
			try
			{
				return View(catManager.GetCategoryById(id));
			}
			catch(Exception)
			{
				return AddNewCategory(ViewBag.ParentName);
			}
		}

		[HttpPost]
		public ActionResult EditSomeCategory(CategoryView model)
		{  
            if (ModelState.IsValid)
			{
				catManager.UpdateCategory(model);
				return RedirectToAction("EditCategories", new RouteValueDictionary(
					new { parentId = model.ParentId }));
			}
			return View(model);
		}

        [HttpGet]
        public ActionResult AddNewCategory(string parentName)
        {
            ViewBag.ParentName = parentName;
            SetParentCookie(ViewBag.ParentName);
            return View(catManager.CreateNewModel(parentName));
        }

		[HttpPost]
		public ActionResult AddNewCategory(CategoryView model)
		{
			if (ModelState.IsValid)
			{
				catManager.SaveNewCategory(model);                         
				return RedirectToAction("EditCategories", new RouteValueDictionary(
					new { parentId = model.ParentId }));
			}
			return View(model);
		}

		[HttpGet]
		public ActionResult RemoveSomeCategory(long id = CategoryManager.DefParentId)
		{
            var parId = catManager.GetParentId(id);
			if (id == CategoryManager.DefParentId)
			{
				//todo log it
				return RedirectToAction("EditCategories", new RouteValueDictionary(
					new { parentId = parId }));
			}
			catManager.RemoveCategoryById(id);
			return RedirectToAction("EditCategories", new RouteValueDictionary(
				new { parentId = parId }));
		}

        [HttpGet]
        public ActionResult GetPath()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("ParentName"))
                return PartialView(catManager.GetAllParentCategories(
                    this.ControllerContext.HttpContext.Request.Cookies["ParentName"].Value));
            else
                return null;
        }
	}
}