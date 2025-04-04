using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class CheckOutOverviewPage : BasePage
    {
        private InventoryItem _inventoryItem;

        public CheckOutOverviewPage(IPage page) : base(page) 
        {
            _inventoryItem = new InventoryItem(page);
        } 
         
        #region Locators
        private ILocator _finish => _page.Locator("id=finish");

        private ILocator _shoppingChart => Locators.GetShoppingChart(_page);

        private ILocator _paymentInformation => _page.Locator("data-test=payment-info-value");
        private ILocator _shippingInformation => _page.Locator("data-test=shipping-info-value");
        private ILocator _totalPrice => _page.Locator("data-test=total-label");

        #endregion

        public async Task VerifyItemDetails(string name, string description, decimal price)
        {
            await _inventoryItem.VerifyInventoryItemDetails(name, description, price);
        }

        public async Task<string> GetPaymentInformation()
        {
            return await _paymentInformation.InnerTextAsync();
        }
        public async Task<string> GetShippingInformation()
        {
            return await _shippingInformation.InnerTextAsync();
        }
        public async Task<decimal> GetTotalPrice()
        {
            decimal total;
            string text = (await _totalPrice.InnerTextAsync()).Split("$")[1].Trim();
            Decimal.TryParse(text, out total);
            return total;
        }

        public async Task ClickOnFinish()
        {
            await _finish.ClickAsync();

        }
        public async Task CheckIfCheckOutOverviewPage()
        {
            await _finish.WaitForAsync();
        }
    }
}