using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class CompleteBookingFlow : BaseTests
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckOutInformationsPage _checkOutInformationsPage;
        private CheckOutOverviewPage _checkOutOverviewPage;
        private CheckOutCompletePage _checkOutCompletePage;

        private LoginData _loginData;
        private ItemData _itemData;
        private PaymentData _paymentData;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
            _inventoryPage = new InventoryPage(Page);
            _cartPage = new CartPage(Page);
            _checkOutInformationsPage = new CheckOutInformationsPage(Page);
            _checkOutOverviewPage = new CheckOutOverviewPage(Page);
            _checkOutCompletePage = new CheckOutCompletePage(Page);

            _loginData = LoginDataSource.GetLoginData();
            _itemData = ItemDataSource.GetItemData();
            _paymentData = PaymentDataSource.GetPaymentData();
        }

        [Test]
        [Category("regression")]
        public async Task CompleteBookingFlowAA()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _inventoryPage.AddInventoryItemToCart(_itemData.Name);
            await _inventoryPage.OpenCart();
            await _cartPage.ClickOnCheckOut();
            await _checkOutInformationsPage.SetPaymentDetailsAndProceed(_paymentData.FirstName, _paymentData.LastName, _paymentData.PostalCode);
            await _checkOutOverviewPage.ClickOnFinish();
            await _checkOutCompletePage.CheckIfOrderCreated();
            await _checkOutCompletePage.ClickOnBackHome();
        }
    }
}