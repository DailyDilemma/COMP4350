using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

using ListAssist.Data.Models;
using ListAssist.Data.Queries;

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
            LAList testList1 = new LAList() { Name = "Test List 1" };
            LAList testList2 = new LAList() { Name = "Test List 2" };

            LAListItem testInsertItem1 = new LAListItem() { Description = "Test Insert 1", Done = false };
            LAListItem testInsertItem2 = new LAListItem() { Description = "Test Insert 2", Done = false };
            LAListItem testInsertItem3 = new LAListItem() { Description = "Test Insert 3", Done = false };

            Assert.IsNotNull(testList1, "Test list 1 is null.");
            Assert.IsNotNull(testList2, "Test list 2 is null.");

            Assert.IsNotNull(testInsertItem1, "Test insert item 1 is null.");
            testList1.LAListItems.Add(testInsertItem1);
            
            Assert.IsNotNull(testInsertItem2, "Test insert item 2 is null.");
            testList1.LAListItems.Add(testInsertItem2);

            Assert.IsNotNull(testInsertItem3, "Test insert item 3 is null,");
            testList2.LAListItems.Add(testInsertItem3);

            Database.SetInitializer(new DbInitializer());
            this.db = new ListAssistContext("DataTests");

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
            
            Assert.IsNotNull(this.db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault(), "Test List 1 not found.");
            Assert.IsNotNull(this.db.LALists.Where(e => e.Name.Equals("Test List 2")).FirstOrDefault(), "Test List 2 not found.");
            Assert.IsNull(this.db.LALists.Where(e => e.Name.Equals("Test List 3")).FirstOrDefault(), "Test List 3 found.");
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
        public void TestGetAllLists()
        {
            var lists = ListQueries.GetLists();

            Assert.IsNotNull(lists);
            Assert.AreEqual(4, lists.Count);
        }

        [TestMethod]
        public void TestGetSingleList()
        {
            var list = ListQueries.GetList(3);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.ID);
            Assert.AreEqual(true, list.Name.Equals("Test List 1"));
            Assert.AreEqual(2, list.LAListItems.Count);
        }

        [TestMethod]
        public void TestGetNonExistantList()
        {
            var list = ListQueries.GetList(99);

            Assert.AreEqual(null, list);
        }

        [TestMethod]
        public void TestGetListBadListID()
        {
            var list = ListQueries.GetList(-99);

            Assert.AreEqual(null, list);
        }

        [TestMethod]
        public void TestGetListZeroListID()
        {
            var list = ListQueries.GetList(0);

            Assert.AreEqual(null, list);
        }

        [TestMethod]
        public void TestAddListToDBNoItems()
        {
            var listName = "Test List 4";

            var lists = new List<LAList>
            {
                new LAList { Name = listName }
            };
                
            // add the lists to the database along with their associated list items
            foreach ( LAList newList in lists ){

                ListQueries.AddList(newList);

                foreach (LAListItem newListItem in newList.LAListItems)
                {
                    ListQueries.AddItemToList(newListItem);
                }
            }

            this.db.SaveChanges();

            Assert.IsNotNull(db.LALists.Where(e => e.Name.Equals(listName)).FirstOrDefault());
            Assert.AreEqual(db.LAListItems.Where(e => e.LAList.Name.Equals(listName)).ToList().Count, 0, "Number of list items is not 0");
        }

        [TestMethod]
        public void TestAddListWithItems()
        {
            var newList = new LAList() { Name = "List with items" };
            newList.LAListItems.Add(new LAListItem() { Description = "Test Item 1", Done = true });
            newList.LAListItems.Add(new LAListItem() { Description = "Test Item 2", Done = false });

            var result = ListQueries.AddList(newList);

            Assert.AreEqual(true, result);
            Assert.AreEqual(5, db.LALists.ToList().Count);

            newList = db.LALists.Where(e => e.Name.Equals("List with items")).FirstOrDefault();

            Assert.IsNotNull(newList);
            Assert.AreEqual(2, newList.LAListItems.Count);
        }

        [TestMethod]
        public void AddDuplicateList()
        {
            var newList = new LAList() { Name = "Test List 1" };
            var result = ListQueries.AddList(newList);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, db.LALists.ToList().Count);
        }

        [TestMethod]
        public void AddListWithNullName()
        {
            var newList = new LAList();
            var result = ListQueries.AddList(newList);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, db.LALists.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveList()
        {
            var firstList = db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault();

            Assert.IsNotNull(firstList, "List 3 not found.");

            var result = ListQueries.RemoveList(firstList.ID);

            Assert.AreEqual(true, result);
            Assert.IsNull(db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault(), "List 3 not removed.");
        }

        [TestMethod]
        public void TestRemoveNonExistingList()
        {
            var result = ListQueries.RemoveList(99);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, db.LALists.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveBadListID()
        {
            var result = ListQueries.RemoveList(-11);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, db.LALists.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveZeroListID()
        {
            var result = ListQueries.RemoveList(0);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, db.LALists.ToList().Count);
        }

        [TestMethod]
        public void TestAddListItem()
        {
            var newItem = new LAListItem()
            {
                Description = "Test Item",
                Done = true,
                ListID = 3
            };

            var result = ListQueries.AddItemToList(newItem);

            Assert.AreEqual(true, result);
            Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Item")).FirstOrDefault());
            Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);
        }

        [TestMethod]
        public void TestAddDuplicateItem()
        {
            var newItem = new LAListItem()
            {
                Description = "Test Insert 1",
                ListID = 3
            };

            var result = ListQueries.AddItemToList(newItem);

            Assert.AreEqual(true, result);
            Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Insert 1")).FirstOrDefault());
            Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);

            var item = db.LAListItems.Where(e => e.Description.Equals("Test Insert 1")).FirstOrDefault();

            Assert.IsNotNull(item);
        }

        //[TestMethod]
        //public void TestResetDoneItem()
        //{
        //    var newItem = new LAListItem()
        //    {
        //        Description = "Test Insert 0",
        //        ListID = 3,
        //        Done = true
        //    };

        //    var result = ListQueries.AddItemToList(newItem);

        //    Assert.AreEqual(true, result);
        //    Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Insert 0")).FirstOrDefault());
        //    Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);

        //    result = ListQueries.AddItemToList(newItem);

        //    Assert.AreEqual(true, result);
        //    Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Insert 0")).FirstOrDefault());
        //    Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);

        //    var item = db.LAListItems.Where(e => e.Description.Equals("Test Insert 0")).FirstOrDefault();

        //    Assert.IsNotNull(item);
        //    Assert.AreEqual(false, item.Done);
        //}

        [TestMethod]
        public void TestAddNullItem()
        {
            var result = ListQueries.AddItemToList(null);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
        }

        [TestMethod]
        public void TestAddItemToNoList()
        {
            var newItem = new LAListItem() { Description = "Test Insert 0", Done = false };
            var itemCount = db.LAListItems.ToList().Count;
            var result = ListQueries.AddItemToList(newItem);

            Assert.AreEqual(false, result);
            Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveListItem()
        {
            var item = db.LAListItems.Find(8);
            var itemCount = db.LAListItems.ToList().Count;

            Assert.IsNotNull(item);

            var result = ListQueries.DeleteItemFromList(item.ID);

            Assert.AreEqual(3, result);
            Assert.AreEqual(true, itemCount > db.LAListItems.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveNonExistantItem()
        {
            var itemCount = db.LAListItems.ToList().Count;

            var result = ListQueries.DeleteItemFromList(99);

            Assert.AreEqual(-1, result);
            Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
        }

        [TestMethod]
        public void TestRemoveItemBadId()
        {
            var itemCount = db.LAListItems.ToList().Count;

            var result = ListQueries.DeleteItemFromList(-99);

            Assert.AreEqual(-1, result);
            Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
        }

        [TestMethod]
        public void TestUpdateListName()
        {
            var list = db.LALists.Find(3);

            Assert.IsNotNull(list);
            
            list.Name = "Updated Name";

            var result = ListQueries.UpdateList(list);

            Assert.AreEqual(true, result);
            Assert.AreEqual("Updated Name", db.LALists.Find(3).Name);
            Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
        }

        [TestMethod]
        public void TestUpdateListNameNull()
        {
            var list = db.LALists.Find(3);

            Assert.IsNotNull(list);

            list.Name = null;

            var result = ListQueries.UpdateList(list);

            Assert.AreEqual(false, result);
           // Assert.AreEqual("Test List 1", db.LALists.Find(3).Name);
            Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
        }

        [TestMethod]
        public void TestUpdateListAddNullItems()
        {
            var list = db.LALists.Find(3);

            Assert.IsNotNull(list);

            list.LAListItems = null;

            var result = ListQueries.UpdateList(list);

            Assert.AreEqual(false, result);
            Assert.AreEqual("Test List 1", db.LALists.Find(3).Name);
            //Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
        }
    }
}
