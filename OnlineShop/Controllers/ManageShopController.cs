using OnlineShop.Models.ManageShopModels.Managers;
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
        public ActionResult EditCategories()
        {
            var list = catManager.GetAllCategories();
            return View();
        }

        [HttpGet]
        public ActionResult EditProducts()
        {
            return View();
        }
    }
}