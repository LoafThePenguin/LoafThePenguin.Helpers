using Moq;

namespace LoafThePenguin.Helpers.Tests;

public sealed class DisposeHelperTests
{
    private const int TIMEOUT = 1000;

    internal interface IDisposableObject : IDisposable, IAsyncDisposable
    {
        bool Disposed { get; set; }
    }

    [Fact(Timeout = TIMEOUT)]
    public void Is_DisposeObject_Correct() 
    {
        IDisposableObject obj = GetDisposableObject();

        DisposeHelper.DisposeObject(obj);

        Assert.True(obj.Disposed);
    }

    [Fact(Timeout = TIMEOUT)]
    public void Is_DisposeObject_Correct_Returns_True()
    {
        IDisposableObject obj = GetDisposableObject();

        Assert.True(DisposeHelper.DisposeObject(obj));
    }

    [Theory(Timeout = TIMEOUT)]
    [InlineData(1)]
    [InlineData("abc")]
    public void Is_DisposeObject_Correct_Returns_False(object? obj)
    {
        Assert.False(DisposeHelper.DisposeObject(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public void Is_DisposeObject_Correct_Returns_False_With_Null()
    {
        Assert.False(DisposeHelper.DisposeObject(null));
    }

    [Fact(Timeout = TIMEOUT)]
    public async void Is_DisposeObjectAsync_Correct()
    {
        IDisposableObject obj = GetDisposableObject();

        _ = await DisposeHelper.DisposeObjectAsync(obj);

        Assert.True(obj.Disposed);
    }

    [Fact(Timeout = TIMEOUT)]
    public async void Is_DisposeObjectAsync_Correct_Returns_True()
    {
        IDisposableObject obj = GetDisposableObject();

        Assert.True(await DisposeHelper.DisposeObjectAsync(obj));
    }

    [Theory(Timeout = TIMEOUT)]
    [InlineData(1)]
    [InlineData("abc")]
    public async void Is_DisposeObjectAsync_Correct_Returns_False(object? obj)
    {
        Assert.False(await DisposeHelper.DisposeObjectAsync(obj));
    }

    [Fact(Timeout = TIMEOUT)]
    public async void Is_DisposeObjectAsync_Correct_Returns_False_With_Null()
    {
        Assert.False(await DisposeHelper.DisposeObjectAsync(null));
    }
    private static IDisposableObject GetDisposableObject()
    {
        Mock<IDisposableObject> disposableMock = new();

        _ = disposableMock
            .SetupProperty(d => d.Disposed, false)
            .Setup(d => d.Dispose())
            .Callback(() => disposableMock.Object.Disposed = true);

        _ = disposableMock
            .Setup(d => d.DisposeAsync())
            .Returns(ValueTask.CompletedTask)
            .Callback(() => disposableMock.Object.Disposed = true);

        return disposableMock.Object;
    }
}
