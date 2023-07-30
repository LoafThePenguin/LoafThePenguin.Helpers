namespace LoafThePenguin.Helpers.Tests;

public sealed class SetHelperTests
{
    private sealed class FooClass
    {
        private string? _myProperty1;

        public string? MyProperty1
        {
            get => _myProperty1;
            set
            {
                if(SetHelper.Set(ref _myProperty1, value))
                {
                    MyProperty1 = value;
                }
            }
        }
        public string? MyProperty2
        {
            get => _myProperty1;
            set
            {
                if(SetHelper.Set(ref _myProperty1, value))
                {
                    MyProperty1 = value;
                }
            }
        }
    }

    private const int TIMEOUT = 1000;

    [Theory(Timeout = TIMEOUT)]
    [InlineData("abc", null)]
    public void NullCheckSet_Throws_ANE_With_Objects<T>(T? field, T? value)
    {
        Assert.Throws<ArgumentNullException>(() => _ = SetHelper.NullCheckSet(ref field, value));
    }

    [Fact(Timeout = TIMEOUT)]
    public void NullCheckSet_Throws_ANE_With_Nullable_Structs()
    {
        int? field = 0;
        int? value = null;

        Assert.Throws<ArgumentNullException>(() => _ = SetHelper.NullCheckSet(ref field, value));
    }

    [Fact(Timeout = TIMEOUT)]
    public void NullCheckSet_Returns_False()
    {
        string field = "abc";
        string value = "abc";

        Assert.False(SetHelper.NullCheckSet(ref field, value));
    }

    [Fact(Timeout = TIMEOUT)]
    public void NullCheckSet_Returns_True()
    {
        string field = "abc";
        string value = "abcd";

        Assert.True(SetHelper.NullCheckSet(ref field, value));
    }

    [Theory(Timeout = TIMEOUT)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(null, 2)]
    [InlineData("abc", "abc")]
    [InlineData("abc", "abcd")]
    [InlineData(null, "abc")]
    public void NullCheckSet_Sets_Value<T>(T? field, T? value)
    {
        SetHelper.NullCheckSet(ref field, value);

        Assert.Equal(value, field);
    }

    [Fact(Timeout = TIMEOUT)]
    public void NullCheckSet_Calls_Callback_When_Returns_True()
    {
        string field = "abc";
        string value = "abcd";
        int callbackVariable = 0;

        SetHelper.NullCheckSet(ref field, value, () => ++callbackVariable);

        Assert.Equal(1, callbackVariable);
    }

    [Fact(Timeout = TIMEOUT)]
    public void NullCheckSet_Dont_Calls_Callback_When_Returns_False()
    {
        string field = "abc";
        string value = "abc";
        int callbackVariable = 0;

        SetHelper.NullCheckSet(ref field, value, () => ++callbackVariable);

        Assert.Equal(0, callbackVariable);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Returns_False()
    {
        string field = "abc";
        string value = "abc";

        Assert.False(SetHelper.Set(ref field, value));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Returns_True()
    {
        string field = "abc";
        string value = "abcd";

        Assert.True(SetHelper.Set(ref field, value));
    }

    [Theory(Timeout = TIMEOUT)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(null, 2)]
    [InlineData("abc", "abc")]
    [InlineData("abc", "abcd")]
    [InlineData(null, "abc")]
    [InlineData("abc", null)]
    public void Set_Sets_Value<T>(T? field, T? value)
    {
        SetHelper.Set(ref field, value);

        Assert.Equal(value, field);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Sets_Value_With_Nullable_Structs()
    {
        int? field = 0;
        int? value = null;

        SetHelper.Set(ref field, value);

        Assert.Equal(value, field);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Calls_Callback_When_Returns_True()
    {
        string field = "abc";
        string value = "abcd";
        int callbackVariable = 0;

        SetHelper.Set(ref field, value, () => ++callbackVariable);

        Assert.Equal(1, callbackVariable);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Dont_Calls_Callback_When_Returns_False()
    {
        string field = "abc";
        string value = "abc";
        int callbackVariable = 0;

        SetHelper.Set(ref field, value, () => ++callbackVariable);

        Assert.Equal(0, callbackVariable);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Is_Correct_With_Class_Properties()
    {
        string value = "abc";
        FooClass fooObject = new()
        {
            MyProperty1 = "abcde",
            MyProperty2 = "abc"
        };

        fooObject.MyProperty1 = value;

        Assert.Equal(fooObject.MyProperty2, value);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Set_Is_Correct_With_Class_Properties_In_Ctor()
    {
        FooClass fooObject = new()
        {
            MyProperty1 = "abcde",
            MyProperty2 = "abc"
        };

        Assert.Equal(fooObject.MyProperty2, fooObject.MyProperty1);
    }
}
