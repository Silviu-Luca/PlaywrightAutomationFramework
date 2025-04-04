using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class MenuTests : BaseTests
    {
        private LoginPage _loginPage;
        private MenuPage _menuPage;

        private LoginData _loginData;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
             _menuPage = new MenuPage(Page);

            _loginData = LoginDataSource.GetLoginData();
        }

        [Test]
        [Category("regression")]
        public async Task CheckMenuItems()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _menuPage.OpenMenu();
            await _menuPage.CloseMenu();
            await _menuPage.OpenMenu();
            await _menuPage.ClickOnAllItems();
            await _menuPage.CloseMenu();
        }
    }
}