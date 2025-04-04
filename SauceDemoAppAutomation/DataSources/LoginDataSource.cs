using PlaywrightAutomationFramework.SauceDemoAutomation.Models;

namespace PlaywrightAutomationFramework.SauceDemoAutomation.DataSources
{
    public class LoginDataSource
    {
        public static IEnumerable<LoginData> GetLoginDataSource()
        {
            LoginData[] login = new LoginData[]
            {
                new LoginData()
                {
                    User = "standard_user",
                    Pass = "secret_sauce"
                },
                new LoginData()
                {
                    User = "visual_user",
                    Pass = "secret_sauce"
                }
            };
            return login;
        }

        public static LoginData GetLoginData()
        {
            return new LoginData()
            {
                User = "standard_user",
                Pass = "secret_sauce"
            };
        }
    }
}
