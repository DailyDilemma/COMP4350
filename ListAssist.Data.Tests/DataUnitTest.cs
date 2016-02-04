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
        private LAList testList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.testList = new LAList() { ID = 3, Name = "Test List 1" };
            Assert.IsNotNull(testList, "Test list is null.");
        }

        [TestMethod]
        public void TestBlankLAList()
        {

            LAListItem testInsertItem = new LAListItem();
            testInsertItem.ID = 10;
            testInsertItem.Description = "Test Insert";
            testInsertItem.Done = false;
            Assert.IsNotNull(testInsertItem, "Test insert item is null.");

            this.testList.LAListItems.Add(testInsertItem);
            Assert.AreEqual<int>(1, testList.LAListItems.Count, "List item not added.");
            this.testList.LAListItems.Remove(testInsertItem);
            Assert.AreEqual<int>(0, testList.LAListItems.Count, "List item not removed.");
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
                    this.testList,
                    new LAList { ID = 4, Name = "Test List 2" }
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
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testList = null;
        }
    }
}
