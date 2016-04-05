using ListAssist.WebAPI.Queries;
using ListAssist.WebAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ListAssist.WebAPI.Controllers
{
    public class ListItemsController : ApiController
    {
        private ListQueries listQueries;

        public ListItemsController()
        {
            this.listQueries = new ListQueries();
        }

        /// <summary>
        /// Add a new item to a list.
        /// </summary>
        /// <remarks>
        /// Add a new item to your shopping list.
        /// </remarks>
        /// <param name="item">The item being added to the list.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to add item to list.</response>
        [HttpPost]
        public HttpStatusCode AddItemToList(ShoppingListItem item)
        {
            if (listQueries.AddItemToList(item))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Gets an item from a selected list.
        /// </summary>
        /// <param name="listId">The ID of the list.</param>
        /// <param name="itemId">The ID of the list item in the selected list.</param>
        /// <response code="200">Success.</response>
        /// <response code="404">List item not found.</response>
        [HttpGet]
        [ResponseType(typeof(ShoppingListItem))]
        public HttpResponseMessage GetItemFromList(int listId, int itemId)
        {
            var result = listQueries.GetItemFromList(listId, itemId);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Set an item's Checked value to true.
        /// </summary>
        /// <remarks>
        /// Set an item's Checked value to true once someone has purchased it.
        /// </remarks>
        /// <param name="itemId">The item that was checked off.</param>
        /// <param name="listId">The list the item belongs to.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to modify item's checked value.</response>
        [HttpPut]
        public HttpStatusCode CheckOffItemFromList(int itemId, int listId)
        {
            if (listQueries.CheckOffItemFromList(listId, itemId))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Delete an existing item from a list.
        /// </summary>
        /// <remarks>
        /// Delete an existing item from an existing shopping list.
        /// </remarks>
        /// <param name="itemId">The id of the item being removed from the list.</param> 
        /// <param name="listId">The id of the list the item is being removed from.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to remove item from list.</response>
        /// <response code="404">Unable to find item in list.</response>
        [HttpDelete]
        public HttpStatusCode DeleteItemFromList(int itemId, int listId)
        {
            if(listQueries.DeleteItemFromList(itemId, listId))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
