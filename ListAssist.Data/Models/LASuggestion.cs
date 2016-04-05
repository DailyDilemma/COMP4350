using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data.Models
{
    public class LASuggestion
    {
        public int ID { get; set; }        
        public int ListID { get; set; }
        public string Description { get; set; }

        public virtual LAList LAList { get; set; }
    }
}
