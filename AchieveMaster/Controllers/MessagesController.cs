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
using Microsoft.AspNet.Identity.Owin;

namespace AchieveMaster.Controllers
{
    public class MessagesController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Messages
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();
            ViewBag.UserId = UserID;
            List<Messages> MyMessages = new List<Messages>();
            foreach (Messages eachMessage in db.Messages)
            {
                if (eachMessage.Expired != "true" && eachMessage.FirstPerson == UserID | eachMessage.SecondPerson == UserID)
                {
                    if (UserID == eachMessage.FirstPerson && eachMessage.FirstDiscontinued == "true")
                    {
                        //don't add to message list
                    }
                    if (UserID == eachMessage.SecondPerson && eachMessage.SecondDiscontinued == "true")
                    {
                        //don't add to message list
                    }
                    else
                    {
                        MyMessages.Add(eachMessage);
                    }
                }
            }
            return View(MyMessages);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            Messages message = db.Messages.Find(id);
            string UserID = User.Identity.GetUserId();

            string FirstName1 = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(message.FirstPerson).FirstName;
            string LastName1 = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(message.FirstPerson).LastName;

            string FirstName2 = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(message.SecondPerson).FirstName;
            string LastName2 = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(message.SecondPerson).LastName;
            ViewBag.FirstPerson = FirstName1 + " " + LastName1;
            ViewBag.SecondPerson = FirstName2 + " " + LastName2;
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (message == null)
            {
                return HttpNotFound();
            }
            //if (messages.FirstPerson != UserID | messages.SecondPerson != UserID)
            //{
            //    return RedirectToAction("NoAccess", "Request");
            //}
            return View(message);
        }

        // GET: Messages/Details/5
        public ActionResult Reply(int? id)
        {
            var UserID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            //if (messages.FirstPerson != UserID | messages.SecondPerson != UserID)
            //{
            //    return RedirectToAction("NoAccess", "Request");
            //}
            List<string> SplitConversation = messages.Conversation.Split('~').ToList();
            ViewBag.SplitConversation = SplitConversation;
            return View(messages);
        }

        // POST: Messages/Reply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply([Bind(Include = "ID,NextMessage")] Messages newMessage)
        {
            if (ModelState.IsValid)
            {
                Messages message = db.Messages.Find(newMessage);
                message.Conversation = message.Conversation + newMessage;
                message.NewMessage = true;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                //this is where I left off at 11:22, I don't know if it works
            }

            return View(newMessage);
        }

        // GET: Messages/Create
        public ActionResult Create(string title, string user1)
        {
            string user2 = User.Identity.GetUserId();
            ApplicationUser userone = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user1);
            string FirstName1 = userone.FirstName;
            string LastName1 = userone.LastName;
            ApplicationUser usertwo = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user2);
            string FirstName2 = usertwo.FirstName;
            string LastName2 = usertwo.LastName;
            ViewBag.title = title;
            ViewBag.user1 = user1;
            ViewBag.user2 = user2;
            ViewBag.FirstPerson = FirstName1 + " " + LastName1;
            ViewBag.SecondPerson = FirstName2 + " " + LastName2;
            
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,FirstPerson,SecondPerson,FirstDiscontinued,SecondDiscontinued,Conversation,Expired,FirstPersonName,SecondPersonName")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messages);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,FirstPerson,SecondPerson,FirstDiscontinued,SecondDiscontinued,Conversation,Expired")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messages);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messages messages = db.Messages.Find(id);
            db.Messages.Remove(messages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Request/Completed/5
        [Authorize]
        public ActionResult LeaveConversation(int? id)
        {
            string UserID = User.Identity.GetUserId();
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Messages message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == message.FirstPerson | User.Identity.GetUserId() == message.SecondPerson)
            {
                if (UserID == message.FirstPerson)
                {
                    message.FirstDiscontinued = "true";
                    db.Entry(message).State = EntityState.Modified;
                    db.SaveChanges();
                }
                if (UserID == message.SecondPerson)
                {
                    message.SecondDiscontinued = "true";
                    db.Entry(message).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Messages");
            }
            return RedirectToAction("NoAccess", "Request");
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
