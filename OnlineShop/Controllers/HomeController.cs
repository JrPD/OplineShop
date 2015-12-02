using OnlineShop.Models.Db.Tables;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TestMenu()
        {
            return PartialView(App.Rep.Select<Category>().ToList());
        }

        public ActionResult PropertiesFilter()
        {
            return PartialView(App.Rep.Select<Link>().ToList());
        }

        public ActionResult Index()
        {
            List<Category> category = App.Rep.Select<Category>().Where(c => c.Cat_Level == 1).ToList();

            return View(category);
        }

        public ActionResult Browse(long id, int? page) //Browse categories and products
        {
            Category category = App.Rep.Select<Category>().Single(c => c.Cat_Id == id);
            if (category.Cat_HasChild)
            {
                List<Category> categories = App.Rep.Select<Category>()
                    .Where(c => c.Cat_Parent_Cat_Id == id).ToList();
                return View(categories);
            }
            else
            {
                //todo Винести в константу чи задавати PageSize динамічно??
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                List<Product> products = App.Rep.Select<Product>()
                    .Where(p => p.Pr_Cat_Id == id).ToList();
                return View("BrowseProducts", products.ToPagedList(pageNumber, pageSize));
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