using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static LoafThePenguin.Helpers.ThrowHelper;

namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы для работы с <see cref="Enum"/>.
/// </summary>
/// <exception cref="InvalidOperationException"/>
/// <exception cref="ArgumentNullException"/>
public static class EnumHelper
{
    /// <summary>
    /// Возвращает именованную константу <see cref="Enum"/>,
    /// по её отображаемому описанию <see cref="DisplayAttribute.Description"/>.
    /// </summary>
    /// <param name="targetType">Тип именованной константы.</param>
    /// <param name="displayDescription">
    /// Отображаемое описание именованной константы <see cref="DisplayAttribute.Description"/>.
    /// </param>
    /// <returns>Именованная константа.</returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает когда в типе именованных констант, 
    /// есть больше чем одна именованная константа 
    /// с одним отображаемым описанием <see cref="DisplayAttribute.Description"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Возникает когда <paramref name="targetType"/> или <paramref name="displayName"/>
    /// оказываются <see langword="null"/>.
    /// </exception>
    public static Enum? GetEnumValueByDisplayDescription(Type targetType, string displayDescription)
    {
        _ = ThrowIfArgumentNull(targetType);
        _ = ThrowIfArgumentNull(displayDescription);

        return GetEnumValueByDisplayProperty(
            targetType,
            displayDescription,
            d => d?.Description);
    }

    /// <summary>
    /// Возвращает именованную константу <see cref="Enum"/>,
    /// по её отображаемому имени <see cref="DisplayAttribute.Name"/>.
    /// </summary>
    /// <param name="targetType">Тип именованной константы.</param>
    /// <param name="displayName">
    /// Отображаемое имя именованной константы <see cref="DisplayAttribute.Name"/>.
    /// </param>
    /// <returns>Именованная константа.</returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает когда в типе именованных констант, 
    /// есть больше чем одна именованная константа 
    /// с одним отображаемым именем <see cref="DisplayAttribute.Name"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Возникает когда <paramref name="targetType"/> или <paramref name="displayName"/>
    /// оказываются <see langword="null"/>.
    /// </exception>
    public static Enum? GetEnumValueByDisplayName(Type targetType, string displayName)
    {
        _ = ThrowIfArgumentNull(targetType);
        _ = ThrowIfArgumentNull(displayName);

        return GetEnumValueByDisplayProperty(
            targetType,
            displayName,
            d => d?.Name);
    }

    private static Enum? GetEnumValueByDisplayProperty(Type targetType, string displayDescription, Func<DisplayAttribute?, string?> selector)
    {
        MemberInfo? memberInfo = targetType
            .GetMembers()
            .SingleOrDefault(m => selector(m.GetCustomAttribute<DisplayAttribute>())?.Equals(displayDescription) ?? false);

        if (memberInfo is null)
        {
            return null;
        }

        Array values = Enum.GetValues(targetType);

        foreach (object value in values)
        {
            if (value.ToString() == memberInfo.Name)
            {
                return value as Enum;
            }
        }

        return null;
    }
}
