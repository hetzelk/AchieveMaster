using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveMaster.Models;
using Microsoft.AspNet.Identity;

namespace AchieveMaster.Controllers
{
    public class HomeController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();
            ViewBag.UserId = UserID;
            List<Models.Request> CurrentRequests = new List<Models.Request>();
            foreach (Request eachRequest in db.Requests)
            {
                if (eachRequest.Expired == "true")
                {
                    //do nothing
                }
                else
                {
                    CurrentRequests.Add(eachRequest);
                }
            }
            return View(CurrentRequests);
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
