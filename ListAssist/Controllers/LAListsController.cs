using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ListAssist.Data;
using ListAssist.Data.Queries;
using ListAssist.Data.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ListAssist.Controllers
{
    public class LAListsController : Controller
    {
        private HttpClient newClient;

        public LAListsController()
        {
            this.newClient = new HttpClient();
            newClient.BaseAddress = new System.Uri("http://localhost:3693/");
            newClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: LALists
        public ActionResult Index()
        {

            HttpResponseMessage responseMsg = this.newClient.GetAsync("api/Lists/").Result;
            JArray jsonArray = JArray.Parse(responseMsg.Content.ReadAsStringAsync().Result);

            List<LAList> lAList = new List<LAList>();

            foreach (var jsonItem in jsonArray)
            {
                LAList newList = jsonItem.ToObject<LAList>();
                List<LAListItem> listItems = jsonItem["ShoppingListItems"].ToObject<List<LAListItem>>();

                foreach (var item in listItems)
                {
                    newList.LAListItems.Add(item);
                }

                lAList.Add(newList);
            }

            return View(lAList);
        }

        // GET: LALists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;
            JObject jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);


            LAList lAList = jsonObj.ToObject<LAList>();
            List<LAListItem> listItems = jsonObj["ShoppingListItems"].ToObject<List<LAListItem>>();

            foreach (LAListItem item in listItems)
            {
                lAList.LAListItems.Add(item);
            }

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
            HttpResponseMessage responseMsg = this.newClient.PostAsync("api/Lists", new StringContent(lAList.Name)).Result;

            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Edit", lAList);
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

            HttpResponseMessage responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;
            JObject jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
          

            LAList lAList = jsonObj.ToObject<LAList>();
            List<LAListItem> listItems = jsonObj["ShoppingListItems"].ToObject<List<LAListItem>>();

            foreach (LAListItem item in listItems)
            {
                lAList.LAListItems.Add(item);
            }

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
            HttpResponseMessage responseMsg = this.newClient.PutAsync<LAList>("api/Lists", lAList, new System.Net.Http.Formatting.JsonMediaTypeFormatter()).Result;

            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
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

            HttpResponseMessage responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;
            LAList lAList = responseMsg.Content.ReadAsAsync<LAList>().Result;

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
            HttpResponseMessage responseMsg = this.newClient.DeleteAsync("api/Lists/" + id).Result;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveListItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage responseMsg = this.newClient.GetAsync("api/ListItems/" + id).Result;
            LAListItem lAListItem = responseMsg.Content.ReadAsAsync<LAListItem>().Result;

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
            //int listID = ListQueries.DeleteItemFromList(id);

            return RedirectToAction("Edit");
        }

        public ActionResult AddListItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LAListItem lAListItem = new LAListItem() { ListID = (int)id };
            
            return View("AddListItem");
        }

        [HttpPost, ActionName("AddListItem")]
        [ValidateAntiForgeryToken]
        public ActionResult AddListItem([Bind(Include = "ListID,Description")]LAListItem lAListItem)
        {
            if ( ModelState.IsValid )
            {
                //ListQueries.AddItemToList(lAListItem);

                return RedirectToAction("Edit", new { id = lAListItem.ListID });
            }

            return View("AddListItem", lAListItem);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
