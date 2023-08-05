using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Linq;
using static LoafThePenguin.Helpers.Internal.ExceptionMessages;

namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы выброса исключений.
/// </summary>
/// <exception cref="InvalidOperationException"/>
/// <exception cref="ObjectDisposedException"/>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="NullReferenceException"/>
/// <exception cref="ArgumentOutOfRangeException"/>
/// <exception cref="IndexOutOfRangeException"/>
public static class ThrowHelper
{
    private const string VALUE_ARGUMENT_NAME = "value";
    private const string SEQUENCE_ARGUMENT_NAME = "sequence";

    /// <summary>
    /// Выбрасывает исключения.
    /// </summary>
    /// <typeparam name="T">
    /// Тип выбрасываемого исключения.
    /// </typeparam>
    /// <param name="args">
    /// Массив аргументов выбрасываемого исключения.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Возникает, когда массив <paramref name="args"/> пуст. Либо когда исключение неопределенно.
    /// </exception>
    /// <remarks>
    /// Массив <paramref name="args"/> должен содержать хотя бы один аргумент - сообщение.
    /// </remarks>
    public static void Throw<T>(params object?[] args)
        where T : Exception
    {
        ThrowIfArgumentNull(args);

        if (args.Length < 1)
        {
            Throw<InvalidOperationException>(NEED_AT_LEAST_A_MESSAGE);
        }

        Exception exception;
        try
        {
            exception = (T)Activator.CreateInstance(typeof(T), args)!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(OBSCURE_EXCEPTION_THROWN, ex);
        }

        throw exception;
    }

    /// <summary>
    /// Выбрасывает исключение <see cref="ObjectDisposedException"/>.
    /// </summary>
    /// <typeparam name="TDisposed">Тип высвобожденного объекта.</typeparam>
    /// <param name="value">Высвобожденный объект.</param>
    /// <param name="argumentName">Имя высвобожденного объекта.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если <paramref name="value"/> является <see langword="null"/>.
    /// </exception>
    public static void ThrowDisposed<TDisposed>(
        TDisposed value,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
        where TDisposed : IDisposable
    {
        ThrowIfArgumentNull(value);

        Throw<ObjectDisposedException>(argumentName);
    }

    /// <summary>
    /// Выбрасывает <see cref="ArgumentNullException"/>, когда <paramref name="value"/> - <see langword="null"/>.
    /// </summary>
    /// <param name="value">Проверяемый объект.</param>
    /// <param name="argumentName">Имя аргумента.</param>
    /// <returns>
    /// <see langword="false"/>, если <paramref name="value"/> не <see langword="null"/>,
    /// иначе - выбрасывает исключение <see cref="ArgumentNullException"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, когда <paramref name="value"/> - <see langword="null"/>.</exception>
    public static bool ThrowIfArgumentNull(
        [NotNullWhen(false)] object? value,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
    {
        if (value is null)
        {
            Throw<ArgumentNullException>(argumentName);
        }

        return false;
    }

    /// <summary>
    /// Выбрасывает <see cref="NullReferenceException"/>, когда <paramref name="value"/> - <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">Тип параметра <paramref name="value"/>.</typeparam>
    /// <param name="value">Проверяемый объект.</param>
    /// <param name="argumentName">Имя аргумента.</param>
    /// <returns>
    /// <see langword="false"/>, если <paramref name="value"/> не <see langword="null"/>,
    /// иначе - выбрасывает исключение <see cref="NullReferenceException"/>.
    /// </returns>
    /// <exception cref="NullReferenceException">Выбрасывается, когда <paramref name="value"/> - <see langword="null"/>.</exception>
    public static bool ThrowIfNull<T>(
        [NotNullWhen(false)] T? value,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
    {
        if (value is null)
        {
            Throw<NullReferenceException>(
                string.Format(NULL_REFERENCE,
                argumentName,
                typeof(T).Name));
        }

        return false;
    }

    /// <summary>
    /// Выбрасывает исключение <see cref="ArgumentOutOfRangeException"/>, когда <paramref name="value"/>
    /// выходит за границы.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <param name="permissibleMinimum">
    /// Минимум, который может принимать <paramref name="value"/>.
    /// </param>
    /// <param name="permissibleMaximum">
    /// Максимум, который может принимать <paramref name="value"/>.
    /// </param>
    /// <param name="argumentName">Имя аргумента.</param>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, когда <paramref name="permissibleMaximum"/>
    /// меньше, чем <paramref name="permissibleMinimum"/>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Выбрасывается, когда <paramref name="value"/>
    /// выходит за границы.
    /// </exception>
    /// <returns><see langword="false"/>, если <paramref name="value"/> находится в границах.</returns>
    public static bool ThrowIfArgumentOutOfRange<T>(
        T value,
        T permissibleMinimum,
        T permissibleMaximum,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
        where T : IComparisonOperators<T, T, bool>, INumber<T>, IFormattable

    {
        static string Format(T value)
        {
            return value.ToString(format: null, CultureInfo.InvariantCulture);
        }

        if (permissibleMaximum < permissibleMinimum)
        {
            Throw<InvalidOperationException>(string.Format(
                ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER,
                nameof(permissibleMaximum),
                nameof(permissibleMinimum)));
        }

        if (value < permissibleMinimum)
        {
            Throw<ArgumentOutOfRangeException>(
                argumentName,
                string.Format(ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER,
                              argumentName,
                              Format(permissibleMinimum)));
        }

        if (value > permissibleMaximum)
        {
            Throw<ArgumentOutOfRangeException>(
                argumentName,
                string.Format(ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER,
                              argumentName,
                              Format(permissibleMaximum)));
        }

        return false;
    }

    /// <summary>
    /// Выбрасывается <see cref="IndexOutOfRangeException"/>, когда значение индекса <paramref name="index"/>
    /// меньше 0.
    /// </summary>
    /// <param name="index">
    /// Значение индекса.
    /// </param>
    /// <exception cref="IndexOutOfRangeException">
    /// Выбрасывается, когда <paramref name="index"/> меньше 0.
    /// </exception>
    /// <returns>
    /// <see langword="false"/>, если когда значение индекса больше либо равно 0.
    /// </returns>
    public static bool ThrowIfIndexLowerZero(int index)
    {
        if (index < 0)
        {
            Throw<IndexOutOfRangeException>(INDEX_CANT_BE_LOWER_TAHN_ZERO);
        }

        return false;
    }

    /// <summary>
    /// Вызывает процедуру <paramref name="action"/>, когда <paramref name="value"/> - <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">Тип параметра <paramref name="value"/>.</typeparam>
    /// <param name="value">Значение.</param>
    /// <param name="action">Процедура, которая вызывается, когда <paramref name="value"/> - <see langword="null"/>.</param>
    /// <returns>
    /// <see langword="true"/>, если <paramref name="value"/> - <see langword="null"/>,
    /// иначе - <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Вызывается, когда <paramref name="action"/> - <see langword="null"/>.</exception>
    public static bool DoIfNull<T>([NotNullWhen(false)] T? value, Action action)
    {
        ThrowIfArgumentNull(action);
        if (value is null)
        {
            action();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Выбрасывает <see cref="NullReferenceException"/>,
    /// если в последовательности <paramref name="sequence"/> имеется элемент, который является <see langword="null"/>.
    /// </summary>
    /// <param name="sequence">Последовательность.</param>
    /// <param name="argumentName">Имя аргумента последовательности.</param>
    /// <returns>
    /// <see langword="false"/> если в последовательности нет элементов, которые <see langword="null"/>,
    /// иначе выбрасывает исключение <see cref="NullReferenceException"/>.
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// Выбрасывается, если в последовательности <paramref name="sequence"/> имеется элемент, который является <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если <paramref name="sequence"/> является <see langword="null"/>.
    /// </exception>
    public static bool ThrowIfAnyItemIsNull(
        IEnumerable sequence,
        [CallerArgumentExpression(SEQUENCE_ARGUMENT_NAME)] string? argumentName = null)
    {
        ThrowIfArgumentNull(sequence);
        if (sequence.Cast<object>().Any(o => o is null))
        {
            Throw<NullReferenceException>(string.Format(SEQUENCE_HAS_NULL_REFERENCE, argumentName));
        }

        return false;
    }
}
