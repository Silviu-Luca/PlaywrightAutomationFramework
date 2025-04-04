using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class CheckOutOverviewTests : BaseTests
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckOutInformationsPage _checkOutInformationsPage;
        private CheckOutOverviewPage _checkOutOverviewPage;

        private LoginData _loginData;
        private ItemData _itemData;
        private PaymentData _paymentData;

        private decimal _taxPercent = 8.00M;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
            _inventoryPage = new InventoryPage(Page);
            _cartPage = new CartPage(Page);
            _checkOutInformationsPage = new CheckOutInformationsPage(Page);
            _checkOutOverviewPage = new CheckOutOverviewPage(Page);

            _loginData = LoginDataSource.GetLoginData();
            _itemData = ItemDataSource.GetItemData();
            _paymentData = PaymentDataSource.GetPaymentData();
        }

        [Test]
        [Category("regression")]
        public async Task VerifyCheckOutOverviewPage()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _inventoryPage.AddInventoryItemToCart(_itemData.Name);
            await _inventoryPage.OpenCart();
            await _cartPage.ClickOnCheckOut();
            await _checkOutInformationsPage.SetPaymentDetailsAndProceed(_paymentData.FirstName, _paymentData.LastName, _paymentData.PostalCode);
            await _checkOutOverviewPage.VerifyItemDetails(_itemData.Name, _itemData.Description, _itemData.Price);

            Assert.That(await _checkOutOverviewPage.GetPaymentInformation(), Is.EqualTo(_paymentData.PaymentInfo));
            Assert.That(await _checkOutOverviewPage.GetShippingInformation(), Is.EqualTo(_paymentData.ShippingInfo));

            decimal tax = Math.Round((_itemData.Price * _taxPercent / 100), 2);
            decimal totalPrice = _itemData.Price + tax;
            Assert.That(await _checkOutOverviewPage.GetTotalPrice(), Is.EqualTo(totalPrice));
        }
    }
}