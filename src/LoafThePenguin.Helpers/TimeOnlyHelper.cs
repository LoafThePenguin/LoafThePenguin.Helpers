namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы для работы с <see cref="TimeOnly"/>
/// </summary>
public static class TimeOnlyHelper
{
    /// <summary>
    /// Возвращает текущее время.
    /// </summary>
    public static TimeOnly TimeNow => TimeOnly.FromDateTime(DateTime.Now);
}
