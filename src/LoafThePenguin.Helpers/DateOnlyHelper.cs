namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы для работы с <see cref="DateOnly"/>
/// </summary>
public static class DateOnlyHelper
{
    /// <summary>
    /// Возвращает текущую дату.
    /// </summary>
    public static DateOnly DateToday => DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// Ищет и возвращает следующую дату дня недели от текущей даты.
    /// </summary>
    /// <param name="nextDayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly NextDayOfWeekDate(DayOfWeek nextDayOfWeek)
    {
        return DayOfWeekDate(DateToday, nextDayOfWeek, nextDayOfweek: true);
    }

    /// <summary>
    /// Ищет и возвращает следующую дату дня недели от даты <paramref name="fromDate"/>.
    /// </summary>
    /// <param name="fromDate">Дата от которой нужно найти дату следующего дня недели <paramref name="nextDayOfWeek"/>.</param>
    /// <param name="nextDayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly NextDayOfWeekDateFrom(DateOnly fromDate, DayOfWeek nextDayOfWeek)
    {
        return DayOfWeekDate(fromDate, nextDayOfWeek, nextDayOfweek: true);
    }

    /// <summary>
    /// Ищет и возвращает предыдущую дату дня недели от текущей даты.
    /// </summary>
    /// <param name="prevDayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly PrevDayOfWeekDate(DayOfWeek prevDayOfWeek)
    {
        return DayOfWeekDate(DateToday, prevDayOfWeek, nextDayOfweek: false);
    }

    /// <summary>
    /// Ищет и возвращает предыдущую дату дня недели от даты <paramref name="fromDate"/>..
    /// </summary>
    /// <param name="fromDate">Дата от которой нужно найти дату предыдущего дня недели <paramref name="prevDayOfWeek"/>.</param>
    /// <param name="prevDayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly PrevDayOfWeekDateFrom(DateOnly fromDate, DayOfWeek prevDayOfWeek)
    {
        return DayOfWeekDate(fromDate, prevDayOfWeek, nextDayOfweek: false);
    }

    private static DateOnly DayOfWeekDate(DateOnly fromDate, DayOfWeek dayOfWeek, bool nextDayOfweek)
    {
        int direction = nextDayOfweek
            ? 1
            : -1;

        DateOnly result = fromDate.AddDays(direction);
        while(result.DayOfWeek != dayOfWeek)
        {
            result = result.AddDays(direction);
        }

        return result;
    }
}
