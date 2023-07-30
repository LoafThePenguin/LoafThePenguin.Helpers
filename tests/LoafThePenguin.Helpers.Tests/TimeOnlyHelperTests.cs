using System.IO;

namespace LoafThePenguin.Helpers.Tests;

public sealed class TimeOnlyHelperTests
{
    [Fact(Timeout = 3)]
    public void Is_TimeNow_Correct()
    {
        var expected = TimeOnly.FromDateTime(DateTime.Now);
        TimeOnly actual = TimeOnlyHelper.TimeNow;
        var epsilon = TimeSpan.FromMilliseconds(1);
        TimeSpan difference = actual - expected;
        if (difference < TimeSpan.Zero)
        {
            difference *= -1;
        }

        Assert.True(difference < epsilon);
    }
}
