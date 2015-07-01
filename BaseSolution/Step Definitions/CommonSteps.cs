namespace BaseSolution.Step_Definitions
{
    using System;

    using BaseSolution.Constants;
    using BaseSolution.Pages;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    using TechTalk.SpecFlow;

    [Binding]
    internal class CommonSteps
    {
        public CommonSteps(SUTMainPage page)
        {
            Page = page;
        }

        private SUTMainPage Page { get; set; }

        [Given(@"I have navigated to '(.*)'")]
        public void GivenIHaveNavigatedTo(string website)
        {
            Page.NavigateTo(website);
        }

        [When(@"I click on the logo")]
        public void GivenIClickOnTheLogo()
        {
            Page.ClickOnLogo();
        }

        [Then(@"the window title is '(.*)'")]
        public void ThenTheWindowTitleIs(string winTitle)
        {
            var getwinTitle = Page.WindowTitle();
            Assert.AreEqual(winTitle, getwinTitle);
        }

        [When(@"I click button '(.*)'")]
        public void WhenIClickButton(string buttonName)
        {
            switch (buttonName)
            {
                case "X":
                    Page.WaitForElementByName(IdAttribute.Logo);
                    Page.ClickByName(IdAttribute.Logo);
                    break;

                case "Y":
                    // Do something else
                    break;
            }
        }

        [Given(@"I enter '(.*)' into the search")]
        public void GivenIEnterIntoTheSearch(string searchCriteria)
        {
            // true or false bool value indicates optional enter keypress
            Page.EnterTextIntoTextBox(By.Id(IdAttribute.Example), searchCriteria, true);
        }

        [Then(@"I am displayed details for '(.*)'")]
        public void ThenIAmDisplayedDetailsFor(string mySearch)
        {
            Page.WaitForElementByClass("cards-alias-entity-location");
            var searchText = Page.GetTextByClass("cards-alias-entity-location");
            Assert.IsTrue(searchText.Contains("Renaissance London"), "Expected text was incorrect or null.");
        }
    }
}