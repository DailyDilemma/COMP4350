namespace ListAssist.WebAPI.Models
{
    public class ShoppingListSuggestion
    {
        /// <summary>
        /// Id for database purposes
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Id of the shopping list this suggestion belongs to.
        /// </summary>
        public int ListId
        {
            get; set;
        }

        /// <summary>
        /// Description of this item.
        /// </summary>
        public string Description
        {
            get; set;
        }
    }
}