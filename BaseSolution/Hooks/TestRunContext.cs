namespace BaseSolution.Hooks
{
    using System;

    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Safari;

    public static class BrowserType
    {
        public const int IE = 0;
        public const int Chrome = 1;
        public const int Firefox = 2;
        public const int Safari = 3;
    }

    [TestFixture]
    public static class TestRunContext
    {
        private static string[] testParams;

        public static void Initialise()
        {
            Initialise(BrowserType.Chrome);
        }

        public static void WindowSetup()
        {
            Driver.Navigate().GoToUrl("about:blank");
        }

        #region Browser Types
        public static void Initialise(int browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    SetupChromeDriver();
                    break;

                case BrowserType.Firefox:
                    SetupFirefoxDriver();
                    break;

                case BrowserType.IE:
                    SetupIEDriver();
                    break;

                case BrowserType.Safari:
                    SetupSafariDriver();
                    break;

                default:
                    SetupChromeDriver();
                    break;
            }
        }
        #endregion // Browser Types

        #region Local Browsers and Setup
        private static void SetupFirefoxDriver()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("network.proxy.type", 4);
            // profile.SetPreference("network.proxy.http", "192.168.34.82");
            // profile.SetPreference("network.proxy.http_port", "3182");
            Driver = new FirefoxDriver();

        }

        private static void SetupChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--no-sandbox");
            Driver = new ChromeDriver(options);
        }

        private static void SetupIEDriver()
        {
            Driver = new InternetExplorerDriver();
        }

        private static void SetupSafariDriver()
        {
            Driver = new SafariDriver();        
        }

        #endregion //Local Browsers
    
        public static IWebDriver Driver { get; private set; }

        public static string Location
        {
            get
            {
#if TEST_LOCAL
                return TestRunLocation.Local;
#else
                return TestRunLocation.Remote;
#endif
            }
        }
    }

    internal static class TestRunLocation
    {
        public const string Local = "TestLocal";
        public const string Remote = "TestRemote";
    }
}