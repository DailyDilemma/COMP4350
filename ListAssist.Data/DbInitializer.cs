using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data
{
    public class DbInitializer : DropCreateDatabaseAlways<ListAssistContainer>
    {
        protected override void Seed(ListAssistContainer context)
        {
            var lists = new List<List>
            {
                new List {Id=1, Name="Groceries" },
                new List {Id=2, Name="Christmas" }
            };            

            lists.ForEach(s => context.Lists.Add(s));
            context.SaveChanges();

            var listItems = new List<ListItem>
            {
                new ListItem {Id=1, Description="Milk", ListId=1 },
                new ListItem {Id=2, Description="Oranges", ListId=1 },
                new ListItem {Id=3, Description="Apples", ListId=1 },
                new ListItem {Id=4, Description="Eggs", ListId=1 },
                new ListItem {Id=5, Description="Puppy", ListId=2 },
                new ListItem {Id=6, Description="Skateboard", ListId=2 },
                new ListItem {Id=7, Description="Choo Choo Train", ListId=2 },
            };
            listItems.ForEach(s => context.ListItems.Add(s));
            context.SaveChanges();

        }
    }
}

