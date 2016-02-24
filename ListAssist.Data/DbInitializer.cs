using ListAssist.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System;

namespace ListAssist.Data
{
    public class DbInitializer : DropCreateDatabaseAlways<ListAssistContext>
    {
        protected override void Seed(ListAssistContext context)
        {
            var lists = new List<LAList>
            {
                new LAList {ID=1, Name="Groceries" },
                new LAList {ID=2, Name="Christmas" }
            };            

            lists.ForEach(s => context.LALists.Add(s));
            context.SaveChanges();

            var listItems = new List<LAListItem>
            {
                new LAListItem {ID=1, Description="Milk", ListID=1, TimesBought = 3, DateAdded = new DateTime(2016, 01, 01) },
                new LAListItem {ID=2, Description="Oranges", ListID=1, TimesBought = 4, DateAdded = new DateTime(2016, 01, 11) },
                new LAListItem {ID=3, Description="Apples", ListID=1, TimesBought = 1, DateAdded = new DateTime(2016, 01, 21) },
                new LAListItem {ID=4, Description="Eggs", ListID=1, TimesBought = 5, DateAdded = new DateTime(2016, 02, 01) },
                new LAListItem {ID=5, Description="Puppy", ListID=2, TimesBought = 2, DateAdded = new DateTime(2016, 02, 11) },
                new LAListItem {ID=6, Description="Skateboard", ListID=2, TimesBought = 1, DateAdded = new DateTime(2016, 01, 07) },
                new LAListItem {ID=7, Description="Choo Choo Train", ListID=2, TimesBought = 2, DateAdded = new DateTime(2016, 01, 13) },
            };
            listItems.ForEach(s => context.LAListItems.Add(s));
            context.SaveChanges();

        }
    }
}

