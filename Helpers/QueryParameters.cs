namespace PlaywrightAutomationFramework.Helpers
{
    public class QueryParameters
    {
        public static string CreateQueryStringFromParams(Dictionary<string, string> queryStringParams)
        {
            var result = "";

            List<string> queryParamsList = new List<string>();
            foreach (var item in queryStringParams)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    queryParamsList.Add(item.Key + "=" + item.Value);
                }
            }

            if (queryParamsList.Count > 0)
            {
                result += "?" + string.Join("&", queryParamsList);
            }

            return result;
        }
    }
}
