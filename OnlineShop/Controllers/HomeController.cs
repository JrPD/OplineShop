using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineShop.Models.Db;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Controllers
{

	public class HomeController : Controller
	{
        public ActionResult TestMenu()
        {

            return PartialView(MvcApplication.ContextRepository.Select<Category>().ToList());
        }
		public ActionResult Index()
		{
			List<Category> category = MvcApplication.ContextRepository.Select<Category>().Where(c => c.Cat_Level == 1).ToList();

			return View(category);
		}
        public ActionResult Browse(long id) //Browse categories and products 
        {
            Category category = MvcApplication.ContextRepository.Select<Category>().Single(c=>c.Cat_Id== id);
            if (category.Cat_HasChild)
            {
                List<Category> categories = MvcApplication.ContextRepository.Select<Category>()
                    .Where( c=> c.Cat_Parent_Cat_Id == id).ToList();
                return View(categories);
            }
            else
            {
                List<Product> products = MvcApplication.ContextRepository.Select<Product>()
                    .Where(p => p.Pr_Cat_Id == id).ToList();
                return View("BrowseProducts", products);
            }
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