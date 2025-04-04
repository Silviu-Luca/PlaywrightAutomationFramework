using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public abstract class BasePage
    {
        protected IPage _page;
        protected string _sauceDemoPageUrl;

        public BasePage(IPage page)
        {
            this._page = page;

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _sauceDemoPageUrl = config["AppConfig:SauceDemoBaseUrl"];
        }
    }
}