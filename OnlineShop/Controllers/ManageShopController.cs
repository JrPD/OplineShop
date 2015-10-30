using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult EditCategories(string parentName)
        {
            return View(catManager.GetAllCategories(
                catManager.GetIdFromName(parentName)));
        }

        [HttpGet]
        public ActionResult EditProducts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditSomeCategory(string catName)
        {//todo зробити
            return View();
        }
    }
}