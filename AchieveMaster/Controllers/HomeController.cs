using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveMaster.Models;

namespace AchieveMaster.Controllers
{
    public class HomeController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();
        public ActionResult Index()
        {
            return View(db.Requests.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}