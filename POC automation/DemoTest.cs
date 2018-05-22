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
       
        private DemoPageService _demo;



        [Test, Category("Selenium")]
        public void PersonalityTest()
        {
            SiteDriver.MaximizeWindow();
            SiteDriver.Open(baseURL);
            Console.WriteLine("Navigate to " + baseURL);
            FillFormFields();
            FillFormFields();
            FillFormFields();
            FillFormFields();
            FillFormFields();
            FillFormFields();
            //var checkBoxCount = _demo.FindCountOfElements(".checkboxDiv:nth-of-type(1)>div.form-group");
            //for (int i = 1; i < checkBoxCount+1; i++)
            //{
            //    _demo.ClickOnFavItem( 1, i, 1);
            //    _demo.ClickOnFavItem( 1, i, 2);
            //    _demo.ClickOnFavItem( 1, i, 3);
            //    _demo.ClickOnFavItem( 1, i, 4);
            //}
            //checkBoxCount = _demo.FindCountOfElements(".checkboxDiv:nth-of-type(2)>div.form-group");
            //for (int i = 1; i < checkBoxCount+1; i++)
            //{
            //    _demo.ClickOnFavItem( 2, i, 1);
            //    _demo.ClickOnFavItem( 2, i, 2);
            //    _demo.ClickOnFavItem( 2, i, 3);
            //    _demo.ClickOnFavItem( 2, i, 4);
            //}
        }

        private void FillFormFields()
        {
            _demo.SetValueByCssSelector("input[type=\"text\"]", "test value");
            _demo.SetTextArea("textarea#introInput", 1500);
            _demo.SetTextArea("textarea#hobbiesInput", 1500);
            _demo.SetFullName("John Doe");
            _demo.SelectGender();
            _demo.SetEmail("johnDoe@johnDoe.com");
            _demo.SetPassword("ASDqwe123");
            _demo.SetCity("Kathmandu");
            _demo.SetState("Pradesh 2");
            _demo.SetZipCode("00977");
            _demo.SetContactNo("9998009123");
            //_demo.SetIntroduction(SiteDriver.ReplicateString("intro "));
            //_demo.SetHobbies(SiteDriver.ReplicateString("hobbies "));
            _demo.ClickFavFood();
            _demo.SelectSkills("Coding");
            _demo.SelectCountry("Nepal");
            _demo.SelectFavItem();
            _demo.ClickOnElementByCssSelector(".submitButton");
        }


        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TearDown()]
        public void TestCleanup()
        {
            SiteDriver.Stop();
        }

        [SetUp]
        public void TestInitialize()
        {   //Set the browswer from a build
            SiteDriver.LaunchDriver();
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
            _demo = new DemoPageService();
        }


       


    }
}