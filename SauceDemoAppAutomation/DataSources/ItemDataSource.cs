using PlaywrightAutomationFramework.SauceDemoAutomation.Models;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.DataSources
{
    public class ItemDataSource
    {
        public static ItemData GetItemData()
        {
            return new ItemData()
            {
                Name = "Sauce Labs Bike Light",
                Description = "A red light isn't the desired state in testing but it sure helps when riding your bike at night. Water-resistant with 3 lighting modes, 1 AAA battery included.",
                Price = 9.99M
            };
        }
    }
}
