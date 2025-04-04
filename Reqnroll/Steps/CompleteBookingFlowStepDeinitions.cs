using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;
using Reqnroll;

namespace PlaywrightAutomationFramework.Reqnroll.Steps
{
    [Binding]
    public class CompleteBookingFlowStepDeinitions
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckOutInformationsPage _checkOutInformationsPage;
        private CheckOutOverviewPage _checkOutOverviewPage;
        private CheckOutCompletePage _checkOutCompletePage;

        public CompleteBookingFlowStepDeinitions(LoginPage loginPage,
            InventoryPage inventoryPage,
            CartPage cartPage,
            CheckOutInformationsPage checkOutInformationsPage,
            CheckOutOverviewPage checkOutOverviewPage,
            CheckOutCompletePage checkOutCompletePage)
        {
            this._loginPage = loginPage;
            this._inventoryPage = inventoryPage;
            this._cartPage = cartPage;
            this._checkOutInformationsPage = checkOutInformationsPage;
            this._checkOutOverviewPage = checkOutOverviewPage;
            this._checkOutCompletePage = checkOutCompletePage;
        }

        [When("I login with valid credentials")]
        public async Task WhenILoginWithValidCredentials()
        {
            await _loginPage.Login("standard_user", "secret_sauce");
        }

        [When("I add a (.*) to cart")]
        public async Task WhenIAddAProductToCart(string productName)
        {
            await _inventoryPage.AddInventoryItemToCart(productName);
        }

        [When("I open cart")]
        public async Task WhenIOpenCart()
        {
            await _inventoryPage.OpenCart();
        }

        [When("I go to check out")]
        public async Task WhenIGoToCheckOut()
        {
            await _cartPage.ClickOnCheckOut();
        }

        [When("I fill the billing details")]
        public async Task WhenIFillTheBillingDetails()
        {
            await _checkOutInformationsPage.SetPaymentDetailsAndProceed("qa", "tester", "IT");
        }

        [When("I click on finish")]
        public async Task WhenIClickOnFinish()
        {
            await _checkOutOverviewPage.ClickOnFinish();
        }

        [Then("I am redirect to thank you page")]
        public async Task ThenIAmRedirectToThankYouPage()
        {
            await _checkOutCompletePage.CheckIfOrderCreated();
        }
    }
}