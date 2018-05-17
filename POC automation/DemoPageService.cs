using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;

namespace Poc.Automation
{
    class DemoPageService
    {
        private DemoPageObject _demoPage;

        public DemoPageService()
        {
            _demoPage = new DemoPageObject();
        }

        public void SetTextArea(string cssSelector, int length)
        {
            var longtext = new string('a', length);
            SiteDriver.FindElement(cssSelector, How.CssSelector)
                .Clear();
            SiteDriver.FindElement(cssSelector, How.CssSelector)
                .SendKeys(longtext);
            Console.WriteLine("Set text Area with characters of length: " + length);
        }

        public void SetValueByCssSelector(string cssSelector, string value)
        {
            SiteDriver.FindElement(cssSelector, How.CssSelector)
                .Clear();
            SiteDriver.FindElement(cssSelector, How.CssSelector)
                .SendKeys(value);
            Console.WriteLine("Set element value as " + value);
        }

        public int FindCountOfElements(string cssLocator)
        {
            return SiteDriver.FindElements(cssLocator, How.CssSelector).Count;

        }

        public void ClickOnElementByCssSelector(string cssSelector)
        {
            // if (!driver.FindElementByCssSelector(cssSelector).Selected)
            SiteDriver.FindElement(cssSelector, How.CssSelector)
                .Click();
            Console.WriteLine("Clicked on element");
        }
        public void ClickOnFavItem(int col, int row, int rowCol)
        {
            // if (!driver.FindElementByCssSelector(cssSelector).Selected)
            SiteDriver.FindElement(String.Format(_demoPage.CheckboxCssSelector, col, row, rowCol), How.CssSelector)
                .Click();
            Console.WriteLine("Clicked on element of Side {0} for row:{1} fav item, selected {2} item", col, row, rowCol);
        }

        public void SetFullName(string strFullName)
        {
            SiteDriver.SendKeys(_demoPage.fullName, How.CssSelector, strFullName);
        }

        public void SelectGender(int gender = 1)
        {
            List<IWebElement> listElements = SiteDriver.FindElements(_demoPage.gender, How.CssSelector);
            listElements[gender].Click();
        }

        public int SelectMultipleGender()
        {
            List<IWebElement> listElements = SiteDriver.FindElements(_demoPage.gender, How.CssSelector);
            int count = 0;
            foreach (IWebElement e in listElements)
            {
                e.Click();
            }

            foreach (IWebElement e in listElements)
            {
                count += e.Selected ? 1 : 0;

            }
            return count;


        }
        public void SetEmail(string strEmail)
        {
            SiteDriver.SendKeys(_demoPage.email, How.CssSelector, strEmail);
        }

        public void SetUserName(string strUName)
        {
            SiteDriver.SendKeys(_demoPage.username, How.CssSelector, strUName);
        }

        public void SetPassword(string strPassword)
        { SiteDriver.SendKeys(_demoPage.password, How.CssSelector, strPassword); }

        public void SetCity(string strCity)
        { SiteDriver.SendKeys(_demoPage.city, How.CssSelector, strCity); }

        public void SetState(string strState)
        { SiteDriver.SendKeys(_demoPage.state, How.CssSelector, strState); }

        public void SetZipCode(string strZipCode)
        { SiteDriver.SendKeys(_demoPage.zipCode, How.CssSelector, strZipCode); }

        public void SetContactNo(string strContactNo)
        { SiteDriver.SendKeys(_demoPage.contactNo, How.CssSelector, strContactNo); }

        public void SetIntroduction(string strIntro)
        {
            SiteDriver.SendKeys(_demoPage.introdution, How.CssSelector, strIntro);

        }

        public void SetHobbies(string strHobbies)
        { SiteDriver.SendKeys(_demoPage.hobbies, How.CssSelector, strHobbies); }

        public void ClickFavFood()
        {
            List<IWebElement> listElements = SiteDriver.FindElements(_demoPage.favFood, How.CssSelector);


            foreach (IWebElement e in listElements)
            {
                e.Click();
            }

        }

        public int SelectMutipleFavFood()
        {
            int count = 0;
            ClickFavFood();
            foreach (IWebElement e in SiteDriver.FindElements(_demoPage.favFood, How.CssSelector))
            {
                count += e.Selected ? 1 : 0;
            }
            Console.WriteLine("count:" + count);
            return count;

        }

        public void SelectSkills(string skill)
        {
            SelectElement selectElement = new SelectElement(SiteDriver.FindElement(_demoPage.skills, How.CssSelector));
            selectElement.SelectByText(skill);
        }

        public void SelectCountry(string country)
        {
            SelectElement selectElement = new SelectElement(SiteDriver.FindElement(_demoPage.country, How.CssSelector));
            selectElement.SelectByText(country);
        }

        public Boolean IsMultipleSkillSelect
        {
            get
            {
                SelectElement selectElement = new SelectElement(SiteDriver.FindElement(_demoPage.skills, How.CssSelector));
                return selectElement.IsMultiple;
            }
        }
        public Boolean IsMultipleCountrySelect
        {
            get
            {
                SelectElement selectElement = new SelectElement(SiteDriver.FindElement(_demoPage.country, How.CssSelector));
                return selectElement.IsMultiple;
            }
        }

        public void SelectFavItem()
        {
            List<IWebElement> listElements = SiteDriver.FindElements(_demoPage.fav, How.CssSelector);

            foreach (IWebElement e in listElements)
            {/*
                if (e.GetAttribute("value").Substring(5, 1) == "1")
                {*/
                e.Click();
            }
            // }
        }


        public void ClickSubmitButton()
        {
            SiteDriver.Click(_demoPage.submitBtn, How.CssSelector);
        }

        public void ClickResetButton()
        {
            SiteDriver.Click(_demoPage.resetBtn, How.CssSelector);
        }


        public string getEmailAddress()
        {
            return getTextBoxValue(_demoPage.email, How.CssSelector);
        }

        public string getTextBoxValue(string selector, How how)
        {
            return SiteDriver.FindElement(selector, how).GetAttribute("value");

        }

        public bool CheckNumericZip
        {
            get
            {
                try
                {
                    string zip = getTextBoxValue(_demoPage.zipCode, How.CssSelector);
                    Convert.ToInt32(zip);
                    return true;
                }
                catch (InvalidCastException e)
                {
                    return false;
                }
            }
        }

    }
}
