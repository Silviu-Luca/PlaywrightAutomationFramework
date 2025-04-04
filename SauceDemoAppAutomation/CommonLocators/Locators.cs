using Microsoft.Playwright;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.CommonLocators
{
    public class Locators
    {
        public static ILocator GetShoppingChart(IPage page)
        {
            return page.Locator("id=shopping_cart_container >> data-test=shopping-cart-link");
        }
    }
}