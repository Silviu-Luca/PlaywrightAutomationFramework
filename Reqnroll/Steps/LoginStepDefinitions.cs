using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;
using Reqnroll;

namespace PlaywrightAutomationFramework.Reqnroll.Steps
{
    [Binding]
    public class LoginStepDefinitions
    {
        private LoginPage _loginPage;
        private string _userName = "standard_user";
        private string _password = "secret_sauce";

        public LoginStepDefinitions(LoginPage loginPage)
        {
            this._loginPage = loginPage;
        }

        [Given("I go to SauceDemo login page")]
        public async Task GivenIGoToSauceDemoLoginPage()
        {
            await _loginPage.OpenSauceDemoPage();
        }

        [When("I fill the user")]
        public async Task WhenIFillTheUser()
        {
            await _loginPage.SetUserName(_userName);
        }

        [When("I fill the pass")]
        public async Task WhenIFillThePass()
        {
            await _loginPage.SetPassword(_password);
        }

        [When("I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            await _loginPage.ClickOnogin();
        }

        [Then("I am redirected to inventory page")]
        public async Task ThenIAmRedirectedToInventoryPage()
        {
            await _loginPage.WaitForInventoryPage();
        }
    }
}
