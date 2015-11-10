using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models.ManageShopModels;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using OnlineShop.Models.ManageShopModels.Managers;

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

        public ActionResult Properties(long link_id)
        {
            ViewBag.LinkName = propManager.GetLinkName(link_id);
            return View(propManager.GetProperties(link_id));
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(LinkView link)
        {
            if (ModelState.IsValid)
            {
                propManager.AddNewLink(link);
                return RedirectToAction("Index");
            }
            return View(link);
        }
    }
}