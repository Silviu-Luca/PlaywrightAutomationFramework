using Microsoft.Playwright;


namespace PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects
{
    public class MenuPage : BasePage
    {
        public MenuPage(IPage page) : base(page) { }

        #region Locators
        private ILocator _openMenuButton => _page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });

        private ILocator _allItems => _page.Locator("id=inventory_sidebar_link");

        private ILocator _about => _page.Locator("id=about_sidebar_link");

        private ILocator _logout => _page.Locator("id=logout_sidebar_link");
        private ILocator _resetAppState => _page.Locator("id=reset_sidebar_link");

        private ILocator _closeMenuButton => _page.GetByRole(AriaRole.Button, new() { Name = "Close Menu" });
        #endregion

        public async Task OpenMenu()
        {
            await _openMenuButton.ClickAsync();
            await _closeMenuButton.WaitForAsync();

        }
        public async Task CloseMenu()
        {
            await _closeMenuButton.ClickAsync();
            await _openMenuButton.WaitForAsync();
        }

        public async Task ClickOnAllItems()
        {
            await _allItems.ClickAsync();
            await _openMenuButton.WaitForAsync();
        }
    }
}