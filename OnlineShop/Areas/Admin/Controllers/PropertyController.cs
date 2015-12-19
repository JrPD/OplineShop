using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class PropertyController : Controller
    {
        //todo Запитати про дефолтні значення
        // GET: Property
        public ActionResult Index()
        {
            return View(PropertyManager.GetAllLinksProperties());
        }

        [HttpGet]
        public ActionResult Properties(long link_id)
        {
            ViewBag.LinkName = PropertyManager.GetLinkName(link_id);
            return View(PropertyManager.GetProperties(link_id));
        }

        [HttpGet]
        public ActionResult CreateLink()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLink(LinkView link)
        {
            if (ModelState.IsValid)
            {
                PropertyManager.AddNewLink(link);
                return RedirectToAction("Index");
            }
            return View(link);
        }

        [HttpGet]
        public ActionResult DeleteLink(long link_id)
        {
            PropertyManager.RemoveLink(link_id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditLink(long link_id)
        {
            return View(PropertyManager.GetLinkView(link_id));
        }

        [HttpPost]
        public ActionResult EditLink(LinkView link)
        {
            if (ModelState.IsValid)
            {
                PropertyManager.UpdateLink(link);
                return RedirectToAction("Index");
            }
            return View(link);
        }

        [HttpGet]
        public ActionResult CreateProperty(string linkName)
        {
            long link_id = PropertyManager.GetLinkId(linkName);
            return View(PropertyManager.CreateNewPropertyModel(link_id));
        }

        [HttpPost]
        public ActionResult CreateProperty(PropertyView property)
        {
            if (ModelState.IsValid)
            {
                PropertyManager.AddNewProperty(property);
                return RedirectToAction("Properties", new RouteValueDictionary(
                    new { link_id = property.LinkId }));
            }
            return View(property);
        }

        [HttpGet]
        public ActionResult EditProperty(long prop_id)
        {
            return View(PropertyManager.GetPropertyView(prop_id));
        }

        [HttpPost]
        public ActionResult EditProperty(PropertyView property)
        {
            if (ModelState.IsValid)
            {
                PropertyManager.UpdateProperty(property);
                return RedirectToAction("Properties", new RouteValueDictionary(
                    new { link_id = property.LinkId }));
            }
            return View(property);
        }

        [HttpGet]
        public ActionResult DeleteProperty(long prop_id)
        {
            long link_id = PropertyManager.GetLinkId(prop_id);
            PropertyManager.RemoveProperty(prop_id);
            return RedirectToAction("Properties", new RouteValueDictionary(
                new { link_id = link_id }));
        }
    }
}