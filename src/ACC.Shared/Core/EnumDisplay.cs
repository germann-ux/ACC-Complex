using System.ComponentModel.DataAnnotations;
using System.Collections.Concurrent;
using System.Reflection;

namespace ACC.Shared.Core; 
public static class EnumDisplay
{
    private static readonly ConcurrentDictionary<(Type, string), string> _cache = new();

    public static string Name<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var type = typeof(TEnum);
        var key = (type, value.ToString());

        return _cache.GetOrAdd(key, _ =>
        {
            var member = type.GetMember(value.ToString()).FirstOrDefault();
            if (member is null) return value.ToString();

            var attr = member.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? value.ToString();
        });
    }
}
