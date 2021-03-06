﻿using System.Collections.Generic;
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
        ListAssistContext db = new ListAssistContext();

        public LAListsController()
        {
            this.newClient = new HttpClient();
#if DEBUG
            // When in Debug mode, use the dev server api address
            newClient.BaseAddress = new System.Uri("http://localhost:3693/");
#else
            // When in Release mode, use the production server api address
            newClient.BaseAddress = new System.Uri("http://localhost:8080/");
#endif
            newClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: LALists
        public ActionResult Index()
        {
            HttpResponseMessage responseMsg = null;
            JArray jsonArray = null;
            List<LAList> lAList = null;
            LAList newList = null;
            List<LAListItem> listItems = null;

            responseMsg = this.newClient.GetAsync("api/Lists/").Result;
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                jsonArray = JArray.Parse(responseMsg.Content.ReadAsStringAsync().Result);

                lAList = new List<LAList>();

                foreach (var jsonItem in jsonArray)
                {
                    newList = jsonItem.ToObject<LAList>();
                    listItems = jsonItem["ShoppingListItems"].ToObject<List<LAListItem>>();

                    foreach (var item in listItems)
                    {
                        newList.LAListItems.Add(item);
                    }

                    lAList.Add(newList);
                }
                return View(lAList);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: LALists/Details/5
        public ActionResult Details(int? id)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
           // JObject jsonItemObj = null;
            LAList lAList = null;
            List<LAListItem> listItems = null;
            List<LASuggestion> listSuggestions = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;

            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                lAList = jsonObj.ToObject<LAList>();

                if (lAList == null)
                {
                    return HttpNotFound();
                }

                foreach (JObject jsonItemObj in jsonObj["ShoppingListItems"])
                {
                    var item = new LAListItem();
                    item.ID = (int)jsonItemObj["Id"];
                    item.ListID = (int)jsonItemObj["ListId"];
                    item.Done = (bool)jsonItemObj["Checked"];
                    item.Description = (string)jsonItemObj["Description"];
                    lAList.LAListItems.Add(item);
                }

                listSuggestions = jsonObj["ShoppingListSuggestions"].ToObject<List<LASuggestion>>();
                foreach (LASuggestion item in listSuggestions)
                {
                    lAList.LASuggestions.Add(item);
                }

                return View("Details", lAList);
            }
            return View(lAList);
        }

        // GET: LALists/Create
        public ActionResult Create()
        {
            LAList aList = new LAList();
            return View(aList);
        }

        // POST: LALists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] LAList AList)
        {
            if(ModelState.IsValid)
            {
                HttpResponseMessage responseMsg = this.newClient.PostAsync(string.Format("api/Lists/{0}", AList.Name), new StringContent(AList.Name)).Result;

                if (responseMsg.StatusCode == HttpStatusCode.OK)
                {
                    LAList newList = new LAList();
                    newList.Name = AList.Name;
                    newList.ID = responseMsg.Content.ReadAsAsync<int>().Result;

                    return RedirectToAction("Edit", newList);
                }

                return View("Create", AList);
            }

            return View("Create", AList);
        }

        // GET: LALists/Edit/5
        public ActionResult Edit(int? id)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
            LAList lAList = null;
            List<LAListItem> listItems = null;
            List<LASuggestion> listSuggestions = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;
            
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                lAList = jsonObj.ToObject<LAList>();

                if (lAList == null)
                {
                    return HttpNotFound();
                }

                foreach (JObject jsonItemObj in jsonObj["ShoppingListItems"])
                {
                    var item = new LAListItem();
                    item.ID = (int)jsonItemObj["Id"];
                    item.ListID = (int)jsonItemObj["ListId"];
                    item.Done = (bool)jsonItemObj["Checked"];
                    item.Description = (string)jsonItemObj["Description"];
                    lAList.LAListItems.Add(item);
                }

                listSuggestions = jsonObj["ShoppingListSuggestions"].ToObject<List<LASuggestion>>();
                foreach (LASuggestion item in listSuggestions) {
                    lAList.LASuggestions.Add(item);
                }

                return View("Edit", lAList);
            }
            return View(lAList);
        }

        // POST: LALists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LAList lAList)
        {
            HttpResponseMessage responseMsg = null;
            
            responseMsg = this.newClient.PutAsync("api/Lists", new ObjectContent<LAList>(lAList, new System.Net.Http.Formatting.JsonMediaTypeFormatter())).Result;
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", lAList);
        }

        // GET: LALists/Delete/5
        public ActionResult Delete(int? id)
        {
            HttpResponseMessage responseMsg = null;
            LAList lAList = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            responseMsg = this.newClient.GetAsync("api/Lists/" + id).Result;

            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                lAList = responseMsg.Content.ReadAsAsync<LAList>().Result;

                if (lAList == null)
                {
                    return HttpNotFound();
                }

                return View("Delete", lAList);
            }

            return View(lAList);
        }

        // POST: LALists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage responseMsg = null;

            responseMsg = this.newClient.DeleteAsync("api/Lists/" + id).Result;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveListItem(int? listID, int? itemID)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
            LAListItem lAListItem = null;

            if (listID == null || itemID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            responseMsg = this.newClient.GetAsync(string.Format("api/ListItems/list/{0}/item/{1}", listID, itemID)).Result;
            if(responseMsg.StatusCode == HttpStatusCode.OK)
            {
                jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                lAListItem = jsonObj.ToObject<LAListItem>();

                if (lAListItem == null)
                {
                    return HttpNotFound();
                }
            }

            return View("RemoveListItem", lAListItem);
        }

        [HttpPost, ActionName("RemoveListItem")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveListItem(int listID, int itemID)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
            LAListItem lAListItem = null;
            LAList lAList = null;
            List<LAListItem> listItems = null;

            responseMsg = this.newClient.GetAsync(string.Format("api/ListItems/list/{0}/item/{1}", listID, itemID)).Result;
            
            if(responseMsg.StatusCode == HttpStatusCode.OK)
            {
                jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                lAListItem = jsonObj.ToObject<LAListItem>();

                responseMsg = this.newClient.DeleteAsync(string.Format("api/ListItems/list/{0}/item/{1}", listID, itemID)).Result;
                if (responseMsg.StatusCode == HttpStatusCode.OK)
                {
                    responseMsg = this.newClient.GetAsync(string.Format("api/Lists/{0}", listID)).Result;
                    if (responseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                        lAList = jsonObj.ToObject<LAList>();
                        listItems = jsonObj["ShoppingListItems"].ToObject<List<LAListItem>>();

                        foreach (LAListItem item in listItems)
                        {
                            lAList.LAListItems.Add(item);
                        }
                        return RedirectToAction("Edit", lAList);
                    }
                }
            }
            return View("RemoveListItem", lAListItem);
        }

        public ActionResult AddListItem(int? id)
        {
            LAListItem lAListItem = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lAListItem = new LAListItem() { ListID = (int)id };

            return View("AddListItem", lAListItem);
        }

        [HttpPost, ActionName("AddListItem")]
        [ValidateAntiForgeryToken]
        public ActionResult AddListItem([Bind(Include = "ListID,Description")]LAListItem lAListItem)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
            LAList lAList = null;
            List<LAListItem> listItems = null;

            responseMsg = this.newClient.PostAsync("api/ListItems", new ObjectContent<LAListItem>(lAListItem, new System.Net.Http.Formatting.JsonMediaTypeFormatter())).Result;
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                responseMsg = this.newClient.GetAsync(string.Format("api/Lists/{0}", lAListItem.ListID)).Result;
                if (responseMsg.StatusCode == HttpStatusCode.OK)
                {
                    jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
                    lAList = jsonObj.ToObject<LAList>();
                    listItems = jsonObj["ShoppingListItems"].ToObject<List<LAListItem>>();

                    foreach (LAListItem item in listItems)
                    {
                        lAList.LAListItems.Add(item);
                    }
                    return RedirectToAction("Edit", lAList);
                }
            }

            return View("AddListItem", lAListItem);
        }

        [HttpPost, ActionName("AcceptSuggestion")]
        public ActionResult AcceptSuggestion(int suggestionId)
        {
            HttpResponseMessage responseMsg = null;
            JObject jsonObj = null;
            LAList lAList = null;
            List<LAListItem> listItems = null;
            List<LASuggestion> listSuggestions = null;
            HttpContent content = null;
               
        responseMsg = this.newClient.PostAsync("api/AcceptSuggestion/" + suggestionId, content).Result;
        if (responseMsg.StatusCode == HttpStatusCode.OK)
        {
            jsonObj = JObject.Parse(responseMsg.Content.ReadAsStringAsync().Result);
            lAList = jsonObj.ToObject<LAList>();
            listItems = jsonObj["ShoppingListItems"].ToObject<List<LAListItem>>();
            listSuggestions = jsonObj["ShoppingListSuggestions"].ToObject<List<LASuggestion>>();
        }

        return Json(new { url = Request.UrlReferrer.AbsoluteUri });

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
