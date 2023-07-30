using System.ComponentModel.DataAnnotations;

namespace LoafThePenguin.Helpers.Tests;

public sealed class EnumHelperTests
{
    private const int TIMEOUT = 1000;
    private const string DISPLAY_NAME = "Отоборажаемое имя";
    private const string DISPLAY_DESCRIPTION = "Отоборажаемое описание";
    private const string DISPLAY_NAME_ONE = "Отоборажаемое имя 1";
    private const string DISPLAY_DESCRIPTION_ONE = "Отоборажаемое описание 1";

    private enum FooType
    {
        [Display(Name = DISPLAY_NAME_ONE, Description = DISPLAY_DESCRIPTION_ONE)]
        One = 1
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayDescription_Throws_ANE_TargetType_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayDescription(null, DISPLAY_DESCRIPTION));
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayDescription_Throws_ANE_DisplayDescription_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayDescription(typeof(FooType), null));
    }


    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayDescription_Gets_Value()
    {
        FooType expected = FooType.One;
        var actual = (FooType?)EnumHelper.GetEnumValueByDisplayDescription(typeof(FooType), DISPLAY_DESCRIPTION_ONE);

        Assert.Equal(expected, actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayDescription_Gets_Null()
    {
        var actual = (FooType?)EnumHelper.GetEnumValueByDisplayDescription(typeof(FooType), DISPLAY_DESCRIPTION);

        Assert.Null(actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayName_Gets_Value()
    {
        FooType expected = FooType.One;
        var actual = (FooType?)EnumHelper.GetEnumValueByDisplayName(typeof(FooType), DISPLAY_NAME_ONE);

        Assert.Equal(expected, actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayName_Gets_Null()
    {
        var actual = (FooType?)EnumHelper.GetEnumValueByDisplayName(typeof(FooType), DISPLAY_NAME);

        Assert.Null(actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayName_Throws_ANE_TargetType_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayName(null, DISPLAY_NAME));
    }

    [Fact(Timeout = TIMEOUT)]
    public void GetEnumValueByDisplayName_Throws_ANE_DisplayName_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayName(typeof(FooType), null));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayDescription_Gets_Value()
    {
        FooType expected = FooType.One;
        FooType? actual = EnumHelper.GetEnumValueByDisplayDescription<FooType>(DISPLAY_DESCRIPTION_ONE);

        Assert.Equal(expected, actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayDescription_Gets_Null()
    {
        FooType? actual = EnumHelper.GetEnumValueByDisplayDescription<FooType>(DISPLAY_DESCRIPTION);

        Assert.Null(actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayDescription_Throws_ANE_DisplayDescription_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayDescription<FooType>(null));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayName_Gets_Value()
    {
        FooType expected = FooType.One;
        FooType? actual = EnumHelper.GetEnumValueByDisplayName<FooType>(DISPLAY_NAME_ONE);

        Assert.Equal(expected, actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayName_Gets_Null()
    {
        FooType? actual = EnumHelper.GetEnumValueByDisplayName<FooType>(DISPLAY_NAME);

        Assert.Null(actual);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Generic_GetEnumValueByDisplayName_Throws_ANE_DisplayDescription_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => EnumHelper.GetEnumValueByDisplayName<FooType>(null));
    }
}
