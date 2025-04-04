
using Newtonsoft.Json;

namespace PlaywrightAutomationFramework.Helpers
{
    public class ObjectComparer
    {
        public static bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            string json1 = JsonConvert.SerializeObject(obj1);
            string json2 = JsonConvert.SerializeObject(obj2);

            return json1 == json2;
        }
    }
}
