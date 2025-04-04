using Microsoft.Playwright;
using PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class CheckOutInformationsPage : BasePage
    {
        private InventoryItem _inventoryItem;
        private CheckOutOverviewPage _checkOutOverviewPage;

        public CheckOutInformationsPage(IPage page) : base(page) 
        {
            _inventoryItem = new InventoryItem(page);
            _checkOutOverviewPage = new CheckOutOverviewPage(page);
        }
         
        #region Locators

        private ILocator _shoppingChart => Locators.GetShoppingChart(_page);
        private ILocator _firstName => _page.Locator("id=first-name");
        private ILocator _lastName => _page.Locator("id=last-name");
        private ILocator _postalCode => _page.Locator("id=postal-code");

        private ILocator _continueButton => _page.Locator("id=continue");

        private ILocator _errorMessage => _page.Locator("data-test=error");

        #endregion

        public async Task SetFirstName(string firstName)
        {
            await this._firstName.FillAsync(firstName);
        }

        public async Task SetLastName(string lastName)
        {
            await this._lastName.FillAsync(lastName);
        }

        public async Task SetPostalCode(string postalCode)
        {
            await this._postalCode.FillAsync(postalCode);
        }

        public async Task SetPaymentDetails(string firstName, string lastName, string postalCode)
        {
            await SetFirstName(firstName);
            await SetLastName(lastName);
            await SetPostalCode(postalCode);
        }

        public async Task SetPaymentDetailsAndProceed(string firstName, string lastName, string postalCode)
        {
            await SetFirstName(firstName);
            await SetLastName(lastName);
            await SetPostalCode(postalCode);
            await ClickOnContinue();
            await _checkOutOverviewPage.CheckIfCheckOutOverviewPage();
        }

        public async Task ClickOnContinue()
        {
            await _continueButton.ClickAsync();
        }

        public async Task<string> GetErrorMessage()
        {
            return await _errorMessage.InnerTextAsync();
        }
    }
}