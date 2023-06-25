namespace LoafThePenguin.Helpers.Tests;

#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
public sealed class DateOnlyHelperTests
{
    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDateFrom_Monday_Correct()
    {
        IsPrevDayOfWeekFromDateCorrect(new DateOnly(2023, 6, 26), DayOfWeek.Sunday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDateFrom_Monday_Correct()
    {
        IsNextDayOfWeekFromDateCorrect(new DateOnly(2023, 6, 21), DayOfWeek.Friday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Monday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Monday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Tuesday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Tuesday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Wednesday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Wednesday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Thursday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Thursday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Friday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Friday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Saturday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Saturday);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Sunday_Correct()
    {
        IsPrevDayOfWeekDateCorrect(DayOfWeek.Saturday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Monday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Monday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Tuesday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Tuesday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Wednesday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Wednesday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Thursday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Thursday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Friday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Friday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Saturday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Saturday);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Sunday_Correct()
    {
        IsNextDayOfWeekDateCorrect(DayOfWeek.Sunday);
    }

    [Fact(Timeout = 3)]
    public void Is_DateToday_Whole_Date_Correct()
    {
        DateTime dateTimeNow = DateTime.Now;
        DateOnly dateNow = DateOnlyHelper.DateToday;

        Assert.True(
            dateTimeNow.Day == dateNow.Day
            && dateTimeNow.Month == dateNow.Month
            && dateTimeNow.Year == dateNow.Year);
    }

    [Fact(Timeout = 3)]
    public void Is_DateToday_Day_Of_Week_Correct()
    {
        DayOfWeek expected = DateTime.Now.DayOfWeek;
        DateOnly dateNow = DateOnlyHelper.DateToday;

        Assert.Equal(expected, dateNow.DayOfWeek);
    }

    [Fact(Timeout = 3)]
    public void Is_DateToday_Day_Correct()
    {
        int expected = DateTime.Now.Day;
        DateOnly dateNow = DateOnlyHelper.DateToday;

        Assert.Equal(expected, dateNow.Day);
    }

    [Fact(Timeout = 3)]
    public void Is_DateToday_Month_Correct()
    {
        int expected = DateTime.Now.Month;
        DateOnly dateNow = DateOnlyHelper.DateToday;

        Assert.Equal(expected, dateNow.Month);
    }

    [Fact(Timeout = 3)]
    public void Is_DateToday_Year_Correct()
    {
        int expected = DateTime.Now.Year;
        DateOnly dateNow = DateOnlyHelper.DateToday;

        Assert.Equal(expected, dateNow.Year);
    }

    private static void IsPrevDayOfWeekFromDateCorrect(DateOnly fromDate, DayOfWeek prevDayOfWeek)
    {
        DateOnly actual = DateOnlyHelper.PrevDayOfWeekDateFrom(fromDate, prevDayOfWeek);
        DateOnly expectedDate = DayOfWeekDate(fromDate, prevDayOfWeek, nextDayOfweek: false);

        Assert.Equal(expectedDate, actual);
    }

    private static void IsNextDayOfWeekFromDateCorrect(DateOnly fromDate, DayOfWeek prevDayOfWeek)
    {
        DateOnly actual = DateOnlyHelper.NextDayOfWeekDateFrom(fromDate, prevDayOfWeek);
        DateOnly expectedDate = DayOfWeekDate(fromDate, prevDayOfWeek, nextDayOfweek: true);

        Assert.Equal(expectedDate, actual);
    }

    private static void IsPrevDayOfWeekDateCorrect(DayOfWeek prevDayOfWeek)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        DateOnly actual = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = DayOfWeekDate(dateNow, prevDayOfWeek, nextDayOfweek: false);

        Assert.Equal(expectedDate, actual);
    }

    private static void IsNextDayOfWeekDateCorrect(DayOfWeek nextDayOfWeek)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        DateOnly actual = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = DayOfWeekDate(dateNow, nextDayOfWeek, nextDayOfweek: true);

        Assert.Equal(expectedDate, actual);
    }

    private static DateOnly DayOfWeekDate(DateOnly fromDate, DayOfWeek dayOfWeek, bool nextDayOfweek)
    {
        int direction = nextDayOfweek
            ? 1
            : -1;

        DateOnly result = fromDate.AddDays(direction);
        while (result.DayOfWeek != dayOfWeek)
        {
            result = result.AddDays(direction);
        }

        return result;
    }
}
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
