using System.Collections.Generic;
using System.Web.Mvc;
using OnlineShop.Models.Db;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {

			//MvcApplication.ContextRepository.Context.Products.Add(new Product() {Pr_Name = "Product", Description = new Description(){Desc_Path = "dfd"}, Images = new List<Image>(), Pr_Price = 500});

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
