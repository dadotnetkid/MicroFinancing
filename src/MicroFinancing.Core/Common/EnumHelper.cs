using System.ComponentModel;

using MicroFinancing.Core.Attributes;

namespace MicroFinancing.Core.Common;

public static class EnumHelper
{
    public static string GetDescription(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .Description;
        }

        return string.Empty;
    }

    public static T GetDefault<T>(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(DefaultValueAttribute), false) is DefaultValueAttribute[] attributes && attributes.Any())
        {
            return (T)Convert.ChangeType(attributes.First()
                                                   .Value,
                                         typeof(T));
        }

        return default!;
    }

    public static string GetColor(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(ColorAttribute), false) is ColorAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .Color;
        }

        return string.Empty;
    }

    public static List<GenericDropItem<T>> GetDropDown<T>(this Type type)
    {
        return type.GetEnumValues()
            .OfType<T>()
            .ToDictionary(key => key, val => (val as Enum).GetDescription())
            .Select(c => new GenericDropItem<T>()
            {
                Value = c.Key,
                Text = c.Value
            }).ToList();
    }
}

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
public class GenericDropItem<T>
{
    public T Value { get; set; }
    public string Text { get; set; }
}