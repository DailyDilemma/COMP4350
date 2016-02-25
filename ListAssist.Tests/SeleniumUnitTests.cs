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
        public void TestOpenApp()
        {
            var expectedTitle = "Welcome to ListAssist!";

            driver.Navigate().GoToUrl(baseURL);
            Assert.AreEqual(expectedTitle, driver.Title);
        }

        [TestMethod]
        public void TestCreateNewEmptyList()
        {
            var expectedListName = "Empty List";

            driver.FindElement(By.LinkText("Create New List")).Click();

            IWebElement element = driver.FindElement(By.Name("Name"));
            element.SendKeys("Empty List");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.LinkText("Home")).Click();

            element = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(element.Text.Contains(expectedListName));
        }

        [TestMethod]
        public void TestRemoveNewEmptyList()
        {
            var deletedListName = "Empty List";

            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[2]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            IWebElement element = driver.FindElement(By.TagName("body"));
            Assert.IsFalse(element.Text.Contains(deletedListName));
        }

        /*Scenario 2: BUS - Add items to list.
        1. View items in a list.
        2. Create new items in a list.
        3. Remove items from a list.
        */
        [TestMethod]
        public void TestViewListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Details')])[2]")).Click();

            IWebElement element = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(element.Text.Contains("Milk"));
            Assert.IsTrue(element.Text.Contains("Oranges"));
            Assert.IsTrue(element.Text.Contains("Apples"));
            Assert.IsTrue(element.Text.Contains("Eggs"));

            driver.FindElement(By.LinkText("Home")).Click();
        }

        [TestMethod]
        public void TestAddListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[2]")).Click();
            driver.FindElement(By.LinkText("Add Item")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Pepperoni");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.LinkText("Add Item")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Noodles");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

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
        public void TestRemoveListItems()
        {
            driver.Navigate().GoToUrl(baseURL + "/LALists");
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[2]")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'X')])[5]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'X')])[5]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Assert.AreEqual(0, driver.FindElements(By.Id("LAListItems_4__Description")).Count);
            Assert.AreEqual(0, driver.FindElements(By.Id("LAListItems_5__Description")).Count);

            driver.FindElement(By.LinkText("Home")).Click();
        }

        /*Scenario 3: BUS - Check off completed items.
        1. Check off a completed item from a list.
        2. Check off multiple items from a list.
        3. Uncheck item from list.
        */

        /*Scenario 4: BUS - Share lists.
        1. Share a list with a single person.
        2. Share a list with a group of people.
        */

        /*Scenario 5: BUS - Purchase frequeny.
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
