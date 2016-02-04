using System;
using ListAssist.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.Data.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestLAList()
        {
            LAList testList = new LAList() { ID = 1, Name = "Test List" };

            int id = testList.ID;
            Assert.AreEqual<int>(1, id);

            string name = testList.Name;
            Assert.IsNotNull(name);
            Assert.AreEqual<string>("Test List", name);
        }

        [TestMethod]
        public void TestLAListItem()
        {
            LAListItem testListItem = new LAListItem() { ID = 1, Description = "Test List Item", Done = true };

            int id = testListItem.ID;
            Assert.AreEqual<int>(1, id);

            string description = testListItem.Description;
            Assert.IsNotNull(description);
            Assert.AreEqual<string>("Test List Item", description);

            Boolean done = testListItem.Done;
            Assert.IsTrue(done);
        }
    }
}
