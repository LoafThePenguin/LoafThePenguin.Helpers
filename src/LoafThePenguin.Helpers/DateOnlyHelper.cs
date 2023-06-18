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
    /// Ищет и возвращает следующую дату дня недели.
    /// </summary>
    /// <param name="dayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly NextDayOfWeekDate(DayOfWeek dayOfWeek)
    {
        return DayOfWeekDate(dayOfWeek, nextDayOfweek: true);
    }

    /// <summary>
    /// Ищет и возвращает предыдущую дату дня недели.
    /// </summary>
    /// <param name="dayOfWeek">День недели, дату которого нужно найти.</param>
    /// <returns>Дата дня недели.</returns>
    public static DateOnly PrevDayOfWeekDate(DayOfWeek dayOfWeek)
    {
        return DayOfWeekDate(dayOfWeek, nextDayOfweek: false);
    }

    private static DateOnly DayOfWeekDate(DayOfWeek dayOfWeek, bool nextDayOfweek)
    {
        int direction = nextDayOfweek
            ? 1
            : -1;

        DateOnly result = DateToday.AddDays(direction);
        while(result.DayOfWeek != dayOfWeek)
        {
            result = result.AddDays(direction);
        }

        return result;
    }
}
