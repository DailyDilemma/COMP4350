using ListAssist.WebAPI.Models;
using ListAssist.WebAPI.Queries;
using ListAssist.WebAPI.MappingProfile;
using ListAssist.Data;
using ListAssist.Data.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.WebAPI.Tests
{
    class IntegrationTests
    {
        [TestClass]
        public class DataUnitTest
        {
            private ListAssistContext db;
            private ListQueries listQueries;

            [TestInitialize]
            public void TestInitialize()
            {
                AutoMapperConfiguration.Configure();

                listQueries = new ListQueries();

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
                this.db = new ListAssistContext();

                this.db.Database.Initialize(false);
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
                ShoppingList testList3 = new ShoppingList() { Name = "Test List 3" };
                ShoppingListItem testInsertItem = new ShoppingListItem() { Description = "Test Insert", Checked = false };

                Assert.IsNotNull(testInsertItem, "Test insert item is null.");

                testList3.ShoppingListItems.Add(testInsertItem);
                Assert.AreEqual<int>(1, testList3.ShoppingListItems.Count, "List item not added.");
                testList3.ShoppingListItems.Remove(testInsertItem);
                Assert.AreEqual<int>(0, testList3.ShoppingListItems.Count, "List item not removed.");
            }

            [TestMethod]
            public void TestGetAllLists()
            {
                var lists = listQueries.GetLists();

                Assert.IsNotNull(lists);
                Assert.AreEqual(4, lists.Count);
            }

            [TestMethod]
            public void TestGetSingleList()
            {
                var list = listQueries.GetList(3);

                Assert.IsNotNull(list);
                Assert.AreEqual(true, list.Name.Equals("Test List 1"));
                Assert.AreEqual(2, list.ShoppingListItems.Count);
            }

            [TestMethod]
            public void TestGetNonExistantList()
            {
                var list = listQueries.GetList(99);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void TestGetListBadListID()
            {
                var list = listQueries.GetList(-99);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void TestGetListZeroListID()
            {
                var list = listQueries.GetList(0);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void AddDuplicateList()
            {
                var newList = "Test List 1";
                var result = listQueries.AddList(newList);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void AddListWithNullName()
            {
                var result = listQueries.AddList(null);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveList()
            {
                var firstList = db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault();

                Assert.IsNotNull(firstList, "List 3 not found.");

                var result = listQueries.RemoveList(firstList.ID);

                Assert.AreEqual(true, result);
                Assert.IsNull(db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault(), "List 3 not removed.");
            }

            [TestMethod]
            public void TestRemoveNonExistingList()
            {
                var result = listQueries.RemoveList(99);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveBadListID()
            {
                var result = listQueries.RemoveList(-11);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveZeroListID()
            {
                var result = listQueries.RemoveList(0);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestAddListItem()
            {
                var newItem = new ShoppingListItem()
                {
                    ListId = 3,
                    Description = "Test Item",
                    Checked = true
                };

                var result = listQueries.AddItemToList(newItem);

                Assert.AreEqual(true, result);
                Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Item")).FirstOrDefault());
                Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestAddDuplicateItem()
            {
                var newItem = new ShoppingListItem()
                {
                    ListId = 3,
                    Description = "Test Insert 1",
                    Checked = false
                };

                var result = listQueries.AddItemToList(newItem);

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
                var result = listQueries.AddItemToList(null);

                Assert.AreEqual(false, result);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestAddItemToNoList()
            {
                var newItem = new ShoppingListItem() { ListId = -1, Description = "Test Insert 0", Checked = false };
                var itemCount = db.LAListItems.ToList().Count;
                var result = listQueries.AddItemToList(newItem);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveListItem()
            {
                var item = db.LAListItems.Find(8);
                var itemCount = db.LAListItems.ToList().Count;

                Assert.IsNotNull(item);

                var result = listQueries.DeleteItemFromList(item.ID, 3);

                Assert.AreEqual(true, result);
                Assert.AreEqual(true, itemCount > db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveNonExistantItem()
            {
                var itemCount = db.LAListItems.ToList().Count;

                var result = listQueries.DeleteItemFromList(99, 3);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveItemBadId()
            {
                var itemCount = db.LAListItems.ToList().Count;

                var result = listQueries.DeleteItemFromList(-99, 3);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestUpdateListName()
            {
                var listName = "Updated Name";

                var result = listQueries.UpdateList(3, listName);

                Assert.AreEqual(true, result);
                Assert.AreEqual("Updated Name", db.LALists.Find(3).Name);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestUpdateListNameNull()
            {
                var result = listQueries.UpdateList(3, null);

                Assert.AreEqual(false, result);
                Assert.AreEqual("Test List 1", db.LALists.Find(3).Name);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }
        }
    }
}
