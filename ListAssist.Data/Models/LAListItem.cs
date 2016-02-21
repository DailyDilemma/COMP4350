using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data.Models
{
    public class LAListItem
    {           
        public int ID { get; set; }
        public int ListID { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
