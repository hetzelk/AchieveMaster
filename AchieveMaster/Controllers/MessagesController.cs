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

namespace AchieveMaster.Controllers
{
    public class MessagesController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();

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
                    MyMessages.Add(eachMessage);
                }
            }
            return View(MyMessages);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            string UserID = User.Identity.GetUserId();
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
            return View(messages);
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
            if (messages.FirstPerson != UserID | messages.SecondPerson != UserID)
            {
                return RedirectToAction("NoAccess", "Request");
            }
            List<string> SplitConversation = messages.Conversation.Split('~').ToList();
            ViewBag.SplitConversation = SplitConversation;
            return View(messages);
        }

        // GET: Messages/Create
        public ActionResult Create(string title, string user1)
        {
            string user2 = User.Identity.GetUserId();
            ViewBag.title = title;
            ViewBag.user1 = user1;
            ViewBag.user2 = user2;
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,FirstPerson,SecondPerson,FirstDiscontinued,SecondDiscontinued,Conversation,Expired")] Messages messages)
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
