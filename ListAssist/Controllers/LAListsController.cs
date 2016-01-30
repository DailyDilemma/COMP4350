using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListAssist.Data;
using ListAssist.Data.Models;

namespace ListAssist.Controllers
{
    public class LAListsController : Controller
    {
        private ListAssistContext db = new ListAssistContext();

        // GET: LALists
        public ActionResult Index()
        {
            return View(db.LALists.ToList());
        }

        // GET: LALists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAList lAList = db.LALists.Find(id);
            if (lAList == null)
            {
                return HttpNotFound();
            }
            return View(lAList);
        }

        // GET: LALists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LALists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] LAList lAList)
        {
            if (ModelState.IsValid)
            {
                db.LALists.Add(lAList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lAList);
        }

        // GET: LALists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAList lAList = db.LALists.Find(id);
            if (lAList == null)
            {
                return HttpNotFound();
            }
            return View(lAList);
        }

        // POST: LALists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] LAList lAList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lAList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lAList);
        }

        // GET: LALists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAList lAList = db.LALists.Find(id);
            if (lAList == null)
            {
                return HttpNotFound();
            }
            return View(lAList);
        }

        // POST: LALists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LAList lAList = db.LALists.Find(id);
            db.LALists.Remove(lAList);
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
