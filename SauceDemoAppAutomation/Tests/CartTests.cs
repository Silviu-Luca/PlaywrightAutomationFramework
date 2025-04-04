using PlaywrightAutomationFramework.SauceDemoAutomation.DataSources;
using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.Tests
{
    public class CartTests : BaseTests
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;

        private LoginData _loginData;
        private ItemData _itemData;

        [SetUp]
        public void SetupPage()
        {
            _loginPage = new LoginPage(Page);
            _inventoryPage = new InventoryPage(Page);
            _cartPage = new CartPage(Page);

            _loginData = LoginDataSource.GetLoginData();
            _itemData = ItemDataSource.GetItemData();
        }

        [Test]
        [Category("smoke")]
        public async Task AddItemToCartAndCheckDetails()
        {
            await _loginPage.Login(_loginData.User, _loginData.Pass);
            await _inventoryPage.AddInventoryItemToCart(_itemData.Name);
            await _inventoryPage.OpenCart();
            await _cartPage.VerifyInventoryItemDetails(_itemData.Name, _itemData.Description, _itemData.Price);
        }
    }
}