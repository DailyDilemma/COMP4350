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
    public class LAShoppingListsController : Controller
    {
        private ListAssistContext db = new ListAssistContext();

        // GET: LAShoppingLists
        public ActionResult Index()
        {
            return View(db.LAShoppingLists.ToList());
        }

        // GET: LAShoppingLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAShoppingList lAShoppingList = (LAShoppingList)db.LALists.Find(id);
            if (lAShoppingList == null)
            {
                return HttpNotFound();
            }
            return View(lAShoppingList);
        }

        // GET: LAShoppingLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LAShoppingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Owner,Store")] LAShoppingList lAShoppingList)
        {
            if (ModelState.IsValid)
            {
                db.LALists.Add(lAShoppingList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lAShoppingList);
        }

        // GET: LAShoppingLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAShoppingList lAShoppingList = (LAShoppingList)db.LALists.Find(id);
            if (lAShoppingList == null)
            {
                return HttpNotFound();
            }
            return View(lAShoppingList);
        }

        // POST: LAShoppingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Owner,Store")] LAShoppingList lAShoppingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lAShoppingList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lAShoppingList);
        }

        // GET: LAShoppingLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAShoppingList lAShoppingList = (LAShoppingList)db.LALists.Find(id);
            if (lAShoppingList == null)
            {
                return HttpNotFound();
            }
            return View(lAShoppingList);
        }

        // POST: LAShoppingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LAShoppingList lAShoppingList = (LAShoppingList)db.LALists.Find(id);
            db.LALists.Remove(lAShoppingList);
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
