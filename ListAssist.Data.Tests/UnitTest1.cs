using System;
using ListAssist.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.Data.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBlankLAList()
        {
            LAList testList = new LAList();
            Assert.IsNotNull(testList, "Test list is null.");

            LAListItem testInsertItem = new LAListItem();
            testInsertItem.ID = 10;
            testInsertItem.Description = "Test Insert";
            testInsertItem.Done = false;
            Assert.IsNotNull(testInsertItem, "Test insert item is null.");

            testList.LAListItems.Add(testInsertItem);
            Assert.AreEqual<int>(1, testList.LAListItems.Count, "List item not added.");
            testList.LAListItems.Remove(testInsertItem);
            Assert.AreEqual<int>(0, testList.LAListItems.Count, "List item not removed.");
        }

        [TestMethod]
        public void TestPopulatedLAList()
        {
            Database.SetInitializer(new DbInitializer());
            using(var db = new ListAssistContext())
            {
                db.Database.Initialize(false);
                Assert.IsNotNull(db, "List assist context is null.");

                var lists = new List<LAList>
                {
                    new LAList { ID = 3, Name = "Test List 1"},
                    new LAList { ID = 4, Name = "Test List 2" }
                };
                lists.ForEach(s => db.LALists.Add(s));
                db.SaveChanges();
            }
        }
    }
}
