﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineShop.Models.Db;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Controllers
{

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var list = MvcApplication.ContextRepository.Select<Category>().ToList();

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