using ListAssist.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ListAssist.Data
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ListAssistContext>
    {
        protected override void Seed(ListAssistContext context)
        {
            var lists = new List<LAList>
            {
                new LAList {Name="Groceries" },
                new LAList {Name="Christmas" }
            };            

            lists.ForEach(s => context.LALists.Add(s));
            context.SaveChanges();

            var listItems = new List<LAListItem>
            {
                new LAListItem {Description="Milk", ListID=1 },
                new LAListItem {Description="Oranges", ListID=1 },
                new LAListItem {Description="Apples", ListID=1 },
                new LAListItem {Description="Eggs", ListID=1 },
                new LAListItem {Description="Puppy", ListID=2 },
                new LAListItem {Description="Skateboard", ListID=2 },
                new LAListItem {Description="Choo Choo Train", ListID=2 }
            };
            listItems.ForEach(s => context.LAListItems.Add(s));
            context.SaveChanges();

            var listSuggestions = new List<LASuggestion>
            {
                new LASuggestion { ListID=1, Description="Bananas" },
                new LASuggestion { ListID=1, Description="Bread" },
                new LASuggestion { ListID=2, Description="Sample Thing" }
            };
            listSuggestions.ForEach(s => context.LASuggestions.Add(s));
            context.SaveChanges();
        }
    }
}

