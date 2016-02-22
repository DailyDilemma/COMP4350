using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ListAssist.Tests
{
    [TestClass]
    public class SeleniumUnitTest
    {
        static IWebDriver driverFF;

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverFF = new FirefoxDriver();

        }

        [TestMethod]
        public void TestOpenApp()
        {
            driverFF.Navigate().GoToUrl("http://localhost:2850");
            
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            driverFF.Quit();
        }
    }
}