using TechTalk.SpecFlow.Assist;

namespace CRM.Automation.Tests.Extensions;

public class ListValueRetriever : IValueRetriever
{
    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        if (propertyType == typeof(List<string>))
        {
            return !string.IsNullOrEmpty(keyValuePair.Value)
                ? keyValuePair.Value.Split(',').ToList()
                : new List<string>();
        }

        return new List<string>();
    }

    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        return propertyType == typeof(List<string>);
    }
}