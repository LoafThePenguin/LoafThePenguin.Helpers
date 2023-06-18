namespace LoafThePenguin.Helpers.Tests;

#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
public sealed class DateOnlyHelperTests
{
    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Monday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Monday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -6);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Tuesday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Tuesday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -5);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Wednesday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Wednesday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -4);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Thursday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Thursday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -3);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Friday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Friday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -2);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Saturday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Saturday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -1);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_PrevDayOfWeekDate_Sunday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek prevDayOfWeek = DayOfWeek.Sunday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.PrevDayOfWeekDate(prevDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: -7);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Monday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Monday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 1);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Tuesday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Tuesday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 2);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Wednesday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Wednesday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 3);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Thursday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Thursday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 4);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Friday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Friday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 5);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Saturday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Saturday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 6);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
    }

    [Fact(Timeout = 3)]
    public void Is_NextDayOfWeekDate_Sunday_Correct()
    {
        DateOnly todayDateExample = new(year: 2023, month: 6, day: 18);
        DayOfWeek nextDayOfWeek = DayOfWeek.Sunday;

        DateOnly nextDayOfWeekDate = DateOnlyHelper.NextDayOfWeekDate(nextDayOfWeek);
        DateOnly expectedDate = todayDateExample.AddDays(value: 7);

        Assert.Equal(expectedDate, nextDayOfWeekDate);
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
}
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
