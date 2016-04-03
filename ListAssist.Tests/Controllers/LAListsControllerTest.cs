using System;
using ListAssist.Controllers;
using ListAssist.Data.Models;
using ListAssist.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListAssist.Tests.Controllers
{
    [TestClass]
    public class LAListsControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new DbInitializer());
            var db = new ListAssistContext();
            db.Database.Initialize(false);
        }

        [TestMethod]
        public void TestDetails()
        {
            LAListsController testController = new LAListsController();
            var result = testController.Details(1) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);

            var nullResult = testController.Details(10) as ViewResult;
            Assert.IsNull(nullResult.View);
        }

        [TestMethod]
        public void TestCreate()
        {
            LAList testList = new LAList() { ID = 80, Name = "Test List 5" };
            LAListsController testController = new LAListsController();
            var result = testController.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestEdit()
        {
            LAListsController testController = new LAListsController();
            var result = testController.Edit(1) as ViewResult;

            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void TestAddListItem()
        {
            LAListsController testController = new LAListsController();
            var result = testController.AddListItem(new LAListItem { ListID = 1, Description="Test", Done=false}) as RedirectToRouteResult;
            
            Assert.AreEqual("Edit", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestRemoveListItem()
        {
            LAListsController testController = new LAListsController();
            var result = testController.RemoveListItem(1,1) as RedirectToRouteResult;

            Assert.AreEqual("Edit", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestDelete()
        {
            LAListsController testController = new LAListsController();
            var result = testController.Delete(2) as ViewResult;

            Assert.AreEqual("Delete", result.ViewName);
        }
    }
}
