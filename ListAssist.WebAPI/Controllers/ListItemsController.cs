﻿using ListAssist.WebAPI.Queries;
using ListAssist.WebAPI.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ListAssist.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2850", headers: "*", methods: "*")]
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
        public HttpStatusCode CheckOffItemFromList(int itemId, int listId)
        {
            if (ListQueries.CheckOffItemFromList(listId, itemId))
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
        /// <param name="listId">The id of the list the item is being removed from.</param>
        /// <param name="item">The item being removed from the list.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Unable to remove item from list.</response>
        /// <response code="404">Unable to find item in list.</response>
        [HttpDelete]
        public HttpStatusCode DeleteItemFromList(int itemId, int listId)
        {
            if(ListQueries.DeleteItemFromList(itemId, listId))
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
