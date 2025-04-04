using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;
using Reqnroll;
using Reqnroll.BoDi;

namespace PlaywrightAutomationFramework.Reqnroll.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IObjectContainer _objectContainer;
        private ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            this._objectContainer = objectContainer;
            this._scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task RegisterInstances()
        {
            var playwright = await Playwright.CreateAsync();
            var chromiumBrowser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var browserContext = await chromiumBrowser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true });
            var page = await browserContext.NewPageAsync();

            _objectContainer.RegisterInstanceAs(new LoginPage(page));
            _objectContainer.RegisterInstanceAs(new InventoryPage(page));
            _objectContainer.RegisterInstanceAs(new CartPage(page));
            _objectContainer.RegisterInstanceAs(new CheckOutInformationsPage(page));
            _objectContainer.RegisterInstanceAs(new CheckOutOverviewPage(page));
            _objectContainer.RegisterInstanceAs(new CheckOutCompletePage(page));
        }
    }
}