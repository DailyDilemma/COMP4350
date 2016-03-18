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
        /// Id for database purposes
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Id of the shopping list this item belongs to.
        /// </summary>
        public int ListId
        {
            get; set;
        }
    }
}