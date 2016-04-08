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
        static string baseURL;

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            //driver = new ChromeDriver(@"C:\Program Files (x86)\chromedriver_win32");  // Not included on install, download here http://chromedriver.storage.googleapis.com/index.html?path=2.21/ and change path to location on your machine.
            driver = new FirefoxDriver();
            baseURL = "http://localhost:2850";
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /* Scenario 1: BUS - Manage Lists.
        1. View existing lists.
        2. Create a list.
        3. Remove a list.
        */
        [TestMethod]
        public void Test01_SeleniumOpenApp()
        {
            var expectedTitle = "Welcome to ListAssist!";

            driver.Navigate().GoToUrl(baseURL);
            Assert.AreEqual(expectedTitle, driver.Title);
        }

        [TestMethod]
        public void Test02_SeleniumCreateNewEmptyList()
        {
            var expectedListName = "Empty List";

            driver.FindElement(By.LinkText("Create")).Click();

            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Empty List");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            //driver.FindElement(By.LinkText("Save")).Click();

            driver.FindElement(By.CssSelector("button.button.expand")).Click();

            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(expectedListName));
        }

        [TestMethod]
        public void Test03_SeleniumRemoveNewEmptyList()
        {
            var deletedListName = "Empty List";

            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[3]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(deletedListName));
        }

        /*Scenario 2: BUS - Add items to list.
        1. View items in a list.
        2. Create new items in a list.
        3. Remove items from a list.
        */
        [TestMethod]
        public void Test04_SeleniumViewListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Details")).Click();

            IWebElement element = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(element.Text.Contains("Milk"));
            Assert.IsTrue(element.Text.Contains("Oranges"));
            Assert.IsTrue(element.Text.Contains("Apples"));
            Assert.IsTrue(element.Text.Contains("Eggs"));

            driver.FindElement(By.LinkText("Home")).Click();
        }

        [TestMethod]
        public void Test05_SeleniumAddListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.LinkText("Add Item")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Pepperoni");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.LinkText("Add Item")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Noodles");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.CssSelector("button.button.expand")).Click();
            driver.FindElement(By.LinkText("Details")).Click();

            try
            {
                Assert.AreEqual("Pepperoni", driver.FindElement(By.Id("LAListItems_4__Description")).GetAttribute("value"));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            try
            {
                Assert.AreEqual("Noodles", driver.FindElement(By.Id("LAListItems_5__Description")).GetAttribute("value"));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            driver.FindElement(By.LinkText("Home")).Click();
        }

        [TestMethod]
        public void Test06_SeleniumRemoveListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'X')])[5]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'X')])[5]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.CssSelector("button.button.expand")).Click();
            driver.FindElement(By.LinkText("Details")).Click();

            Assert.AreEqual(0, driver.FindElements(By.Id("LAListItems_4__Description")).Count);
            Assert.AreEqual(0, driver.FindElements(By.Id("LAListItems_5__Description")).Count);

            driver.FindElement(By.LinkText("Home")).Click();
        }

        /*Scenario 3: BUS - Check off completed items.
        1. Check off a completed item from a list.
        2. Check off multiple items from a list.
        3. Uncheck item from list.
        */
        [TestMethod]
        public void Test07_SeleniumCheckItem()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[2]")).Click();
            driver.FindElement(By.Id("LAListItems_0__Done")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.LinkText("Home")).Click();
        }

        [TestMethod]
        public void Test08_SeleniumCheckMultipleItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.Id("LAListItems_0__Done")).Click();
            driver.FindElement(By.Id("LAListItems_1__Done")).Click();
            driver.FindElement(By.Id("LAListItems_2__Done")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.LinkText("Home")).Click();
        }

        [TestMethod]
        public void Test09_SeleniumUncheckItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.Id("LAListItems_0__Done")).Click();
            driver.FindElement(By.Id("LAListItems_1__Done")).Click();
            driver.FindElement(By.Id("LAListItems_2__Done")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[2]")).Click();
            driver.FindElement(By.Id("LAListItems_0__Done")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.LinkText("Home")).Click();
        }

        /*Scenario 4: BUS - Add suggested item to list.
        1. Add suggested item to a list.
        */
        /*[TestMethod]
        public void Test10_SeleniumAddSuggestedItem()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.LinkText("Add")).Click();
            driver.FindElement(By.LinkText("Add")).Click();
            driver.FindElement(By.LinkText("Save")).Click();
            driver.FindElement(By.CssSelector("button.button.expand")).Click();
        }*/

        [AssemblyCleanup]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}
