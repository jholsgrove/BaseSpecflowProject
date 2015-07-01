﻿using System;
using System.Collections.Generic;
﻿using System.Collections.ObjectModel;
﻿using System.Drawing;
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Net;
﻿using TechTalk.SpecFlow;

namespace BaseSolution.Pages
{
    using BaseSolution.Constants;

    //COMMON ACTIONS FOR WEBDRIVER
    /// <summary>
    /// Common actions for WebDriver
    /// This base class contains a useful set of helper methods for scripting Selenium. It should 
    /// encapsulate the common practices and apply sensible programming practices such as DRY.
    /// Keep this code clean & refactored.
    /// 
    /// It should not contain knowledge of any specific page elements, use SUTMainPage for those.
    /// </summary>
    public class PageBase
    {
        protected readonly PageContext Context;
        private const int MaxWaitSeconds = 30;

        public PageBase(PageContext context)
        {
            this.Context = context;
            PageFactory.InitElements(this.Context.Driver, this);
        }

        public string GetCurrentUrl()
        {
            return Context.Driver.Url;
        }

        public void Back()
        {
            this.Context.Driver.Navigate().Back();
        }

        public Actions GetBuilder()
        {
            return Context.Actions;
        }

        public void NavigateTo(string url)
        {
            this.Context.Driver.Navigate().GoToUrl(url);
        }

        public void RefreshBrowser()
        {
            this.Context.Driver.Navigate().Refresh();
        }

        internal void SendKeyPress(string identifierQuery, string searchCriteria)
        {
            IWebElement element = Context.Driver.FindElement(By.ClassName(identifierQuery));
            element.SendKeys(searchCriteria);
        }

        public IWebElement WaitForElementByClass(string className)
        {
            return this.WaitForVisibility(By.ClassName(className));
        }

        public IWebElement WaitForElementByName(string name)
        {
            return this.WaitForVisibility(By.Name(name));
        }

        // Get by Text
        public string GetTextById(string id)
        {
            return this.Context.Driver.FindElement(By.Id(id)).Text;
        }

        public string GetTextByClass(string className)
        {
            var element = Context.Driver.FindElement(By.ClassName(className));
            return element.Text;
        }

        public string GetTextByCssSelector(string cssSelector)
        {
            return this.WaitForVisibility(By.CssSelector(cssSelector)).Text;
        }

        public string GetTextByName(string name)
        {
            return this.WaitForVisibility(By.Name(name)).Text;
        }

        public string GetInputValueById(string id)
        {
            return this.Context.Driver.FindElement(By.Id(id)).GetAttribute("value");
        }

        public string GetInputValueByName(string name)
        {
            return this.Context.Driver.FindElement(By.Name(name)).GetAttribute("value");
        }

        public void EnterTextIntoTextBox(By criteria, string text, bool addEnter = false)
        {
            WaitForVisibility(criteria, 2);
            var element = Context.Driver.FindElement(criteria);
            element.Click();
            element.Clear();
            element.SendKeys(text);

            if (addEnter)
            {
                element.SendKeys(Keys.Enter);
            }
        }

        internal void SendKeysSlowly(string IdQuery, string searchCriteria)
        {
            var element = Context.Driver.FindElement(By.Id(IdQuery));
            element.Clear();
            element.SendKeys(searchCriteria);
        }

        public IReadOnlyCollection<IWebElement> GetElementsByTagName(string tagName)
        {
            return Context.Driver.FindElements(By.TagName(tagName));
        }

        public IReadOnlyCollection<IWebElement> GetElementsById(string id)
        {
            return Context.Driver.FindElements(By.Id(id));
        }

        public string GetTextByXPath(string XPathText)
        {
            WaitForVisibility(By.XPath(XPathText), 10);
            return Context.Driver.FindElement(By.XPath(XPathText)).Text;
        }

        public string GetTextByTagName(string tagName)
        {
            WaitForVisibility(By.TagName(tagName), 10);
            return Context.Driver.FindElement(By.TagName(tagName)).Text;
        }

        public bool CheckULElementIsEmpty(string xPathText)
        {
            var element = Context.Driver.FindElement(By.XPath(xPathText));
            var lis = element.FindElements(By.TagName(TagName.ListItem));
            return (lis == null || lis.Count == 0);
        }

        public int GetElementUlLiCount(string xPathText)
        {
            var element = Context.Driver.FindElement(By.XPath(xPathText));

            var lis = element.FindElements(By.TagName(TagName.ListItem));
            return (lis == null) ? 0 : lis.Count;
        }

        public int GetElementUlLiCountWithId(string IdText)
        {
            var element = Context.Driver.FindElement(By.Id(IdText));

            var lis = element.FindElements(By.TagName(TagName.ListItem));
            return (lis == null) ? 0 : lis.Count;
        }

        public IWebElement GetElement(By byCriterion)
        {
            return Context.Driver.FindElement(byCriterion);
        }

        public ReadOnlyCollection<IWebElement> GetElementsByClassName(string className)
        {
            return Context.Driver.FindElements(By.ClassName(className));
        }

        public ReadOnlyCollection<IWebElement> GetElementsByCssSelector(string CssSelector)
        {
            return Context.Driver.FindElements(By.CssSelector(CssSelector));
        }

        public IWebElement GetElementByClassName(string className)
        {
            return Context.Driver.FindElements(By.ClassName(className))[0];
        }

        public bool ElementExists(By byCriterion)
        {
            try
            {
                Context.Driver.FindElement(byCriterion);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetElementText(By byCriterion)
        {
            return Context.Driver.FindElement(byCriterion).Text;
        }

        public bool InputElementIsEnabled(By byCriterion)
        {
            var element = Context.Driver.FindElement(byCriterion);
            return element.Enabled;
        }

        public bool ElementHasClass(By byCriterion, string className)
        {
            var element = Context.Driver.FindElement(byCriterion);
            var classes = element.GetAttribute("class");
            return classes.Contains(className);
        }

        public bool ElementIsVisible(By byCriterion)
        {
            var element = Context.Driver.FindElement(byCriterion);
            return element.Displayed;
        }

        public int GetElementListItemCount(By byCriterion)
        {
            var element = Context.Driver.FindElement(byCriterion);
            var lis = element.FindElements(By.TagName(TagName.ListItem));
            return (lis == null) ? 0 : lis.Count;
        }

        public bool IsListEmpty(By byCriterion)
        {
            var element = Context.Driver.FindElement(byCriterion);
            var lis = element.FindElements(By.TagName(TagName.ListItem));
            return (lis == null || lis.Count == 0);
        }

        public string GetSelectedItemFromCombobox(By combobox)
        {
            IWebElement chosenCombobox = Context.Driver.FindElement(combobox);
            SelectElement itemSelected = new SelectElement(chosenCombobox);

            return itemSelected.SelectedOption.Text;
        }

        public bool IsCheckboxSelected(By checkBox)
        {
            IWebElement Checkbox = Context.Driver.FindElement(checkBox);

            return Checkbox.Selected;
        }
     
        // Click By
        public void ClickByXPath(string xpathQuery)
        {
            WaitForVisibility(By.XPath(xpathQuery));
            var element = this.Context.Driver.FindElement(By.XPath(xpathQuery));
            Assert.IsNotNull(element);
            element.Click();
        }

        public void ClickByClass(string className)
        {
            WaitForClassToBeVisible(className, 5);
            this.Context.Driver.FindElements(By.ClassName(className)).First().Click();
        }

        public void ClickByTagname(string tagname)
        {
            WaitForVisibility(By.TagName(tagname), 5).Click();

        }

        public void ClickByCssSelector(string css)
        {
            WaitForVisibility(By.CssSelector(css), 5).Click();
        }

        public void ClickByLinkText(string LinkText)
        {
            WaitForVisibility(By.LinkText(LinkText), 10).Click();
        }

        public void ClickById(string id)
        {
            WaitForVisibility(By.Id(id), 10).Click();
        }

        public void ClickByName(string name)
        {
            WaitForVisibility(By.Name(name), 10).Click();
        }

        public void ClickByPartialLinkText(string LinkText)
        {
            WaitForVisibility(By.PartialLinkText(LinkText), 5).Click();
        }

        public void ClickOnElementOffset(By byCriterion, int offX, int offY)
        {
            WaitForVisibility(byCriterion);
            var element = this.Context.Driver.FindElement(byCriterion);
            this.Context.Actions.MoveToElement(element).MoveByOffset(offX, offY).Click().Perform();
        }

        public string WindowTitle()
        {
            return this.Context.Driver.Title;
        }

        # region Protected Helper Methods for derived classes

        /// <summary>
        /// Refactored all the "wait for this & that to be visible" stuff into a couple 
        /// of tiny methods
        /// </summary>
        /// <param name="byCriterion"></param>
        /// <param name="maxWaitSeconds"></param>
        public IWebElement WaitForVisibility(By byCriterion, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(this.Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(byCriterion));
        }

        protected IWebElement WaitForClassToBeVisible(string className, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(By.ClassName(className), maxWaitSeconds);
        }

        protected IWebElement WaitForIdToBeVisible(string id, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(By.Id(id), maxWaitSeconds);
        }
        #endregion
    }
}