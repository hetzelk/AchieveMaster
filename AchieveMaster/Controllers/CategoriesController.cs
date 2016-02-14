﻿using System;
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
    public class CategoriesController : Controller
    {
        private AchieveMasterDB db = new AchieveMasterDB();

        // GET: Categories
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() != "1905dbba-838a-419c-b714-e9c24b9a7476")
            {
                return RedirectToAction("NoAccess", "Request");
            }
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (User.Identity.GetUserId() != "1905dbba-838a-419c-b714-e9c24b9a7476")
            {
                return RedirectToAction("NoAccess", "Request");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (User.Identity.GetUserId() != "1905dbba-838a-419c-b714-e9c24b9a7476")
            {
                return RedirectToAction("NoAccess", "Request");
            }
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.GetUserId() != "1905dbba-838a-419c-b714-e9c24b9a7476")
            {
                return RedirectToAction("NoAccess", "Request");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.Identity.GetUserId() != "1905dbba-838a-419c-b714-e9c24b9a7476")
            {
                return RedirectToAction("NoAccess", "Request");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
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
