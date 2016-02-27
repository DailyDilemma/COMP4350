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
            private ListQueries queries;
            private ListAssistContext db;

            [TestInitialize]
            public void TestInitialize()
            {
                AutoMapperConfiguration.Configure();

                LAList testList1 = new LAList() { Name = "Test List 1" };
                LAList testList2 = new LAList() { Name = "Test List 2" };

                LAListItem testInsertItem1 = new LAListItem() { Description = "Test Insert 1", Done = false, DateAdded = new System.DateTime(2016, 01, 01), TimesBought = 3 };
                LAListItem testInsertItem2 = new LAListItem() { Description = "Test Insert 2", Done = false, DateAdded = new System.DateTime(2015, 12, 12), TimesBought = 20 };
                LAListItem testInsertItem3 = new LAListItem() { Description = "Test Insert 3", Done = false, DateAdded = new System.DateTime(2016, 01, 11), TimesBought = 10 };

                Assert.IsNotNull(testList1, "Test list 1 is null.");
                Assert.IsNotNull(testList2, "Test list 2 is null.");

                Assert.IsNotNull(testInsertItem1, "Test insert item 1 is null.");
                testList1.LAListItems.Add(testInsertItem1);

                Assert.IsNotNull(testInsertItem2, "Test insert item 2 is null.");
                testList1.LAListItems.Add(testInsertItem2);

                Assert.IsNotNull(testInsertItem3, "Test insert item 3 is null,");
                testList2.LAListItems.Add(testInsertItem3);

                Database.SetInitializer(new DbInitializer());
                this.db = new ListAssistContext("webApiTestsDB");
                this.queries = new ListQueries(db);

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
                var lists = queries.GetLists();

                Assert.IsNotNull(lists);
                Assert.AreEqual(4, lists.Count);
            }

            [TestMethod]
            public void TestGetSingleList()
            {
                var list = queries.GetList(3);

                Assert.IsNotNull(list);
                Assert.AreEqual(true, list.Name.Equals("Test List 1"));
                Assert.AreEqual(2, list.ShoppingListItems.Count);
            }

            [TestMethod]
            public void TestGetNonExistantList()
            {
                var list = queries.GetList(99);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void TestGetListBadListID()
            {
                var list = queries.GetList(-99);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void TestGetListZeroListID()
            {
                var list = queries.GetList(0);

                Assert.AreEqual(null, list);
            }

            [TestMethod]
            public void AddDuplicateList()
            {
                var newList = "Test List 1";
                var result = queries.AddList(newList);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void AddListWithNullName()
            {
                var result = queries.AddList(null);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveList()
            {
                var firstList = db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault();

                Assert.IsNotNull(firstList, "List 3 not found.");

                var result = queries.RemoveList(firstList.ID);

                Assert.AreEqual(true, result);
                Assert.IsNull(db.LALists.Where(e => e.Name.Equals("Test List 1")).FirstOrDefault(), "List 3 not removed.");
            }

            [TestMethod]
            public void TestRemoveNonExistingList()
            {
                var result = queries.RemoveList(99);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveBadListID()
            {
                var result = queries.RemoveList(-11);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveZeroListID()
            {
                var result = queries.RemoveList(0);

                Assert.AreEqual(false, result);
                Assert.AreEqual(4, db.LALists.ToList().Count);
            }

            [TestMethod]
            public void TestAddListItem()
            {
                var newItem = new ShoppingListItem()
                {
                    Description = "Test Item",
                    Checked = true
                };

                var result = queries.AddItemToList(3, newItem);

                Assert.AreEqual(true, result);
                Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Item")).FirstOrDefault());
                Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestAddDuplicateItem()
            {
                var newItem = new ShoppingListItem()
                {
                    Description = "Test Insert 1",
                    Checked = false
                };

                var result = queries.AddItemToList(3, newItem);

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

            //    var result = queries.AddItemToList(newItem);

            //    Assert.AreEqual(true, result);
            //    Assert.IsNotNull(db.LAListItems.Where(e => e.Description.Equals("Test Insert 0")).FirstOrDefault());
            //    Assert.AreEqual(3, db.LALists.Find(3).LAListItems.Count);

            //    result = queries.AddItemToList(newItem);

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
                var result = queries.AddItemToList(3, null);

                Assert.AreEqual(false, result);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestAddItemToNoList()
            {
                var newItem = new ShoppingListItem() { Description = "Test Insert 0", Checked = false };
                var itemCount = db.LAListItems.ToList().Count;
                var result = queries.AddItemToList(-1, newItem);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveListItem()
            {
                var item = db.LAListItems.Find(8);
                var itemCount = db.LAListItems.ToList().Count;

                Assert.IsNotNull(item);

                var result = queries.DeleteItemFromList(item.ID, 3);

                Assert.AreEqual(true, result);
                Assert.AreEqual(true, itemCount > db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveNonExistantItem()
            {
                var itemCount = db.LAListItems.ToList().Count;

                var result = queries.DeleteItemFromList(99, 3);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestRemoveItemBadId()
            {
                var itemCount = db.LAListItems.ToList().Count;

                var result = queries.DeleteItemFromList(-99, 3);

                Assert.AreEqual(false, result);
                Assert.AreEqual(itemCount, db.LAListItems.ToList().Count);
            }

            [TestMethod]
            public void TestUpdateListName()
            {
                var listName = "Updated Name";

                var result = queries.UpdateList(3, listName);

                Assert.AreEqual(true, result);
                //Assert.AreEqual("Updated Name", db.LALists.Find(3).Name);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }

            [TestMethod]
            public void TestUpdateListNameNull()
            {
                var result = queries.UpdateList(3, null);

                Assert.AreEqual(false, result);
                // Assert.AreEqual("Test List 1", db.LALists.Find(3).Name);
                Assert.AreEqual(2, db.LALists.Find(3).LAListItems.Count);
            }
        }
    }
}
