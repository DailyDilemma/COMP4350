﻿using System;
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
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LAListItem> LAListItems { get; set; }
    }
}