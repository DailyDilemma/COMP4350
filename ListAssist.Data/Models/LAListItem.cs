using System;

namespace ListAssist.Data.Models
{
    public class LAListItem
    {           
        public int ID { get; set; }
        public string Description { get; set; }
        public int ListID { get; set; }
        public bool Done { get; set; }
        public int TimesBought { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual LAList LAList { get; set; }
    }
}
