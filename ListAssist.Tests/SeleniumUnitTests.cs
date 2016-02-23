using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ListAssist.Tests
{
    [TestClass]
    public class SeleniumUnitTests
    {
        static IWebDriver driverFF;  // FF to identify browser if more added later

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverFF = new FirefoxDriver();
            driverFF.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /* Scenario - Open app.
        1. Open app in browser
        */
        [TestMethod]
        public void TestOpenApp()
        {
            driverFF.Navigate().GoToUrl("http://localhost:2850");
        }

        /* Scenario - Create new empty list.
        1. Select Create
        2. Enter name for new list
        3. Click create button
        4. Click save button - doesn't auto redirect to home
        5. Select Home
        */
        [TestMethod]
        public void TestCreateNewEmptyList()
        {
            driverFF.FindElement(By.LinkText("Create")).Click();

            IWebElement element = driverFF.FindElement(By.Name("Name"));
            element.SendKeys("Empty List");
            element.Submit();

            element = driverFF.FindElement(By.Name("Name"));
            element.Submit();

            driverFF.FindElement(By.LinkText("Home")).Click();
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            //driverFF.Quit();
        }
    }
}
