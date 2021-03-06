﻿using ListAssist.WebAPI.Queries;
using ListAssist.WebAPI.Models;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ListAssist.WebAPI.Controllers
{
    public class ListsController : ApiController
    {
        private ListQueries listQueries;

        public ListsController()
        {
            this.listQueries = new ListQueries();
        }

        /// <summary>
        /// Move a suggestion into the list
        /// </summary>
        /// <remarks>
        /// Move a suggestion into the list
        /// </remarks>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to accept suggestion.</response>
        [HttpPost]
        [ResponseType(typeof(List<ShoppingList>))]
        public HttpResponseMessage AcceptSuggestion(int suggestionId)
        {
            var result = listQueries.AcceptSuggestion(suggestionId);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
        }

        /// <summary>
        /// Retrieve all existing shopping lists from the database
        /// </summary>
        /// <remarks>
        /// Get a list of all shopping lists
        /// </remarks>
        /// <returns>List of ShoppingList items</returns>
        /// <response code="200">Success.</response>
        [HttpGet]
        [ResponseType(typeof(List<ShoppingList>))]
        public HttpResponseMessage AllLists()
        {
            var test = listQueries.GetLists();

            return Request.CreateResponse(HttpStatusCode.OK, listQueries.GetLists());
        }

        /// <summary>
        /// Add a new list to the database.
        /// </summary>
        /// <remarks>
        /// Add a new shopping list.
        /// </remarks>
        /// <param name="listName">The name of the new list.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Internal Error. Please try again.</response>
        [HttpPost]
        public HttpResponseMessage AddList(string listName)
        {
            int insertedId = listQueries.AddList(listName);

            if(insertedId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, insertedId);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Removes the selected list by id.
        /// </summary>
        /// <remarks>
        /// Deletes the selected list.
        /// </remarks>
        /// <param name="listId">The id of the list being deleted.</param>
        /// <response code="200">Success.</response>
        /// <response code="404">List not found.</response>
        [HttpDelete]
        public HttpStatusCode RemoveList(int listId)
        {
            if (listQueries.RemoveList(listId))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Retrieves a single list from the database.
        /// </summary>
        /// <remarks>
        /// Find and retrieve a list from the database by it's id.
        /// </remarks>
        /// <param name="listId">The id of the list being selected.</param>
        /// <returns>The shopping list that was found in the database.</returns>
        /// <response code="404">Unable to find list with that id.</response>
        /// <response code="200">Success.</response>
        [HttpGet]
        [ResponseType(typeof(ShoppingList))]
        public HttpResponseMessage SingleList(int listId)
        {
            var result = listQueries.GetList(listId);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Update a list including the name and list item descriptions.
        /// </summary>
        /// <remarks>
        /// Change the name of a shopping list or descriptions of list items.
        /// </remarks>
        /// <response code="200">Success.</response>
        /// <response code="500">Internal Error. Please try again.</response>
        [HttpPut]
        public HttpStatusCode UpdateList()
        {
            JObject jsonObj = null;
            ShoppingList newList = null;
            ShoppingListItem newItem = null;

            jsonObj = JObject.Parse(Request.Content.ReadAsStringAsync().Result);
            newList = new ShoppingList();
            newList.Id = (int)jsonObj["ID"];
            newList.Name = jsonObj["Name"].ToString();

            foreach (var jsonItem in jsonObj["LAListItems"].Children())
            {
                newItem = new ShoppingListItem();
                newItem.Id = (int)jsonItem["ID"];
                newItem.ListId = (int)jsonItem["ListID"];
                newItem.Description = jsonItem["Description"].ToString();
                newItem.Checked = (bool)jsonItem["Done"];

                newList.ShoppingListItems.Add(newItem);
            }

            var result = listQueries.UpdateList(newList.Id, newList.Name);

            if(result)
            {
                foreach (var item in newList.ShoppingListItems)
                {
                    if (!listQueries.UpdateItemFromList(item))
                    {
                        return HttpStatusCode.InternalServerError;
                    }
                }

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
