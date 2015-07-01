namespace BaseSolution.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    internal static class TestRunHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
             TestRunContext.Initialise();
             TestRunContext.WindowSetup();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestRunContext.Driver.Quit();
        }
    }
}
