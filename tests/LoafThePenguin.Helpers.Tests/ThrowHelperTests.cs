namespace LoafThePenguin.Helpers.Tests;
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена

public sealed class ThrowHelperTests
{
    private const int TIMEOUT = 3;

    private sealed class FooClass
    {
        public int Property { get; set; }
    }

    private sealed class ExceptionWithoutCtor : Exception
    {
        private ExceptionWithoutCtor()
        {

        }
    }

    private sealed class ExcetptionWithCtor : Exception
    {
        public ExcetptionWithCtor()
        {

        }

        public ExcetptionWithCtor(string message) : base(message)
        {

        }
    }

    private const string EXCEPTION_MESSAGE = "Сообщение исключения.";

    [Fact(Timeout = TIMEOUT)]
    public void Throw_Throws_ANE()
    {
        Assert.Throws<ArgumentNullException>(() => ThrowHelper.Throw<ExcetptionWithCtor>(null!));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Throw_Throws_IOE_NEED_AT_LEAST_A_MESSAGE()
    {
        Assert.Throws<InvalidOperationException>(() => ThrowHelper.Throw<ExcetptionWithCtor>());
    }

    [Fact(Timeout = TIMEOUT)]
    public void Throw_Throws_NRE()
    {
        Assert.Throws<ExcetptionWithCtor>(() => ThrowHelper.Throw<ExcetptionWithCtor>(EXCEPTION_MESSAGE));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Throw_Throws_NRE_MESSAGE_IS_CORRECT()
    {
        try
        {
            ThrowHelper.Throw<ExcetptionWithCtor>(EXCEPTION_MESSAGE);
        }
        catch (Exception ex)
        {
            Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
        }
    }

    [Fact(Timeout = TIMEOUT)]
    public void Throw_Throws_IOE_OBSCURE_EXCEPTION_THROWN()
    {
        Assert.Throws<InvalidOperationException>(() => ThrowHelper.Throw<ExceptionWithoutCtor>(EXCEPTION_MESSAGE));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowDisposed_Throws_ANE()
    {
        Assert.Throws<ArgumentNullException>(() => ThrowHelper.ThrowDisposed<Stream>(null!));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowDisposed_Throws_ObjectDisposedException()
    {
        using MemoryStream stream = new();
        stream.Dispose();
        Assert.Throws<ObjectDisposedException>(() => ThrowHelper.ThrowDisposed(stream));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfArgumentNull_Throws_ANE()
    {
        object? obj = null;

        Assert.Throws<ArgumentNullException>(() => ThrowHelper.ThrowIfArgumentNull(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfArgumentNull_Not_Throws_ANE()
    {
        object? obj = new();

        Assert.False(ThrowHelper.ThrowIfArgumentNull(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfNull_Throws_NRE()
    {
        object? obj = null;

        Assert.Throws<NullReferenceException>(() => ThrowHelper.ThrowIfNull(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfNull_Not_Throws_NRE()
    {
        object? obj = new();

        Assert.False(ThrowHelper.ThrowIfNull(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfArgumentOutOfRange_Throws_IOE_Max_Lower_Min()
    {
        int value = 30;
        int permissibleMinimum = 50;
        int permissibleMaximum = 30;

        Assert.Throws<InvalidOperationException>(() => ThrowHelper.ThrowIfArgumentOutOfRange(value, permissibleMinimum, permissibleMaximum));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfArgumentOutOfRange_Not_Throws_IOE_Max_Lower_Min()
    {
        int value = 30;
        int permissibleMaximum = 50;
        int permissibleMinimum = 30;

        Assert.False(ThrowHelper.ThrowIfArgumentOutOfRange(value, permissibleMinimum, permissibleMaximum));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfIndexLowerZero_Throws_IndexOutOfRangeException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => ThrowHelper.ThrowIfIndexLowerZero(-1));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfIndexLowerZero_Not_Throws_IndexOutOfRangeException()
    {
        Assert.False(ThrowHelper.ThrowIfIndexLowerZero(1));
    }

    [Fact(Timeout = TIMEOUT)]
    public void DoIfNull_Throws_ANE_If_Action_Null()
    {
        Assert.Throws<ArgumentNullException>(() => ThrowHelper.DoIfNull<FooClass>(null, null!));
    }

    [Fact(Timeout = TIMEOUT)]
    public void DoIfNull_Returns_False_When_Value_Not_Null()
    {
        int a = 1;

        Assert.False(ThrowHelper.DoIfNull(new FooClass(), () => a++));
    }

    [Fact(Timeout = TIMEOUT)]
    public void DoIfNull_Returns_True_When_Value_Null()
    {
        int a = 1;

        Assert.True(ThrowHelper.DoIfNull<FooClass>(null, () => a++));
    }

    [Fact(Timeout = TIMEOUT)]
    public void DoIfNull_Action_Invokes_When_Value_Null()
    {
        int a = 1;
        ThrowHelper.DoIfNull<FooClass>(null, () => a++);
        
        Assert.Equal(expected: 2, a);
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfAnyItemIsNull_Throws_ANE()
    {
        Assert.Throws<ArgumentNullException>(() => ThrowHelper.ThrowIfAnyItemIsNull(null!));
    }

    [Fact(Timeout = TIMEOUT)]
    public void ThrowIfAnyItemIsNull_Throws_NRE()
    {
        Assert.Throws<NullReferenceException>(() => ThrowHelper.ThrowIfAnyItemIsNull(new[] { "sadfasdf", null, ""}));
    }

}
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
