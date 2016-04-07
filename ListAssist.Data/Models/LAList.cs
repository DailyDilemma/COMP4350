using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data.Models
{
    public class LAList
    {
        public LAList()
        {
            this.LAListItems = new List<LAListItem>();
            this.LASuggestions = new List<LASuggestion>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage ="Please input a name")]
        public string Name { get; set; }
        public virtual List<LAListItem> LAListItems { get; set; }
        public virtual List<LASuggestion> LASuggestions { get; set; }
    }
}
