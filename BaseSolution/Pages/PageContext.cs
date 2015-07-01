namespace BaseSolution.Pages
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public class PageContext
    {
        public PageContext(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public IWebDriver Driver { get; private set; }

        public Actions Actions { get; set; }
    }
}