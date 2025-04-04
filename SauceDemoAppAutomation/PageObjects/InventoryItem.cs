using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class InventoryItem : BasePage
    {
        public InventoryItem(IPage page) : base(page) { }

        #region Locators

        private ILocator inventoryItem => _page.Locator("data-test=inventory-item");

        //Search for an item and check it's description and price
        public async Task VerifyInventoryItemDetails(string name, string description, decimal price)
        {
            var inventoryItemDiv = inventoryItem.
                Filter(new()
                {
                    Has = _page.GetByRole(AriaRole.Link, new() { NameString = name })
                });

            var itemDescription = await inventoryItemDiv.Locator("data-test=inventory-item-desc").InnerTextAsync();
            var text = await inventoryItemDiv.Locator("data-test=inventory-item-price").InnerTextAsync();
            decimal itemPrice;
            Decimal.TryParse(text.Split('$')[1].Trim(), out itemPrice);

            Assert.That(itemDescription, Is.EqualTo(description));
            Assert.That(itemPrice, Is.EqualTo(price));
        }

        #endregion
    }
}