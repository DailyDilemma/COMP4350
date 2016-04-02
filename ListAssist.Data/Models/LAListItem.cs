using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data.Models
{
    public class LAListItem
    {           
        public int ID { get; set; }
        public string Description { get; set; }
        public int ListID { get; set; }
        public bool Done { get; set; }
        public DateTime? LastCheckedOn { get; set; }

        public virtual LAList LAList { get; set; }
    }
}
