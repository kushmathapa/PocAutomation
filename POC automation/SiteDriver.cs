using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;

namespace Poc.Automation
{
    public class SiteDriver
    {

        private static IWebDriver _webDriver;
        private static string _browserOptions;
        private static Uri _remoteAddress;
        private static DesiredCapabilities _capabilities;
        private static DriverService _driverService;
        //private static IWebDriver _webDriver;
        public SiteDriver(IWebDriver webDriver)
        {
            webDriver.Manage().Window.Maximize();
            _webDriver = webDriver;            
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
           
        }

        public static IWebElement FindElement(string selector, How how)
        {
            switch (how)
            {
                case How.CssSelector:
                    return _webDriver.FindElement(By.CssSelector(selector));
                case How.XPath:
                    return _webDriver.FindElement(By.XPath(selector));
                default:
                    return _webDriver.FindElement(By.Id(selector));
            }
        }
        public static List<IWebElement> FindElements(string selector, How how)
        {
            switch (how)
            {
                case How.CssSelector:
                    return _webDriver.FindElements(By.CssSelector(selector)).ToList();
                case How.XPath:
                    return _webDriver.FindElements(By.XPath(selector)).ToList();
                default:
                    return _webDriver.FindElements(By.Id(selector)).ToList();
            }
        }

        public static void Click(string selector, How how)
        {
            FindElement(selector, how).Click();
        }

        public static void SendKeys(string selector, How how, string text)
        {
            FindElement(selector, how).SendKeys(text);
        }

        public static string ReplicateString(string text)
        {
            string myvar = text; 
            StringBuilder b = new StringBuilder(myvar.Length * 2000);
            for (int i = 0; i != 2000; ++i)
            {
                b.Append(myvar);
            }
            return b.ToString();
        }

        public static string GetCurrentUrl()
        {
            return _webDriver.Url;
        }

        public static void OpenUrl(string url)
        {
            _webDriver.Url = url;
        }
        public static void Open(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public static void CloseWindow()
        {

            _webDriver.Close();
        }
        public static void Stop()
        {
           _webDriver.Quit();
        }
        public static void MaximizeWindow()
        {
            _webDriver.Manage().Window.Maximize();
        }

        public static void ToList(string selector, How how)
        {
            List<IWebElement> listElements = FindElements(selector, how).ToList();
        }


        public static bool _IsOrdered(IList<IWebElement> list, IList<string> comparer)
        {
            if (list.Count > 1)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i].Text != comparer[i].ToString())
                    { return false; }
                }
            }
            return true;
        }

        public static bool _IsOrdered(IList<string> list, IList<string> comparer)
        {
            if (list.Count > 1)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i].ToString() != comparer[i].ToString())
                    { return false; }
                }
            }
            else {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Instantiates a <see cref="ScreenshotRemoteWebDriver"/>.
        /// </summary>
        /// <returns>A <see cref="IWebDriver"/></returns>
        public static IWebDriver LaunchDriver()
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
                    _webDriver = new FirefoxDriver();
                    break;
                case "ie":
                    _webDriver = new InternetExplorerDriver();
                    break;
                case "chrome":
                    _webDriver = LaunchChrome();
                    break;
                default:
                    throw new NotSupportedException(String.Format("Browser {0} is not supported.", _browserOptions));
            }


            return _webDriver;
        }


        /// <summary>
        /// Instantiates a <see cref="ChromeDriver"/>.
        /// </summary>
        /// <returns>A <see cref="IWebDriver"/></returns>
        public static IWebDriver LaunchChrome(string incognito = "0")
        {
            var switches = new[] { "--start-maximized", "--disable-popup-blocking", "--ignore-certificate-errors", "--multi-profiles", "--profiling-flush", "--disable-extensions" };
            if (incognito == "1") switches[switches.Length - 1] = "--incognito";
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments(switches);
            _capabilities = options.ToCapabilities() as DesiredCapabilities;
            if (_remoteAddress == null)
            {
                //_driverService = ChromeDriverService.CreateDefaultService(Path.GetFullPath(PATH));
                //_driverService.Start();
                //_remoteAddress = _driverService.ServiceUrl;
                _webDriver = new ChromeDriver();

            }
            else
                _webDriver = new RemoteWebDriver(_remoteAddress, _capabilities);
            return _webDriver;
        }  
        
    }
}
