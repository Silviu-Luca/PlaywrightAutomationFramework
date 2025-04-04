using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class LoginPage : BasePage
    {
        private InventoryPage _inventoryPage;

        public LoginPage(IPage page) : base(page)
        {
            _inventoryPage = new InventoryPage(page);
        }
        
        #region Locators
        private ILocator _userName => _page.Locator("id=user-name");

        private ILocator _password => _page.Locator("id=password");

        private ILocator _loginButton => _page.Locator("id=login-button");

        private ILocator _invalidLoginErrorMessage => _page.Locator("data-test=error").Locator("\"Epic sadface: Username and password do not match any user in this service\"");
        #endregion

        public async Task OpenSauceDemoPage()
        {
            await _page.GotoAsync(_sauceDemoPageUrl);
        }

        public async Task SetUserName(string user)
        {
            await _userName.FillAsync(user);

        }
        public async Task SetPassword(string pass)
        {
            await _password.FillAsync(pass);

        }
        public async Task ClickOnogin()
        {
            await _loginButton.ClickAsync();

        }
        public async Task WaitForInventoryPage()
        {
            await _inventoryPage.CheckIfInventoryPage();
        }
        public async Task Login(string user, string pass)
        {
            await OpenSauceDemoPage();
            await _userName.FillAsync(user);
            await _password.FillAsync(pass);
            await _loginButton.ClickAsync();
            await _inventoryPage.CheckIfInventoryPage();
        }
        public async Task InvalidLogin(string user, string pass)
        {
            await OpenSauceDemoPage();
            await _userName.FillAsync(user);
            await _password.FillAsync(pass);
            await _loginButton.ClickAsync();
            await _invalidLoginErrorMessage.WaitForAsync();
            await _invalidLoginErrorMessage.InnerTextAsync();
        }
    }
}