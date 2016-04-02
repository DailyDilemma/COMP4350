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
        [TestInitialize]
        public void TestInitialize()
        {
            Database.SetInitializer(new DbInitializer());
            var db = new ListAssistContext(new DbConStringBuilder("ListAssist").getConnectionString());
            db.Database.Initialize(true);
        }

        [TestMethod]
        public void TestBlankLAList()
        {
            LAList testList3 = new LAList() { ID = 50, Name = "Blank List" };
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
            Assert.AreEqual(2, lists.Count);
        }

        [TestMethod]
        public void TestGetSingleList()
        {
            var list = ListQueries.GetList(1);

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.ID);
            Assert.AreEqual(true, list.Name.Equals("Groceries"));
            Assert.AreEqual(4, list.LAListItems.Count);
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

            ListQueries.AddList(lists[0]);
            var result = ListQueries.GetList(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.LAListItems.Count, 0, "Number of list items is not 0");

            var findResult = ListQueries.GetList(3);

            Assert.IsNotNull(findResult);
            Assert.AreEqual<string>("Test List 4", findResult.Name);

            ListQueries.RemoveList(findResult.ID);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestAddListWithItems()
        {
            var newList = new LAList() { Name = "List with items" };
            newList.LAListItems.Add(new LAListItem() { Description = "Test Item 1", Done = true });
            newList.LAListItems.Add(new LAListItem() { Description = "Test Item 2", Done = false });

            var result = ListQueries.AddList(newList);

            Assert.AreEqual(true, result);
            Assert.AreEqual(3, ListQueries.GetLists().Count);

            Assert.IsNotNull(newList);
            Assert.AreEqual(2, newList.LAListItems.Count);

            ListQueries.RemoveList(newList.ID);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void AddDuplicateList()
        {
            var newList = new LAList() { Name = "Groceries" };
            var result = ListQueries.AddList(newList);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void AddListWithNullName()
        {
            var newList = new LAList();
            var result = ListQueries.AddList(newList);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestRemoveList()
        {
            LAList testList = new LAList() { Name = "List to Remove" };
            ListQueries.AddList(testList);

            var findList = ListQueries.GetList(testList.ID);
            Assert.IsNotNull(findList, "Test List not found.");
            Assert.AreEqual<string>("List to Remove", findList.Name);

            var result = ListQueries.RemoveList(findList.ID);

            Assert.AreEqual(true, result);
            Assert.IsNull(ListQueries.GetList(3), "List 3 not removed.");
        }

        [TestMethod]
        public void TestRemoveNonExistingList()
        {
            var result = ListQueries.RemoveList(99);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestRemoveBadListID()
        {
            var result = ListQueries.RemoveList(-11);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestRemoveZeroListID()
        {
            var result = ListQueries.RemoveList(0);

            Assert.AreEqual(false, result);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestAddListItem()
        {
            LAList testList = new LAList() { Name = "List to Remove" };
            ListQueries.AddList(testList);

            Assert.IsNotNull(ListQueries.GetList(testList.ID));
            Assert.AreEqual(0, ListQueries.GetList(testList.ID).LAListItems.Count);

            var newItem = new LAListItem()
            {
                Description = "Test Item",
                Done = true,
                ListID = testList.ID
            };

            var result = ListQueries.AddItemToList(newItem);
            var foundList = ListQueries.GetList(testList.ID);

            Assert.AreEqual(true, result);
            Assert.IsNotNull(foundList);
            Assert.AreEqual(1, foundList.LAListItems.Count);

            ListQueries.RemoveList(foundList.ID);
            Assert.AreEqual(2, ListQueries.GetLists().Count);
        }

        [TestMethod]
        public void TestAddDuplicateItem()
        {
            var newItem = new LAListItem()
            {
                Description = "Milk",
                ListID = 1
            };

            var result = ListQueries.AddItemToList(newItem);

            Assert.AreEqual(false, result);
            Assert.AreEqual(4, ListQueries.GetList(1).LAListItems.Count);
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
        }

        [TestMethod]
        public void TestAddItemToNoList()
        {
            var newItem = new LAListItem() { Description = "Test Insert 0", Done = false };
            var result = ListQueries.AddItemToList(newItem);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRemoveListItem()
        {
            LAList testList = ListQueries.GetList(1);

            Assert.IsNotNull(testList);

            Assert.IsTrue(ListQueries.AddItemToList(new LAListItem() { Description = "Bread", ListID = 1 }));

            testList = ListQueries.GetList(1);
            Assert.IsNotNull(testList);
            Assert.AreEqual(5, testList.LAListItems.Count);

            var result = ListQueries.DeleteItemFromList(testList.LAListItems[4].ID);

            testList = ListQueries.GetList(1);
            Assert.IsNotNull(testList);

            Assert.AreEqual(1, result);
            Assert.AreEqual(4, testList.LAListItems.Count);
        }

        [TestMethod]
        public void TestRemoveNonExistantItem()
        {
            LAList testList = ListQueries.GetList(1);
            var itemCount = testList.LAListItems.Count;

            var result = ListQueries.DeleteItemFromList(99);

            testList = ListQueries.GetList(1);
            Assert.AreEqual(-1, result);
            Assert.AreEqual(itemCount, testList.LAListItems.Count);
        }

        [TestMethod]
        public void TestRemoveItemBadId()
        {
            LAList testList = ListQueries.GetList(1);
            var itemCount = testList.LAListItems.Count;

            var result = ListQueries.DeleteItemFromList(-99);

            testList = ListQueries.GetList(1);
            Assert.AreEqual(-1, result);
            Assert.AreEqual(itemCount, testList.LAListItems.Count);
        }

        [TestMethod]
        public void TestUpdateListName()
        {
            LAList testList = ListQueries.GetList(1);
            Assert.IsNotNull(testList);

            testList.Name = "Updated Name";

            var result = ListQueries.UpdateList(testList);
            testList = ListQueries.GetList(1);

            Assert.AreEqual(true, result);
            Assert.AreEqual("Updated Name", testList.Name);
            Assert.AreEqual(4, testList.LAListItems.Count);

            testList.Name = "Groceries";
            var restoreName = ListQueries.UpdateList(testList);

            testList = ListQueries.GetList(1);

            Assert.AreEqual(true, restoreName);
            Assert.AreEqual("Groceries", testList.Name);
        }

        [TestMethod]
        public void TestUpdateListNameNull()
        {
            LAList testList = ListQueries.GetList(1);

            Assert.IsNotNull(testList);

            testList.Name = null;

            var result = ListQueries.UpdateList(testList);

            testList = ListQueries.GetList(1);
            testList.Name = "Groceries";
            Assert.AreEqual(false, result);
            Assert.IsNotNull(testList);
            Assert.AreEqual("Groceries", testList.Name);
        }

        [TestMethod]
        public void TestUpdateListAddNullItems()
        {
            LAList testList = ListQueries.GetList(1);

            Assert.IsNotNull(testList);

            testList.LAListItems.Add(null);

            var result = ListQueries.UpdateList(testList);

            testList = null;
            testList = ListQueries.GetList(1);

            Assert.AreEqual(true, result);
            Assert.IsNotNull(testList);
            Assert.AreEqual("Groceries", testList.Name);
            Assert.AreEqual(4, testList.LAListItems.Count);
        }
    }
}
