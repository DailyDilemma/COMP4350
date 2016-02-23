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

        }

        /* Scenario
        1. Open app in browser
        2. Maximize app
        */
        [TestMethod]
        public void TestOpenApp()
        {
            driverFF.Navigate().GoToUrl("http://localhost:2850");
            driverFF.Manage().Window.Maximize();
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            driverFF.Quit();
        }
    }
}
