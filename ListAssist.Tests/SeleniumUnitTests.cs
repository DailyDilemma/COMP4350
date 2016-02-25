using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ListAssist.Tests
{
    [TestClass]
    public class SeleniumUnitTests
    {
        static IWebDriver driver;

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /* Scenario 1: BUS - Manage Lists.
        1. View existing lists.
        2. Create a list.
        3. Remove a list.
        */
        [TestMethod]
        public void TestOpenApp()
        {
            var expectedTitle = "Welcome to ListAssist!";

            driver.Navigate().GoToUrl("http://localhost:2850");
            Assert.AreEqual(expectedTitle, driver.Title);
        }

        [TestMethod]
        public void TestCreateNewEmptyList()
        {
            var expectedListName = "Empty List";

            driver.FindElement(By.LinkText("Create")).Click();

            IWebElement element = driver.FindElement(By.Name("Name"));
            element.SendKeys("Empty List");
            element.Submit();

            element = driver.FindElement(By.Name("Name"));
            element.Submit();

            driver.FindElement(By.LinkText("Home")).Click();

            element = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(element.Text.Contains(expectedListName));
        }

        /*Scenario 2: BUS - Add items to list.
        1. View items in a list.
        2. Create a new item in a list.
        3. Remove an item from a list.
        */

        /*Scenario 3: BUS - Check off completed items.
        1. Check off a completed item from a list.
        2. Check off multiple items from a list.
        3. Uncheck item from list.
        */

        /*Scenario 4: BUS - Share lists.
        1. Share a list with a single person.
        2. Share a list with a group of people.
        */

        /*Scenario 5: BUS - Purchse frequeny.
        1. Add a user specified replacement frequency to an item on a list.
        2. View how often an item on a list is added to the list.
        3. View the date an item was checked off on a list.
        */

        /*Scenario 6: BUS - Specialized lists.
        1. Create a specialized Christmas list.
        2. Create a sub-list in a Christmas list, for a specific person.
        3. Set viewing restrictions for sub-list.
        */

        [AssemblyCleanup]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}
