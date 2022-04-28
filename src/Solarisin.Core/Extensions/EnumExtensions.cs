using System.ComponentModel;
using System.Reflection;

namespace Solarisin.Core.Extensions;

/// <summary>
/// Class extensions for <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieve the description attribute for the given enum value, or the name if the attribute does not exist.
    /// </summary>
    /// <param name="value">The enum value to retrieve the description for.</param>
    /// <returns>The description attribute string if found, name of the enum value otherwise.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the supplied enum value is not represented in the enumeration.</exception>
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value) ??
                   throw new InvalidOperationException($"Enum {type.Name} does not contain value {value}");
        var field = type.GetField(name) ??
                   throw new InvalidOperationException($"Enum {type.Name} does not contain field {name}");
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute != null ? attribute.Description : name;
    }
}