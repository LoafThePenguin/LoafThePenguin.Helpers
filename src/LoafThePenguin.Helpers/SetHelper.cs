using System.Runtime.CompilerServices;
using static LoafThePenguin.Helpers.ThrowHelper;

namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы сохранения значений в полях класса.
/// </summary>
public static class SetHelper
{
    /// <summary>
    /// 123123
    /// </summary>
    private const string VALUE_ARGUMENT_NAME = "value";

    /// <summary>
    /// Проверяет <paramref name="value"/> на <see langword="null"/> ссылку 
    /// и устанвливает его значение полю <paramref name="field"/>, 
    /// если это значение не является <see langword="null"/> ссылкой. 
    /// </summary>
    /// <typeparam name="T">Тип поля.</typeparam>z
    /// <param name="field">Изменяемое поле.</param>
    /// <param name="value">Новое значение поля.</param>
    /// <param name="argumentName">
    /// Наименование аргумента. 
    /// По-умолчанию - наименование входного параметра <paramref name="value"/>.
    /// </param>
    /// <param name="callback">
    /// Делегат, выполняющийся, после того, 
    /// как у поля <paramref name="field"/> изменилось значение на <paramref name="value"/>. 
    /// По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> если значение поля <paramref name="field"/> изменилось, иначе - <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Вызывается, если <paramref name="field"/> является <see langword="null"/> ссылкой.
    /// </exception>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// Этот метод не вызывает рекурсии, 
    /// в случае если два разных свойства ссылаются друг на друга 
    /// и пытаются изменить одно и тоже поле на одно и тоже значение.
    /// </item>
    /// <item>
    /// Вызывает <paramref name="callback"/> только после успешного присваивания, и если делегат не <see langword="null"/>.
    /// </item>
    /// <item>
    /// Вызывает <see cref="ArgumentNullException"/>, если <paramref name="field"/> является <see langword="null"/> ссылкой.
    /// </item>
    /// </list>
    /// </remarks>
    public static bool NullCheckSet<T>
        (ref T? field,
        T value,
        Action? callback = null,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
    {
        ThrowIfArgumentNull(value, argumentName);

        return Set(ref field, value, callback);
    }

    /// <summary>
    /// Устанавливает полю <paramref name="field"/> значение <paramref name="value"/>. 
    /// </summary>
    /// <typeparam name="T">Тип поля.</typeparam>
    /// <param name="field">Изменяемое поле.</param>
    /// <param name="value">Новое значение поля.</param>
    /// <param name="callback">
    /// Делегат, выполняющийся, после того, 
    /// как у поля <paramref name="field"/> изменилось значение на <paramref name="value"/>. 
    /// По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> если значение поля <paramref name="field"/> изменилось, иначе - <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// Этот метод не вызывает рекурсии, 
    /// в случае если два разных свойства ссылаются друг на друга 
    /// и пытаются изменить одно и тоже поле на одно и тоже значение.
    /// </item>
    /// <item>
    /// Вызывает <paramref name="callback"/> только после успешного присваивания, и если делегат не <see langword="null"/>.
    /// </item>
    /// </list>
    /// </remarks>
    public static bool Set<T>(ref T? field, T? value, Action? callback = null)
    {
        if (field?.Equals(value) ?? value is null)
        {
            return false;
        }

        field = value;
        callback?.Invoke();

        return true;
    }
}
