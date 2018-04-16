using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Poc.Automation
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using NUnit.Framework;

    
    public class PocAutomation
    {
        private string baseURL = "https://wingskushma.github.io/pages/demoPage.html";
        private RemoteWebDriver driver;
        private string browser = "chromes";

        public string CheckboxCssSelector =
            ".checkboxDiv:nth-of-type({0})>div.form-group:nth-of-type({1}) .checkbox:nth-of-type({2}) input";


        [Test, Category("Selenium")]
        public void PersonalityTest()
        {
            //driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseURL);
            SetValueByCssSelector("input[type=\"text\"]", "test value"); 
            SetTextArea("textarea#introInput", 5001);
            SetTextArea("textarea#hobbiesInput", 5001);
            var checkBoxCount = FindCountOfElements(".checkboxDiv:nth-of-type(1)>div.form-group");
            for (int i = 1; i < checkBoxCount+1; i++)
            {
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 1, i, 1));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 1, i, 2));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 1, i, 3));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 1, i, 4));
            }
            checkBoxCount = FindCountOfElements(".checkboxDiv:nth-of-type(2)>div.form-group");
            for (int i = 1; i < checkBoxCount+1; i++)
            {
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 2, i, 1));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 2, i, 2));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 2, i, 3));
                ClickOnElementByCssSelector(string.Format(CheckboxCssSelector, 2, i, 4));
            }
            ClickOnElementByCssSelector(".submitButton");
        }

      

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TearDown()]
        public void TestCleanup()
        {
            driver.Quit();
        }

        [SetUp]
        public void TestInitialize()
        {   //Set the browswer from a build
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                
                default:
                    driver = new ChromeDriver();
                    break;
            }
          
           
        }

        [TestFixtureSetUp]
        protected virtual void ClassInit()
        {
            Console.WriteLine("Class initialized");
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