using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models.ManageShopModels;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using OnlineShop.Models.ManageShopModels.Managers;
using System.Web.Routing;

namespace OnlineShop.Controllers
{
    public class PropertyController : Controller
    {
        private PropertyManager propManager;

        public PropertyController()
        {
            propManager = new PropertyManager();
        }
        // GET: Property
        public ActionResult Index()
        {
            return View(propManager.GetAllLinksProperties());
        }

        [HttpGet]
        public ActionResult Properties(long link_id)
        {
            ViewBag.LinkName = propManager.GetLinkName(link_id);
            return View(propManager.GetProperties(link_id));
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
                propManager.AddNewLink(link);
                return RedirectToAction("Index");
            }
            return View(link);
        }

        [HttpGet]
        public ActionResult DeleteLink(long link_id)
        {
            propManager.RemoveLink(link_id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateProperty(string linkName)
        {
            long link_id = propManager.GetLinkId(linkName);
            return View(propManager.CreateNewPropertyModel(link_id));
        }

        [HttpPost]
        public ActionResult CreateProperty(PropertyView property)
        {
            propManager.AddNewProperty(property);
            return RedirectToAction("Properties", new RouteValueDictionary(
                new { link_id = property.Link_Id }));
        }
        [HttpGet]
        public ActionResult DeleteProperty(long prop_id)
        {
            long link_id = propManager.GetLinkId(prop_id);
            propManager.RemoveProperty(prop_id);
            return RedirectToAction("Properties", new RouteValueDictionary(
                new { link_id = link_id }));
        }
    }
}