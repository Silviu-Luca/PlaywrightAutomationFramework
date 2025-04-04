using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class CartPage : BasePage
    {
        private InventoryItem _inventoryItem;

        public CartPage(IPage page) : base(page)
        {
            _inventoryItem = new InventoryItem(page);
        }
         
        #region Locators

        private ILocator _shoppingChart => Locators.GetShoppingChart(_page);

        private ILocator _checkOut => _page.Locator("#checkout");

        #endregion

        public async Task VerifyInventoryItemDetails(string name, string description, decimal price)
        {
            await _inventoryItem.VerifyInventoryItemDetails(name, description, price);
        }

        public async Task ClickOnCheckOut()
        {
            await _checkOut.ClickAsync();
        }

        public async Task CheckIfCartPagePage()
        {
            await _checkOut.WaitForAsync();
        }
    }
}