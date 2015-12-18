using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
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
        public ActionResult EditProducts()
        {
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult EditCategories()
        {
            return RedirectToAction("EditCategories", "Category");
        }

        [HttpGet]
        public ActionResult EditProperties()
        {
            return RedirectToAction("Index", "Property");
        }
    }
}