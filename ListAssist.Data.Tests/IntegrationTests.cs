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
        [TestInitialize]
        public void TestInitialize()
        {
            LAList testList1 = new LAList() { ID = 3, Name = "Test List 1" };
            LAList testList2 = new LAList() { ID = 4, Name = "Test List 2" };

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
            using (var db = new ListAssistContext())
            {
                db.Database.Initialize(false);
                Assert.IsNotNull(db, "List assist context is null.");

                var lists = new List<LAList>
                {
                    testList1,
                    testList2,
                };

                
                foreach (LAList newList in lists)
                {
                    db.LALists.Add(newList);
                    foreach (LAListItem newListItem in newList.LAListItems)
                    {
                        db.LAListItems.Add(newListItem);
                    }
                }
             
                db.SaveChanges();

                Assert.IsNotNull(db.LALists.Find(3), "List with ID = 3 not found.");
            }
        }

        [TestMethod]
        public void TestBlankLAList()
        {
            LAList testList3 = new LAList() { ID = 5, Name = "Test List 3" };
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
            Database.SetInitializer(new DbInitializer());
            using(var db = new ListAssistContext())
            {
                db.Database.Initialize(false);
                Assert.IsNotNull(db, "List assist context is null.");

                var lists = new List<LAList>
                {
                    new LAList { ID = 6, Name = "Test List 4" }
                };
                
                // add the lists to the database along with their associated list items
                foreach ( LAList newList in lists ){
                    db.LALists.Add(newList);
                    foreach (LAListItem newListItem in newList.LAListItems)
                    {
                        db.LAListItems.Add(newListItem);
                    }
                }
                db.SaveChanges();
                Assert.AreEqual<string>(db.LALists.Find(6).Name, "Test List 4", "Test List 4 not found.");
                Assert.AreEqual<int>(db.LALists.Find(6).LAListItems.Count, 0, "Number of list items is not 0.");
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            Database.SetInitializer(new DbInitializer());
            using(var db = new ListAssistContext())
            {
                db.Database.Initialize(false);

                Assert.AreEqual<int>(2, db.LALists.Find(3).LAListItems.Count, "Test list 3 does not contain 2 list items.");
                db.LALists.Find(3).LAListItems.Clear();
                Assert.AreEqual<int>(0, db.LALists.Find(3).LAListItems.Count, "Test list 3 still contains list items.");

                Assert.IsNotNull(db.LALists.Find(3), "List 3 not found.");
                db.LALists.Remove(db.LALists.Find(3));
                db.SaveChanges();
                Assert.IsNull(db.LALists.Find(3), "List 3 not removed.");
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            Database.SetInitializer(new DbInitializer());
            using (var db = new ListAssistContext())
            {
                db.Database.Initialize(false);

                LAListItem listItem1 = db.LAListItems.Find(14);
                Assert.IsNotNull(listItem1, "List Item 1 is null.");
                Assert.AreEqual<int>(2, listItem1.ListID, "List ID doesn't match List Item.");
                Assert.AreEqual<string>("Test Insert 3", listItem1.Description, "List item 3 description is wrong.");
                listItem1.Done = true;
                db.SaveChanges();

                Assert.IsTrue(db.LAListItems.Find(14).Done, "Update of list item failed.");
            }
        }
    }
}
