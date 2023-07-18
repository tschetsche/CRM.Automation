using System.ComponentModel;

namespace CRM.Automation.Framework.Utils;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        return Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute
            ? value.ToString()
            : attribute.Description;
    }
}