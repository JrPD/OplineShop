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
    public class CategoryController : Controller
    {

        private CategoryManager catManager;

        public CategoryController()
        {
            catManager = new CategoryManager();
        }


        [HttpGet]
        public ActionResult EditSomeCategory(long id = CategoryManager.DefaultParentCategoryId)
        {
            ViewBag.ParentName = catManager.GetParentName(id);
            SetParentCookie(ViewBag.ParentName);
            if (id == Convert.ToInt64(Res.DefaultParentCategoryId))
                return AddNewCategory(ViewBag.ParentName);
            try
            {
                return View(catManager.GetCategoryById(id));
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
                    model.ImagePath = Res.ImagesDirectory
                         + Res.CategoryImagesDirectory
                         + Guid.NewGuid().ToString()
                         + model.ImgFile.FileName;
                    model.ImgFile.SaveAs(Server.MapPath(Res.RootPath + model.ImagePath));
                    catManager.SaveNewImage(model);
                }
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
                if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
                {
                    model.ImagePath = Res.ImagesDirectory
                        + Res.CategoryImagesDirectory
                        + Guid.NewGuid().ToString()
                        + model.ImgFile.FileName;
                    model.ImgFile.SaveAs(Server.MapPath(Res.RootPath + model.ImagePath));
                    catManager.SaveNewImage(model);
                }
                catManager.SaveNewCategory(model);
                return RedirectToAction("EditCategories", new RouteValueDictionary(
                    new { parentId = model.ParentId }));
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult RemoveSomeCategory(long id = CategoryManager.DefaultParentCategoryId)
        {
            var parId = catManager.GetParentId(id);
            if (id == Convert.ToInt64(Res.DefaultParentCategoryId))
            {
                //todo log it
                return RedirectToAction("EditCategories", new RouteValueDictionary(
                    new { parentId = parId }));
            }
            catManager.RemoveCategoryById(id);
            return RedirectToAction("EditCategories", new RouteValueDictionary(
                new { parentId = parId }));
        }

        [AllowAnonymous]
        public ActionResult GetPath()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("ParentName"))
                return PartialView(catManager.GetAllParentCategories(
                    this.ControllerContext.HttpContext.Request.Cookies["ParentName"].Value));
            else
                return null;
        }

        public ActionResult EditCategories(long parentId = CategoryManager.DefaultParentCategoryId)
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

    }
}