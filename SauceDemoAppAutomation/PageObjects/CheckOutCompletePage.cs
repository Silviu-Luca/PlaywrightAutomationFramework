using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class CheckOutCompletePage : BasePage
    {
        private InventoryPage _inventoryPage;

        public CheckOutCompletePage(IPage page) : base(page) 
        {
             _inventoryPage = new InventoryPage(page);
        }

        #region Locators
        private ILocator _backHome => _page.Locator("id=back-to-products");

        private ILocator _completeBookingThankYou => _page.Locator("data-test=complete-header").GetByText("Thank you for your order!");

        private ILocator _completeBookingDetails => _page.Locator("data-test=complete-text").GetByText("Your order has been dispatched, and will arrive just as fast as the pony can get there!");

        private ILocator _shoppingChart => Locators.GetShoppingChart(_page);
        #endregion

        public async Task CheckIfOrderCreated()
        {
            await _completeBookingThankYou.WaitForAsync();
            await _completeBookingThankYou.WaitForAsync();

            await _completeBookingDetails.WaitForAsync();
            await _completeBookingDetails.WaitForAsync();
        }

        public async Task ClickOnBackHome()
        {
            await _backHome.ClickAsync();
            await _inventoryPage.CheckIfInventoryPage();

        }
        public async Task CheckIfCheckOutCompletePage()
        {
            await _backHome.WaitForAsync();
        }
    }
}