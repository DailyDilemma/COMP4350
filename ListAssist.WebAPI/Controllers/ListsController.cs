using ListAssist.WebAPI.Queries;
using ListAssist.WebAPI.Models;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace ListAssist.WebAPI.Controllers
{
    public class ListsController : ApiController
    {
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
            var test = ListQueries.GetLists();

            return Request.CreateResponse(HttpStatusCode.OK,ListQueries.GetLists());
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
        public HttpStatusCode AddList(string listName)
        {
            if(ListQueries.AddList(listName))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
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
            if (ListQueries.RemoveList(listId))
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
        /// <param name="id">The id of the list being selected.</param>
        /// <returns>The shopping list that was found in the database.</returns>
        /// <response code="404">Unable to find list with that id.</response>
        /// <response code="200">Success.</response>
        [HttpGet]
        [ResponseType(typeof(ShoppingList))]
        public HttpResponseMessage SingleList(int id)
        {
            var result = ListQueries.GetList(id);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Update a list's name in the database.
        /// </summary>
        /// <remarks>
        /// Change the name of a shopping list.
        /// </remarks>
        /// <param name="newName">The new name of the list.</param>
        /// <param name="id">The id of the list being updated.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Internal Error. Please try again.</response>
        [HttpPut]
        public HttpStatusCode UpdateList(int id, string newName)
        {
            var result = ListQueries.UpdateList(id, newName);

            if(result)
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
