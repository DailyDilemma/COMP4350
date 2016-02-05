using ListAssist.WebAPI.Helpers;
using ListAssist.WebAPI.Models;
using System.Net;
using System.Web.Http;

namespace ListAssist.WebAPI.Controllers
{
    public class ListItemsController : ApiController
    {
        /// <summary>
        /// Add a new item to a list.
        /// </summary>
        /// <remarks>
        /// Add a new item to your shopping list.
        /// </remarks>
        /// <param name="listId">The id of the list the item is being added to.</param>
        /// <param name="item">The item being added to the list.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to add item to list.</response>
        [HttpPut]
        public HttpStatusCode AddItemToList(int listId, ShoppingListItem item)
        {
            if (ListQueries.AddItemToList(listId, item))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
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
        [HttpPost]
        public HttpStatusCode RemoveItemFromList(int itemId, int listId)
        {
            if (ListQueries.RemoveItemFromList(listId, itemId))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
