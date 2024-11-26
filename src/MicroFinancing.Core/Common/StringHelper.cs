using System.ComponentModel;

namespace MicroFinancing.Core.Common;

public static class StringHelper
{
    public static string GetDescription(this string enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration);

        if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .Description;
        }

        return string.Empty;
    }
}
