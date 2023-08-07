using System.Net;
using System.Text.Json;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.ApiRequest.Internal;
using LoafThePenguin.ApiRequest.Tests.Foos;

namespace LoafThePenguin.ApiRequest.Tests;

public sealed class ApiRequestSenderTests
{
    private const int TIMEOUT = 1000;

    #region Tests when ApiRequestSender is disposed.

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс исключения {nameof(ObjectDisposedException)} " +
                      $"при попытке вызвать метод {nameof(ApiRequestSender.ConfigureRequest)}, " +
                      $"после вызова {nameof(ApiRequestMessage.Dispose)}")]
    public void ApiRequestSender_Try_To_Call_ConfigureRequest_When_Instance_Is_Disposed()
    {
        ApiRequestSender sender = new();
        sender.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = sender.ConfigureRequest(Constants.LOCALHOST_TEST_URI, HttpMethod.Get));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс исключения {nameof(ObjectDisposedException)} " +
                      $"при попытке вызвать метод {nameof(ApiRequestSender.SendAsync)}," +
                      $"после вызова {nameof(ApiRequestMessage.Dispose)}")]
    public async Task ApiRequestSender_Try_To_Call_SendAsync_When_Instance_Is_Disposed()
    {
        ApiRequestSender sender = new();
        sender.Dispose();

        using HttpRequestMessage message = new();

        await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = await sender.SendAsync(message));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"{nameof(ApiRequestMessage.Dispose)} может вызываться дважды")]
    public void ApiRequestSender_Can_Calls_Twice()
    {
        ApiRequestSender sender = new();
        sender.Dispose();

        try
        {
            sender.Dispose();
        }
        catch ( Exception ex )
        {
            Assert.Fail($"Было выброшено исключение типа {ex.GetType()}: {ex.Message}");
        }
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"{nameof(ApiRequestMessage.Dispose)} может вызываться сколько угодно")]
    public void ApiRequestSender_Can_Calls_ManyTimes()
    {
        ApiRequestSender sender = new();
        sender.Dispose();

        try
        {
            sender.Dispose();
            sender.Dispose();
            sender.Dispose();
            sender.Dispose();
            sender.Dispose();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Было выброшено исключение типа {ex.GetType()}: {ex.Message}");
        }
    }

    #endregion

    #region Tests when methods parameters are null

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс {nameof(ArgumentNullException)}, " +
                      $"если вызвать {nameof(ApiRequestSender.ConfigureRequest)} " +
                      $"и передать в качестве параметра uri - null")]
    public void ApiRequestSender_ConfigureRequest_Throws_ArgumentNullException_With_Null_As_Uri()
    {
        using ApiRequestSender sender = new();

        Assert.Throws<ArgumentNullException>(() => sender.ConfigureRequest(null, HttpMethod.Get));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс {nameof(ArgumentNullException)}, " +
                      $"если вызвать {nameof(ApiRequestSender.ConfigureRequest)} " +
                      $"и передать в качестве параметра method - null")]
    public void ApiRequestSender_ConfigureRequest_Throws_ArgumentNullException_With_Null_As_Method()
    {
        using ApiRequestSender sender = new();

        Assert.Throws<ArgumentNullException>(() => sender.ConfigureRequest(Constants.LOCALHOST_TEST_URI, null));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс {nameof(ArgumentNullException)}, " +
                      $"если вызвать {nameof(ApiRequestSender.SendAsync)} " +
                      $"и передать в качестве параметра httpRequestMessage - null")]
    public async Task ApiRequestSender_SendAsync_Throws_ArgumentNullException_With_Null_As_HttpRequestMessage()
    {
        using ApiRequestSender sender = new();

        await Assert.ThrowsAsync<ArgumentNullException>(() => sender.SendAsync(null));
    }

    #endregion

    #region Tests when methods parameters are empty strings

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс {nameof(InvalidOperationException)}, " +
        $"если вызвать {nameof(ApiRequestSender.ConfigureRequest)} " +
        $"и передать в качестве параметра uri пустую строку")]
    [InlineData("")]
    [InlineData("       ")]
    public void ApiRequestSender_ConfigureRequest_Throws_InvalidOperationException_With_Empty_String_As_Uri(string uri)
    {
        using ApiRequestSender sender = new();

        Assert.Throws<InvalidOperationException>(() => sender.ConfigureRequest(uri, HttpMethod.Get));
    }

    #endregion

    #region Events Rise

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Событие {nameof(ApiRequestSender.Disposed)} " +
                      $"вызывается при вызове метода {nameof(ApiRequestSender.Dispose)}")]
    public void ApiRequestSender_Disposed_Rises_After_Dispose_Call()
    {
        using ApiRequestSender sender = new();

        Assert.Raises<EventArgs>(
            d => sender.Disposed += d,
            d => sender.Disposed -= d,
            () => sender.Dispose());
    }

    #endregion

    #region Returns of SendAsync tests

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestSender.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.ResponseStream)}")]
    [MemberData(nameof(ConfigureHttpClients))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_Correct_Value<T>(HttpClient httpClient, T expectedObject)
    {
        using ApiRequestSender sender = new(httpClient);
        using IApiResponse response = await sender
            .SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.LOCALHOST_TEST_URI))
            .ConfigureAwait(continueOnCapturedContext: false);

        T? actual = await JsonSerializer.DeserializeAsync<T>(response.ResponseStream);

        Assert.NotNull(actual);
        Assert.Equal(expectedObject, actual, new JsonResponseTestClass.JsonResponseTestClassComparer<T>());
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestSender.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.StatusCode)}")]
    [MemberData(nameof(GetHttpClientWithDifferentStatusCode))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_ResponseCode_Value<T>(HttpClient httpClient, int expected)
    {
        using ApiRequestSender sender = new(httpClient);
        using IApiResponse response = await sender
            .SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.LOCALHOST_TEST_URI))
            .ConfigureAwait(continueOnCapturedContext: false);

        Assert.Equal(expected, response.StatusCode);
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestSender.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.IsSuccess)}")]
    [MemberData(nameof(GetHttpClientWithDifferentStatusCode))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_IsSuccess_Value<T>(HttpClient httpClient, int expectedStatusCode)
    {
        using ApiRequestSender sender = new(httpClient);
        using IApiResponse response = await sender
            .SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.LOCALHOST_TEST_URI))
            .ConfigureAwait(continueOnCapturedContext: false);

        bool expected = expectedStatusCode >= 200 && expectedStatusCode <= 299;

        Assert.Equal(expected, response.IsSuccess);
    }

    #endregion

    #region Cancelation Tests

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestSender.SendAsync)} " +
                      $"прерывается с помощью {nameof(CancellationToken)}")]
    public async Task ApiRequestSender_SendAsync_Can_Be_Cancel()
    {
        using HttpClient httpClient = Mocks.HttpClient(new JsonResponseTestClass(), HttpStatusCode.OK);
        using ApiRequestSender sender = new(httpClient);

        using CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromMicroseconds(10));

        await Assert.ThrowsAsync<OperationCanceledException>(
            async () =>
            {
                using IApiResponse _ = await sender
                .SendAsync(
                    new HttpRequestMessage(HttpMethod.Get, Constants.LOCALHOST_TEST_URI), 
                    cancellationTokenSource.Token);
            });
    }

    #endregion

    public static IEnumerable<object[]> ConfigureHttpClients()
    {
        #region Case 1

        {
            JsonResponseTestClass obj = new()
            {
                MyProperty = 1,
                MyProperty1 = "132",
                MyProperty2 = true
            };

            yield return new object[] { Mocks.HttpClient(obj, HttpStatusCode.OK), obj };
        }

        #endregion

        #region Case 2

        {
            JsonResponseTestClass[] obj = new[]
            {
                new JsonResponseTestClass
                {
                    MyProperty = 1,
                    MyProperty1 = "132",
                    MyProperty2 = true
                },

                new JsonResponseTestClass
                {
                    MyProperty = 2,
                    MyProperty1 = "abc",
                    MyProperty2 = false
                },

                new JsonResponseTestClass
                {
                    MyProperty = 3,
                    MyProperty1 = "test",
                    MyProperty2 = true
                },
            };

            yield return new object[] { Mocks.HttpClient(obj, HttpStatusCode.OK), obj };
        };

        #endregion
    }

    public static IEnumerable<object[]> GetHttpClientWithDifferentStatusCode()
    {
        foreach(HttpStatusCode httpStatusCode in Enum.GetValues<HttpStatusCode>())
        {
            yield return new object[] { Mocks.HttpClient(new JsonResponseTestClass(), httpStatusCode), (int)httpStatusCode };
        }
    }
}
