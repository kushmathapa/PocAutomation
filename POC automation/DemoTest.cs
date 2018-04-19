using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Poc.Automation
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using NUnit.Framework;
    using System.Configuration;

    
    public class DemoTest
    {
        private string baseURL = "https://wingskushma.github.io/pages/demoPage.html";
        private RemoteWebDriver _driver;
        private string _browserOptions;
        private Uri _remoteAddress;
        private DesiredCapabilities _capabilities;
        private DriverService _driverService;
        private const string PATH = "Executables";


        public string CheckboxCssSelector =
            ".checkboxDiv:nth-of-type({0})>div.form-group:nth-of-type({1}) .checkbox:nth-of-type({2}) input";


        [Test, Category("Selenium")]
        public void PersonalityTest()
        {
            //driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(baseURL);
            Console.WriteLine("Navigate to " + baseURL);
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
            _driver.Quit();
        }

        [SetUp]
        public void TestInitialize()
        {   //Set the browswer from a build
            LaunchDriver();
            // DesiredCapabilities capability = new DesiredCapabilities();
            //capability.SetCapability("browserName", "chrome");
            //_driver = new RemoteWebDriver(new Uri("http://192.168.73.67:4445/wd/hub"), capability);
            //_driver = new RemoteWebDriver(new Uri("http://192.168.74.166:4445/wd/hub"), capability);
            //driver = new ChromeDriver();



        }

        [TestFixtureSetUp]
        protected virtual void ClassInit()
        {
            Console.WriteLine("Class initialized");
        }


        private void SetTextArea(string cssSelector, int length)
        {
            var longtext = new string('a', length);
            _driver.FindElementByCssSelector(cssSelector)
                .Clear();
            _driver.FindElementByCssSelector(cssSelector)
                .SendKeys(longtext);
            Console.WriteLine("Set text Area with characters of length: "+ length);
        }

        private void SetValueByCssSelector(string cssSelector, string value)
        {
            _driver.FindElementByCssSelector(cssSelector)
                .Clear();
            _driver.FindElementByCssSelector(cssSelector)
                .SendKeys(value);
            Console.WriteLine("Set element value as "+ value);
        }

        private int FindCountOfElements(string cssLocator)
        {
            return _driver.FindElements(By.CssSelector(cssLocator)).Count;
            
        }

        private void ClickOnElementByCssSelector(string cssSelector)
        {
           // if (!driver.FindElementByCssSelector(cssSelector).Selected)
            _driver.FindElementByCssSelector(cssSelector)
                .Click();
            Console.WriteLine("Clicked on element" );
        }

        /// <summary>
        /// Instantiates a <see cref="ScreenshotRemoteWebDriver"/>.
        /// </summary>
        /// <returns>A <see cref="IWebDriver"/></returns>
        private RemoteWebDriver LaunchDriver()
        {
            string testServer = ConfigurationManager.AppSettings["TestServer"];
            string testPort = ConfigurationManager.AppSettings["TestPort"];
            _browserOptions = ConfigurationManager.AppSettings["TestBrowser"];
            var strBuilder = new StringBuilder();
            string requestUri = !string.IsNullOrEmpty(testServer) && !string.IsNullOrEmpty(testPort)
                                    ? strBuilder.Append("http://")
                                          .Append(testServer)
                                          .Append(":")
                                          .Append(testPort)
                                          .Append("/").ToString()
                                    : string.Empty;

            _remoteAddress = !string.IsNullOrEmpty(requestUri) ? new Uri(strBuilder
            .Append("wd")
            .Append("/")
            .Append("hub").ToString()) : null;
            switch (_browserOptions)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "ie":
                    _driver = new InternetExplorerDriver();
                    break;
                case "chrome":
                    _driver = LaunchChrome();
                    break;
                default:
                    throw new NotSupportedException(String.Format("Browser {0} is not supported.", _browserOptions));
            }


            return _driver;
        }


        /// <summary>
        /// Instantiates a <see cref="ChromeDriver"/>.
        /// </summary>
        /// <returns>A <see cref="IWebDriver"/></returns>
        private RemoteWebDriver LaunchChrome(string incognito = "0")
        {
            var switches = new[] { "--start-maximized", "--disable-popup-blocking", "--ignore-certificate-errors", "--multi-profiles", "--profiling-flush", "--disable-extensions" };
            if (incognito == "1") switches[switches.Length - 1] = "--incognito";
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
             options.AddArguments(switches);
             _capabilities = options.ToCapabilities() as DesiredCapabilities;
            if (null == _remoteAddress)
            {
                //_driverService = ChromeDriverService.CreateDefaultService(Path.GetFullPath(PATH));
                //_driverService.Start();
                //_remoteAddress = _driverService.ServiceUrl;
                _driver = new ChromeDriver();

            }
            if (_remoteAddress != null)
                _driver = new RemoteWebDriver(_remoteAddress, _capabilities);
            return _driver;
        }


    }
}