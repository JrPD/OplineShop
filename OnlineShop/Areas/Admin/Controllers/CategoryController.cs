using OnlineShop.Models.ImageManager;
using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult EditSomeCategory(long id = CategoryManager.DefaultParentCategoryId)
        {
            ViewBag.ParentName = CategoryManager.GetParentName(id);
            SetParentCookie(ViewBag.ParentName);
            if (id == CategoryManager.DefaultParentCategoryId)
                return AddNewCategory(ViewBag.ParentName);
            try
            {
                return View(CategoryManager.GetCategoryById(id));
            }
            catch (Exception)
            {
                return AddNewCategory(ViewBag.ParentName);
            }
        }

        [HttpPost]
        public ActionResult EditSomeCategory(CategoryView model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
                {
                    CategoryManager.SaveNewImage(model);
                }
                CategoryManager.UpdateCategory(model);
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
            return View(CategoryManager.CreateNewModel(parentName));
        }

        [HttpPost]
        public ActionResult AddNewCategory(CategoryView model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
                {
                    CategoryManager.SaveNewImage(model);
                }
                CategoryManager.SaveNewCategory(model);
                return RedirectToAction("EditCategories", new RouteValueDictionary(
                    new { parentId = model.ParentId }));
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult RemoveSomeCategory(long id = CategoryManager.DefaultParentCategoryId)
        {
            var parId = CategoryManager.GetParentId(id);
            if (id == CategoryManager.DefaultParentCategoryId)
            {
                //todo log it
                return RedirectToAction("EditCategories", new RouteValueDictionary(
                    new { parentId = parId }));
            }
            CategoryManager.RemoveCategoryById(id);
            return RedirectToAction("EditCategories", new RouteValueDictionary(
                new { parentId = parId }));
        }

        [AllowAnonymous]
        public ActionResult GetPath()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("ParentName"))
                return PartialView(CategoryManager.GetAllParentCategories(
                    this.ControllerContext.HttpContext.Request.Cookies["ParentName"].Value));
            else
                return null;
        }

        public ActionResult EditCategories(long parentId = CategoryManager.DefaultParentCategoryId)
        {
            ViewBag.ParentName = CategoryManager.GetNameFromId(parentId);
            SetParentCookie(ViewBag.ParentName);
            ViewBag.IsLastLevel = CategoryManager.IsNextLastLevel(parentId);
            return View(CategoryManager.GetAllCategories(parentId));
        }

        public ActionResult GetImage(string path)
        {
            try
            {
                return File(ImageManager.DownloadFile(path), "image/png");
            }
            catch
            {
                return null;
            }
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
    }
}