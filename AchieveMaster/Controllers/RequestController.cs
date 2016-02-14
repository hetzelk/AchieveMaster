using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AchieveMaster.Models;
using Microsoft.AspNet.Identity;

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PayPalExample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string currency = "GBP"; //change to "USD" for US dollars
            string token = string.Empty;
            string retMsg = string.Empty;
            NVPAPICaller PPCaller = new NVPAPICaller();
            bool ret = PPCaller.ExpressCheckout(txtName.Text, txtDescription.Text, txtPrice.Text, txtQuantity.Text, currency, ref token, ref retMsg);
            if (ret)
            {
                HttpContext.Current.Session["token"] = token;
                Response.Redirect(retMsg);
            }
            else
            {
                //PayPal has not responded successfully, let user know
                lblError.Text = "PayPal is not responding, please try again in a few moments.";
            }
        }
    }
}
    */

namespace AchieveMaster.Controllers
{
    public class RequestController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();

        // GET: Request
        public ActionResult Index()
        {
            //PayPal.Api.Payment
            //var UserID = User.Identity.GetUserId();
            //ViewBag.UserId = UserID;
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

        public ActionResult NoAccess()
        {
            return View();
        }

        // GET: Request
        public ActionResult OldRequests()
        {
            var UserID = User.Identity.GetUserId();
            ViewBag.UserId = UserID;
            List<Models.Request> OldRequests = new List<Models.Request>();
            foreach (Request eachRequest in db.Requests)
            {
                if (eachRequest.Expired == "true" && eachRequest.UserID == UserID)
                {
                    OldRequests.Add(eachRequest);
                }
            }
            return View(OldRequests);
        }

        // GET: Request
        public ActionResult MyRequests()
        {
            var UserID = User.Identity.GetUserId();
            ViewBag.UserId = UserID;
            List<Models.Request> MyRequests = new List<Models.Request>();
            foreach(Request eachRequest in db.Requests)
            {
                if(eachRequest.Expired != "true" && eachRequest.UserID == UserID)
                {
                    MyRequests.Add(eachRequest);
                }
            }
            return View(MyRequests);
        }

        // GET: Request/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Request/Create
        public ActionResult Create()
        {
            var UserID = User.Identity.GetUserId();
            List<string> AllCategories = new List<string>();
            foreach(Categories category in db.Categories)
            {
                AllCategories.Add(category.Title);
            }
            ViewBag.ACategories = AllCategories;
            ViewBag.UserId = UserID;
            CategoriesList cats = new Models.CategoriesList();
            cats.AllCategories = new SelectList(db.Categories, "Title", "Title");
            return View();
        }

        // POST: Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ID,Title,Category,Description,StudentLocation,MeetLocation,PayRate")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: Request/Completed/5
        [Authorize]
        public ActionResult Completed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == request.UserID)
            {
                request.Expired = "true";
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyRequests", "Request");
            }
            return RedirectToAction("NoAccess", "Request");
        }
        
        // GET: Request/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == request.UserID)
            {
                return View(request);
            }
            return RedirectToAction("NoAccess", "Request");
        }

        // POST: Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,ID,Title,Category,Description,StudentLocation,MeetLocation,PayRate")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == request.UserID)
            {
                return View(request);
            }
            return RedirectToAction("NoAccess", "Request");
        }

        // POST: Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
