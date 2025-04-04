using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class InventoryPage : BasePage
    {
        public InventoryPage(IPage page) : base(page) { }
         
        #region Locators
        private ILocator userName => _page.Locator("header_container >> header_secondary_container >> ").Locator("data-test=title");

        //private ILocator inventoryItem => _page.Locator("data-test=inventory-item");

        private class InventoryItem
        {
            IPage _page;

            private ILocator inventoryItem => _page.Locator("data-test=inventory-item");

            public InventoryItem(IPage page)
            {
                _page = page;
            }
            public async Task AddInventoryItemTOChartByName(string productName)
            {
                var addToChartButton = _page.GetByRole(AriaRole.Button, new() { NameString = "Add to cart" });
                var inventoryItemDiv = inventoryItem.
                    Filter(new()
                    {
                        Has = _page.GetByRole(AriaRole.Link, new() { NameString = "Sauce Labs Bike Light" })
                    });
                await inventoryItemDiv.Locator(addToChartButton).ClickAsync();
            }
            public async Task VerifyDetails(string name, string description, decimal price)
            {
                var inventoryItemDiv = inventoryItem.
                    Filter(new()
                    {
                        Has = _page.GetByRole(AriaRole.Link, new() { NameString = "Sauce Labs Bike Light" })
                    });

                await inventoryItemDiv.HighlightAsync();
                var itemName = await inventoryItemDiv.Locator("data-test=inventory-item-name").InnerTextAsync();
                var itemDescription = await inventoryItemDiv.Locator("data-test=inventory-item-desc").InnerTextAsync();
                var text = await inventoryItemDiv.Locator("data-test=inventory-item-price").InnerTextAsync();
                text = text.Split('$')[1].Trim();
                decimal itemPrice;
                Decimal.TryParse(text, out itemPrice);

                Assert.That(itemName, Is.EqualTo(name));
                Assert.That(itemDescription, Is.EqualTo(description));
                Assert.That(itemPrice, Is.EqualTo(price));
            }
        }

        private ILocator shoppingChart => Locators.GetShoppingChart(_page);

        private ILocator numberOfItemsFromChart => _page.Locator("data-test=shopping-cart-badge");

        private ILocator productHeader => _page.Locator("data-test=secondary-header").GetByText("Products");
        #endregion

        public async Task VerifyInventoryItemDetails(string name, string description, decimal price)
        {
            InventoryItem inventoryItem = new InventoryItem(_page);
            await inventoryItem.VerifyDetails(name, description, price);
        }

        public async Task AddInventoryItemToCart(string productName)
        {
            InventoryItem inventoryItem = new InventoryItem(_page);
            await inventoryItem.AddInventoryItemTOChartByName(productName);
        }

        public async Task<int> GetTotalNumberOfItemsFromCart()
        {
            int result;
            await shoppingChart.HighlightAsync();
            int.TryParse(await shoppingChart.InnerTextAsync(), out result);
            return result;
        }
        public async Task OpenCart()
        {
            await shoppingChart.ClickAsync();
            CartPage cartPage = new CartPage(_page);
            await cartPage.CheckIfCartPagePage();
        }
        public async Task CheckIfInventoryPage()
        {
            await productHeader.WaitForAsync();
        }
    }
}