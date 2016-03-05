using System;
using ListAssist.Data.Models;
using ListAssist.Data;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.Data.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new DbInitializer());
            var db = new ListAssistContext();
            db.Database.Initialize(false);
        }

        [TestMethod]
        public void TestConnectionStringBuilder()
        {
            DbConStringBuilder builder = new Data.DbConStringBuilder("TestDatabaseName");
            Assert.AreEqual<string>(
                @"Data Source=(localdb)\MSSQLLocalDB;initial catalog=TestDatabaseName;integrated security=True".ToLower(), 
                builder.getConnectionString().ToLower());
        }

        [TestMethod]
        public void TestLAList()
        {
            LAList testList = new LAList() { Name = "Test List" };

            int id = testList.ID;
            Assert.IsNotNull(id);
            
            string name = testList.Name;
            Assert.IsNotNull(name);
            Assert.AreEqual<string>("Test List", name);
        }

        [TestMethod]
        public void TestLAListItem()
        {
            LAListItem testListItem = new LAListItem() { Description = "Test List Item", Done = true };

            int id = testListItem.ID;
            Assert.IsNotNull(id);

            string description = testListItem.Description;
            Assert.IsNotNull(description);
            Assert.AreEqual<string>("Test List Item", description);

            Boolean done = testListItem.Done;
            Assert.IsTrue(done);
        }

        [TestMethod]
        public void TestAddListItem()
        {
            LAList testList = new LAList { Name = "Test List 1" };
            Assert.IsNotNull(testList);

            LAListItem testListItem = new LAListItem { Description = "Test Item 1", Done = true };
            Assert.IsNotNull(testListItem);

            Assert.AreEqual<int>(0, testList.LAListItems.Count);
            testList.LAListItems.Add(testListItem);
            Assert.AreEqual<int>(1, testList.LAListItems.Count);

            testList = null;
            testListItem = null;
        }

        [TestMethod]
        public void TestRemoveListItem()
        {
            LAList testList = new LAList { Name = "Test List 2" };
            Assert.IsNotNull(testList);

            LAListItem testListItem = new LAListItem { Description = "Test Item 2", Done = false };
            Assert.IsNotNull(testListItem);

            Assert.AreEqual<int>(0, testList.LAListItems.Count);
            testList.LAListItems.Add(testListItem);
            Assert.AreEqual<int>(1, testList.LAListItems.Count);

            testList.LAListItems.Remove(testListItem);
            Assert.AreEqual<int>(0, testList.LAListItems.Count);

            testList = null;
            testListItem = null;
        }

        [TestMethod]
        public void TestEditListItem()
        {
            LAList testList = new LAList { Name = "Test List 3" };
            Assert.IsNotNull(testList);

            LAListItem testListItem = new LAListItem { Description = "Test Item 3", Done = false };
            Assert.IsNotNull(testListItem);

            Assert.AreEqual<int>(0, testList.LAListItems.Count);
            testList.LAListItems.Add(testListItem);
            Assert.AreEqual<int>(1, testList.LAListItems.Count);

            LAListItem updateListItem = testList.LAListItems.Find(x => x.ID == 0);
            Assert.IsNotNull(updateListItem);
            updateListItem.Done = true;
            updateListItem = null;

            LAListItem foundListItem = testList.LAListItems.Find(x => x.ID == 0);
            Assert.IsNotNull(foundListItem);
            Assert.IsTrue(foundListItem.Done);
            foundListItem = null;

            testList = null;
            testListItem = null;
        }
    }
}
