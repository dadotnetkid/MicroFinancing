using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MicroFinancing.Infrastructure.Common;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var _enum = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ??
                    enumValue.GetType()
                                   .GetMember(enumValue.ToString())
                                   .FirstOrDefault()?
                                   .GetCustomAttribute<DescriptionAttribute>()?
                                   .Description ??
                    enumValue.ToString();

        return _enum;
    }


    public static Dictionary<T, string> ToDictionary<T>() where T : struct
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(e => e, e => (e as Enum).GetDisplayName());
    }
}