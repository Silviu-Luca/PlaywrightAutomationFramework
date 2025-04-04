using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class InventoryTests : BaseTests
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;

        private LoginData _loginData;
        private ItemData _itemData;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
            _inventoryPage = new InventoryPage(Page);

            _loginData = LoginDataSource.GetLoginData();
            _itemData = ItemDataSource.GetItemData();
        }

        [Test]
        [Category("smoke")]
        public async Task VerifyCartIsUpdatedAfterAnItemIsAdded()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);

            int totalItems = await _inventoryPage.GetTotalNumberOfItemsFromCart();
            await _inventoryPage.AddInventoryItemToCart(_itemData.Name);
            int newTotalItems = await _inventoryPage.GetTotalNumberOfItemsFromCart();
            Assert.That(newTotalItems, Is.EqualTo(totalItems + 1));
        }

        [Test]
        [Category("regression")]
        public async Task VerifyInventoryItemDetails()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _inventoryPage.VerifyInventoryItemDetails(_itemData.Name, _itemData.Description, _itemData.Price);
        }
    }
}