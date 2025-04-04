using PlaywrightAutomationFramework.SauceDemoAutomation.Models;
using PlaywrightAutomationFramework.SauceDemoAutomation.PageObjects;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.DataSources
{
    public class PaymentDataSource
    {
        public static PaymentData GetPaymentData()
        {
            return new PaymentData()
            {
                FirstName = "Qa",
                LastName = "Tester",
                PostalCode = "IT",
                PaymentInfo = "SauceCard #31337",
                ShippingInfo = "Free Pony Express Delivery!"
            };
        }
    }
}
