using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ListAssist.Data.Models
{
    public class LAShoppingList : LAList
    {
        [MaxLength(50,ErrorMessage ="Too long!")]
        [Display(Name="Name of the Store")]
        public string Store { get; set; }
    }
}
