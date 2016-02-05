using System;
using ListAssist.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.Data.Tests
{
    [TestClass]
    public class DataUnitTest
    {
        private ListAssistContext db;

        [TestInitialize]
        public void TestInitialize()
        {
            LAList testList1 = new LAList() { ID = 30, Name = "Test List 1" };
            LAList testList2 = new LAList() { ID = 40, Name = "Test List 2" };

            LAListItem testInsertItem1 = new LAListItem() { ID = 12, Description = "Test Insert 1", Done = false };
            LAListItem testInsertItem2 = new LAListItem() { ID = 13, Description = "Test Insert 2", Done = false };
            LAListItem testInsertItem3 = new LAListItem() { ID = 14, Description = "Test Insert 3", Done = false };

            Assert.IsNotNull(testList1, "Test list 1 is null.");
            Assert.IsNotNull(testList2, "Test list 2 is null.");

            Assert.IsNotNull(testInsertItem1, "Test insert item 1 is null.");
            testList1.LAListItems.Add(testInsertItem1);
            
            Assert.IsNotNull(testInsertItem2, "Test insert item 2 is null.");
            testList1.LAListItems.Add(testInsertItem2);

            Assert.IsNotNull(testInsertItem3, "Test insert item 3 is null,");
            testList2.LAListItems.Add(testInsertItem3);

            Database.SetInitializer(new DbInitializer());
            this.db = new ListAssistContext();

            this.db.Database.Initialize(true);
            Assert.IsNotNull(this.db, "List assist context is null.");

            var lists = new List<LAList>
            {
                testList1,
                testList2,
            };

                
            foreach (LAList newList in lists)
            {
                this.db.LALists.Add(newList);
                foreach (LAListItem newListItem in newList.LAListItems)
                {
                    this.db.LAListItems.Add(newListItem);
                }
            }

            this.db.SaveChanges();

            Assert.IsNotNull(this.db.LALists.Find(30), "List with ID = 30 not found.");
        }

        [TestMethod]
        public void TestBlankLAList()
        {
            LAList testList3 = new LAList() { ID = 50, Name = "Test List 3" };
            LAListItem testInsertItem = new LAListItem() { ID = 10, Description = "Test Insert", Done = false };

            Assert.IsNotNull(testInsertItem, "Test insert item is null.");

            testList3.LAListItems.Add(testInsertItem);
            Assert.AreEqual<int>(1, testList3.LAListItems.Count, "List item not added.");
            testList3.LAListItems.Remove(testInsertItem);
            Assert.AreEqual<int>(0, testList3.LAListItems.Count, "List item not removed.");
        }

        [TestMethod]
        public void TestAddListToDB()
        {
            var lists = new List<LAList>
            {
                new LAList { ID = 60, Name = "Test List 4" }
            };
                
            // add the lists to the database along with their associated list items
            foreach ( LAList newList in lists ){
                this.db.LALists.Add(newList);
                foreach (LAListItem newListItem in newList.LAListItems)
                {
                    this.db.LAListItems.Add(newListItem);
                }
            }
            this.db.SaveChanges();
            Assert.AreEqual<string>(this.db.LALists.Find(60).Name, "Test List 4", "Test List 4 not found.");
            Assert.AreEqual<int>(this.db.LALists.Find(60).LAListItems.Count, 0, "Number of list items is not 0.");
        }

        [TestMethod]
        public void TestRemove()
        {
            Assert.AreEqual<int>(2, this.db.LALists.Find(30).LAListItems.Count, "Test list 1 does not contain 2 list items.");
            this.db.LALists.Find(30).LAListItems.Remove(new LAListItem() { ID = 12, Description = "Test Insert 1", Done = false });
            Assert.AreEqual<int>(2, this.db.LALists.Find(30).LAListItems.Count, "Test list still contains 3 items.");

            Assert.IsNotNull(db.LALists.Find(30), "List 3 not found.");
            this.db.LALists.Remove(this.db.LALists.Find(30));
            this.db.SaveChanges();
            Assert.IsNull(this.db.LALists.Find(30), "List 3 not removed.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            Assert.AreEqual<int>(1, this.db.LALists.Find(40).LAListItems.Count, "Test list 2 does not contain 1 list item.");
            IEnumerator<LAListItem> itemList1 = this.db.LALists.Find(40).LAListItems.GetEnumerator();
            Assert.IsNotNull(itemList1.MoveNext(), "Item not found in test list 2.");
            itemList1.Current.Description = "Update Test";
            this.db.SaveChanges();

            Assert.AreEqual<int>(1, this.db.LALists.Find(40).LAListItems.Count, "Test list 2 does not contain 1 list item.");
            IEnumerator<LAListItem> itemList2 = this.db.LALists.Find(40).LAListItems.GetEnumerator();
            Assert.IsNotNull(itemList2.MoveNext(), "Item not found in test list 2.");
            Assert.AreEqual<string>("Update Test", itemList2.Current.Description);
        }
    }
}
