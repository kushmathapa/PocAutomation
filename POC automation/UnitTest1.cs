using System.Collections.Generic;
using System.Linq;

namespace Partsunlimited.UITests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.PhantomJS;
    using System;

    [TestClass]
    public class ChucksClass1
    {
        private string baseURL = "https://wingskushma.github.io/pages/demoPage.html";
        private RemoteWebDriver driver;
        private string browser;
        public TestContext TestContext { get; set; }

        public string checkboxCssSelector =
            ".checkboxDiv:nth-of-type({0})>div.form-group:nth-of-type({1}) .checkbox:nth-of-type({2}) input";

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        [Owner("Chrome")]

        public void PersonalityTest()
        {
            //driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseURL);
            SetValueByCssSelector("input[type=\"text\"]","test value");
            SetTextArea("textarea#introInput", 5001);
            SetTextArea("textarea#hobbiesInput", 5001);
            var checkBoxCount = FindCountOfElements(".checkboxDiv:nth-of-type(1)>div.form-group");
            for (int i = 1; i < checkBoxCount+1; i++)
            {
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 1, i, 1));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 1, i, 2));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 1, i, 3));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 1, i, 4));
            }
            checkBoxCount = FindCountOfElements(".checkboxDiv:nth-of-type(2)>div.form-group");
            for (int i = 1; i < checkBoxCount+1; i++)
            {
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 2, i, 1));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 2, i, 2));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 2, i, 3));
                ClickOnElementByCssSelector(string.Format(checkboxCssSelector, 2, i, 4));
            }
            ClickOnElementByCssSelector(".submitButton");
        }

      

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {   //Set the browswer from a build
            browser = TestContext.Properties["browser"] != null ? TestContext.Properties["browser"].ToString() : "chrome";
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
            if (TestContext.Properties["Url"] != null) //Set URL from a build
            {
                baseURL = TestContext.Properties["Url"].ToString();
            }
           
        }

        private void SetTextArea(string cssSelector, int length)
        {
            var longtext = new string('a', length);
            driver.FindElementByCssSelector(cssSelector)
                .Clear();
            driver.FindElementByCssSelector(cssSelector)
                .SendKeys(longtext);
        }

        private void SetValueByCssSelector(string cssSelector, string value)
        {
            driver.FindElementByCssSelector(cssSelector)
                .Clear();
            driver.FindElementByCssSelector(cssSelector)
                .SendKeys(value);
        }

        private int FindCountOfElements(string cssLocator)
        {
            return driver.FindElements(By.CssSelector(cssLocator)).Count;
            
        }

        private void ClickOnElementByCssSelector(string cssSelector)
        {
           // if (!driver.FindElementByCssSelector(cssSelector).Selected)
            driver.FindElementByCssSelector(cssSelector)
                .Click();
            
        }
    }
}