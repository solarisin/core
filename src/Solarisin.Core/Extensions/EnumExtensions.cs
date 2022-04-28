using System.ComponentModel;
using System.Reflection;

namespace Solarisin.Core.Extensions;

/// <summary>
/// Class extensions for <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieve the description attribute for the given enum value.
    /// </summary>
    /// <param name="value">The enum value to retrieve the description for.</param>
    /// <returns>The description attribute string.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the enum value or attribute is missing.</exception>
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value) ??
                   throw new InvalidOperationException($"Enum {type.Name} does not contain value {value}");
        var field = type.GetField(name) ??
                   throw new InvalidOperationException($"Enum {type.Name} does not contain field {name}");
        var attribute = field.GetCustomAttribute<DescriptionAttribute>() ??
                   throw new InvalidOperationException($"Enum {type.Name} does not contain description attribute for {name}");
        return attribute.Description;
    }
}