using System.Linq;

namespace BaseSolution.Hooks
{
    using System.Threading;

    using TechTalk.SpecFlow;

    [Binding]
    class StepHooks
    {
        [BeforeStep]
        public void WaitHalfASecond()
        {
            if (!ScenarioContext.Current.ScenarioInfo.Tags.Contains("NoSleep"))
                Thread.Sleep(500);
        }
    }
}
