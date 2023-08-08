using System.Net;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.ApiRequest.Tests.Foos;

namespace LoafThePenguin.ApiRequest.Tests;

public sealed class ApiResponseReaderFromJsonTests
{
    [Fact(
        Timeout = Constants.TIMEOUT_1_SEC,
        DisplayName = $"Выбрасывает {nameof(ArgumentNullException)}, " +
                      $"если в {nameof(ApiResponseReaderFromJson.ReadAsync)} " +
                      $"передать в качестве параметра - null")]
    public async Task ApiResponseReaderFromJson_ReadAsync_Throws_ANE_When_Parameter_Is_Null()
    {
        ApiResponseReaderFromJson reader = new();

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await reader.ReadAsync<string>(null));
    }

    [Fact(
        Timeout = Constants.TIMEOUT_1_SEC,
        DisplayName = $"{nameof(ApiResponseReaderFromJson.ReadAsync)}" +
                      $" высвобождает {nameof(IApiResponse)} после чтения")]
    public async Task ApiResponseReaderFromJson_ReadAsync_Disposes_Response_After_Reading()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(new JsonResponseTestClass(), HttpStatusCode.OK);
        using IApiRequestMessage request = sender.ConfigureRequest(Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        using IApiResponse response = await request
            .SendAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        ApiResponseReaderFromJson reader = new();
        _ = await reader
            .ReadAsync<JsonResponseTestClass>(response)
            .ConfigureAwait(continueOnCapturedContext: false);

        Assert.Throws<ObjectDisposedException>(() => response.IsSuccess);
    }

    [Theory(
        Timeout = Constants.TIMEOUT_1_SEC,
        DisplayName = $"{nameof(ApiResponseReaderFromJson.ReadAsync)} " +
                      $"работает корректно")]
    [MemberData(nameof(ReadAsyncReadsCorrectCases))]
    public async Task ApiResponseReaderFromJson_ReadAsync_Reads_Correct<T>(T expected, IEqualityComparer<T>? equalityComparer = null)
        where T : class, new()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(expected, HttpStatusCode.OK);
        using IApiRequestMessage request = sender.ConfigureRequest(Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        using IApiResponse response = await request
            .SendAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        ApiResponseReaderFromJson reader = new();
        T? actual = await reader
            .ReadAsync<T>(response)
            .ConfigureAwait(continueOnCapturedContext: false);
        if(actual is null)
        {
            Assert.Fail($"{nameof(actual)} оказался null");
        }

        Assert.Equal(expected, actual, equalityComparer ?? EqualityComparer<T>.Default);
    }

    public static IEnumerable<object[]> ReadAsyncReadsCorrectCases()
    {
        yield return new object[]
        {
            new JsonResponseTestClass
            {
                MyProperty = 1,
                MyProperty1 = "1",
                MyProperty2 = true
            },
            new JsonResponseTestClass.JsonResponseTestClassComparer<JsonResponseTestClass>()
        };
    }
}
