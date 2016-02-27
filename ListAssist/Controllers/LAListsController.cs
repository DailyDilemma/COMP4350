using System.Net;
using System.Web.Mvc;
using ListAssist.Data;
using ListAssist.Data.Queries;
using ListAssist.Data.Models;
using System;

namespace ListAssist.Controllers
{
    public class LAListsController : Controller
    {
        private ListQueries queries;

        public LAListsController()
        {
            var db = new ListAssistContext("ListAssistContext");
            db.Database.Initialize(false);

            this.queries = new ListQueries(db);
        }

        // GET: LALists
        public ActionResult Index()
        {
            return View(queries.GetLists());
        }

        // GET: LALists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LAList lAList = queries.GetList((int) id);

            if (lAList == null)
            {
                return HttpNotFound();
            }

            return View("Details" ,lAList);
        }

        // GET: LALists/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: LALists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] LAList lAList)
        {
            if (ModelState.IsValid)
            {
                if (queries.AddList(lAList))
                {
                    return RedirectToAction("Edit", lAList);
                }
            }

            return View("Create", lAList);
        }

        // GET: LALists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LAList lAList = queries.GetList((int) id);

            if (lAList == null)
            {
                return HttpNotFound();
            }
            return View("Edit", lAList);
        }

        // POST: LALists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LAList lAList)
        {
            if (ModelState.IsValid)
            {
                queries.UpdateList(lAList);

                return RedirectToAction("Index");
            }
            return View("Edit",lAList);
        }

        // GET: LALists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LAList lAList = queries.GetList((int) id);

            if (lAList == null)
            {
                return HttpNotFound();
            }

            return View("Delete",lAList);
        }

        // POST: LALists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            queries.RemoveList(id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveListItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LAListItem lAListItem = queries.GetItem((int)id);
            if (lAListItem == null)
            {
                return HttpNotFound();
            }

            return View("RemoveListItem", lAListItem);
        }

        [HttpPost, ActionName("RemoveListItem")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveListItem(int id)
        {
            int listID = queries.DeleteItemFromList(id);

            return RedirectToAction("Edit", new { id = listID });
        }

        public ActionResult AddListItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAListItem lAListItem = new LAListItem() { ListID = (int)id };
            
            return View("AddListItem", lAListItem);
        }

        [HttpPost, ActionName("AddListItem")]
        [ValidateAntiForgeryToken]
        public ActionResult AddListItem([Bind(Include = "ListID,Description")]LAListItem lAListItem)
        {
            if ( ModelState.IsValid )
            {
                queries.AddItemToList(lAListItem);

                return RedirectToAction("Edit", new { id = lAListItem.ListID });
            }

            return View("AddListItem", lAListItem);
        }
    }
}
