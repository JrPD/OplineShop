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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditCategories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProducts()
        {
            return View();
        }
    }
}