using System;

namespace ListAssist.WebAPI.Models
{
    public class ShoppingListItem
    {
        /// <summary>
        /// Description of this item.
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Indicator for whether someone has already purchased this item.
        /// </summary>
        public bool Checked
        {
            get; set;
        }

        /// <summary>
        /// The frequency this item is checked. (Times checked/Days since it was added)
        /// </summary>
        public double Frequency
        {
            get
            {
                return TimesBought / (DateTime.Now - DateAdded).TotalDays;
            }
        }

        /// <summary>
        /// Id for database purposes
        /// </summary>
        private int Id
        {
            get; set;
        }

        /// <summary>
        /// Id of the shopping list this item belongs to.
        /// </summary>
        private int ListId
        {
            get; set;
        }

        /// <summary>
        /// The number of times this item has been checked since it was added to the list.
        /// Read only.
        /// </summary>
        public int TimesBought
        {
            get; private set;
        }

        /// <summary>
        /// The date the item was added to it's list.
        /// Read only.
        /// </summary>
        public DateTime DateAdded
        {
            get; private set;
        }

        public void setDateAdded(DateTime date)
        {
            this.DateAdded = date;
        }

        public void setTimesBought(int times)
        {
            this.TimesBought = times;
        }
    }
}