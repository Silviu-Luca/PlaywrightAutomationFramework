using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;
using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class LoginTests : BaseTests
    {
        private LoginPage _loginPage;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
        }

        [Test]
        [Category("smoke")]
        [TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.GetLoginDataSource))]
        public async Task TestLogin(LoginData login)
        {
            await _loginPage.Login(login.User, login.Pass);
        }

        [Test]
        [Category("smoke")]
        public async Task TestInvalidLogin()
        {
            await _loginPage.InvalidLogin("invalid_user", "invalid_Pass");
        }
    }
}