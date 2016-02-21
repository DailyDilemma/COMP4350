using ListAssist.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data
{
    public class DbInitializer : DropCreateDatabaseAlways<ListAssistContext>
    {
        protected override void Seed(ListAssistContext context)
        {         
            context.LALists.Add(new LAList { Name = "Groceries" });
            context.LALists.Add(new LAList { Name = "Christmas" });
            context.SaveChanges();

            context.LALists.Find(1).LAListItems.Add(new LAListItem { Description = "Milk", ListID = 1 });
            context.LALists.Find(1).LAListItems.Add(new LAListItem { Description = "Oranges", ListID = 1 });
            context.LALists.Find(1).LAListItems.Add(new LAListItem { Description = "Apples", ListID = 1 });
            context.LALists.Find(1).LAListItems.Add(new LAListItem { Description = "Eggs", ListID = 1 });
            context.LALists.Find(2).LAListItems.Add(new LAListItem { Description = "Puppy", ListID = 2 });
            context.LALists.Find(2).LAListItems.Add(new LAListItem { Description = "Skateboard", ListID = 2 });
            context.LALists.Find(2).LAListItems.Add(new LAListItem { Description = "Choo Choo Train", ListID = 2 });
            context.SaveChanges();

        }
    }
}

