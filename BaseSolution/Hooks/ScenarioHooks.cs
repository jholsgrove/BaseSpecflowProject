using BoDi;
using TechTalk.SpecFlow;

namespace BaseSolution.Hooks
{
    using BaseSolution.Pages;

    [Binding]
    public class ScenarioHooks
    {
        private readonly IObjectContainer objectContainer;

        public ScenarioHooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.objectContainer.RegisterInstanceAs(new PageContext(TestRunContext.Driver));
        }

        [AfterScenario]
        public void AfterScenario()
        {
        }
    }
}