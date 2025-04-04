using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class CheckOutInformationsTests : BaseTests
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckOutInformationsPage _checkOutInformationsPage;

        private LoginData _loginData;
        private ItemData _itemData;
        private PaymentData _paymentData;

        [SetUp]
        public async Task SetupPage()
        {
            _loginPage = new LoginPage(Page);
            _inventoryPage = new InventoryPage(Page);
            _cartPage = new CartPage(Page);
            _checkOutInformationsPage = new CheckOutInformationsPage(Page);

            _loginData = LoginDataSource.GetLoginData();
            _itemData = ItemDataSource.GetItemData();
            _paymentData = PaymentDataSource.GetPaymentData();

            await LoginAddItemToCartAndGoToCheckout();
        }

        public async Task LoginAddItemToCartAndGoToCheckout()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _inventoryPage.AddInventoryItemToCart(_itemData.Name);
            await _inventoryPage.OpenCart();
            await _cartPage.ClickOnCheckOut();
        }

        [Test]
        [Category("regression")]
        public async Task VerifyFirstNameIsRequired()
        {
            await _checkOutInformationsPage.SetLastName(_paymentData.LastName);
            await _checkOutInformationsPage.SetPostalCode(_paymentData.PostalCode);
            await _checkOutInformationsPage.ClickOnContinue();
            Assert.That(await _checkOutInformationsPage.GetErrorMessage(), Is.EqualTo("Error: First Name is required"));
        }

        [Test]
        [Category("regression")]
        public async Task VerifyLastNameIsRequired()
        {
            await _checkOutInformationsPage.SetFirstName(_paymentData.FirstName);
            await _checkOutInformationsPage.SetPostalCode(_paymentData.PostalCode);
            await _checkOutInformationsPage.ClickOnContinue();
            Assert.That(await _checkOutInformationsPage.GetErrorMessage(), Is.EqualTo("Error: Last Name is required"));
        }

        [Test]
        [Category("regression")]
        public async Task VerifyPostalCodeIsRequired()
        {
            await _checkOutInformationsPage.SetFirstName(_paymentData.FirstName);
            await _checkOutInformationsPage.SetLastName(_paymentData.LastName);
            await _checkOutInformationsPage.ClickOnContinue();
            Assert.That(await _checkOutInformationsPage.GetErrorMessage(), Is.EqualTo("Error: Postal Code is required"));
        }

        [Test]
        [Category("regression")]
        public async Task SetPaymentDetailsAndGoToNextPage()
        {
            await _checkOutInformationsPage.SetPaymentDetailsAndProceed(_paymentData.FirstName, _paymentData.LastName, _paymentData.PostalCode);
        }
    }
}