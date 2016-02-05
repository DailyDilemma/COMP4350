﻿using System.Collections.Generic;

namespace ListAssist.WebAPI.Models
{
    public class ShoppingList
    {
        public ShoppingList()
        {
            ShoppingListItems = new List<ShoppingListItem>();
        }

        /// <summary>
        /// Name of the shopping list.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// List of all the items belonging to this shopping list.
        /// </summary>
        public virtual ICollection<ShoppingListItem> ShoppingListItems
        {
            get; set;
        }

        /// <summary>
        /// Id used for database purposes.
        /// </summary>
        private int Id
        {
            get; set;
        }
    }
}