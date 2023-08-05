using System.Reflection;
using System.Text;
using LoafThePenguin.Helpers;
using LoafThePenguin.MOEXSharp.ApiRequest.Abstracts;
using LoafThePenguin.MOEXSharp.ApiRequest.Internal;

namespace LoafThePenguin.ApiRequest.Tests;

public sealed class ApiResponseTests
{
    private const int TIMEOUT = 1000;

    #region Try to get properties values when object disposed.

    [Fact(
        Timeout = TIMEOUT, 
        DisplayName = $"Если объект высвобожден, то при попытке получить {nameof(ApiResponse.StatusCode)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Gets_StatusCode_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.StatusCode);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден, то при попытке получить {nameof(ApiResponse.IsSuccess)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Gets_IsSuccess_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.IsSuccess);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден, то при попытке получить {nameof(ApiResponse.ResponseStream)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Gets_ResponseStream_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.ResponseStream);
    }

    #endregion

    #region Try to set properties values when object disposed.

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден, то при попытке записать значение в {nameof(ApiResponse.StatusCode)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Sets_StatusCode_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.StatusCode = 200);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден, то при попытке записать значение в {nameof(ApiResponse.IsSuccess)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Sets_IsSuccess_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.IsSuccess = false);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден, то при попытке записать значение в {nameof(ApiResponse.ResponseStream)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public void ApiResponse_Disposed_Sets_ResponseStream_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        apiResponse.Dispose();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.ResponseStream = Stream.Null);
    }

    #endregion

    #region Try to get properties values when object disposed asynchronously.

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке получить {nameof(ApiResponse.StatusCode)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Gets_StatusCode_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.StatusCode);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке получить {nameof(ApiResponse.IsSuccess)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Gets_IsSuccess_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.IsSuccess);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке получить {nameof(ApiResponse.ResponseStream)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Gets_ResponseStream_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => _ = apiResponse.ResponseStream);
    }

    #endregion

    #region Try to set properties values when object disposed asynchronously.

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке записать значение в {nameof(ApiResponse.StatusCode)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Sets_StatusCode_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.StatusCode = 200);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке записать значение в {nameof(ApiResponse.IsSuccess)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Sets_IsSuccess_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.IsSuccess = false);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если объект высвобожден асинхронно, то при попытке записать значение в {nameof(ApiResponse.ResponseStream)}, " +
                      $"должен произойти выброс {nameof(ObjectDisposedException)}")]
    public async Task ApiResponse_Disposed_Async_Sets_ResponseStream_Throw_ObjectDisposedException()
    {
        ApiResponse apiResponse = GetDefaultApiResponseObject();
        await apiResponse.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => apiResponse.ResponseStream = Stream.Null);
    }

    #endregion

    #region Try to set null for ResponseStream

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiResponse.ResponseStream)} попытаться записать null при инициализации объекта, " +
                      $"то должен произойти выброс {nameof(ArgumentNullException)}")]
    public async Task ApiResponse_Set_Null_For_ResponseStream_With_Init_Properties_Through_Ctor_Throws_ArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await using ApiResponse _ = new()
            {
                IsSuccess = false,
                ResponseStream = null,
                StatusCode = 200
            };
        });
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiResponse.ResponseStream)} попытаться записать null после инициализации объекта, " +
                      $"то должен произойти выброс {nameof(ArgumentNullException)}")]
    public async Task ApiResponse_Set_Null_For_ResponseStream_After_Ctor_Throws_ArgumentNullException()
    {
        await using ApiResponse response = GetDefaultApiResponseObject();

        Assert.Throws<ArgumentNullException>(() => response.ResponseStream = null);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiResponse.ResponseStream)} попытаться записать null через интерфейс с помощью рефлексии, " +
                      $"то должен произойти выброс {nameof(TargetInvocationException)}, с внутренним исключением {nameof(ArgumentNullException)}")]
    public async Task ApiResponse_Set_Null_For_ResponseStream_Through_Interface_With_Reflection_Throws_TargetInvocationException_With_Inner_ArgumentNullException()
    {
        await using IApiResponse response = GetDefaultApiResponseObject();
        PropertyInfo property = response
            .GetType()
            .GetProperty(nameof(IApiResponse.ResponseStream));

        Assert.Throws<TargetInvocationException>(() =>
        {
            try
            {
                property.SetValue(response, null);
            }
            catch (Exception ex)
            {
                Assert.Equal(ex.InnerException.GetType().FullName, typeof(ArgumentNullException).FullName);

                throw;
            }
        });
    }

    #endregion

    #region Get valid values of ApiResponse

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Получение ожидаемого значения {nameof(ApiResponse.StatusCode)}")]
    [MemberData(nameof(GetValidValuesForApiResponse))]
    public async Task ApiResponse_Gets_StatusCode_Value(bool isSuccess, int statusCode, Stream stream)
    {
        await using ApiResponse apiResponse = GetResponse(isSuccess, statusCode, stream);

        Assert.Equal(statusCode, apiResponse.StatusCode);
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Получение ожидаемого значения {nameof(ApiResponse.IsSuccess)}")]
    [MemberData(nameof(GetValidValuesForApiResponse))]
    public async Task ApiResponse_Gets_IsSuccess_Value(bool isSuccess, int statusCode, Stream stream)
    {
        await using ApiResponse apiResponse = GetResponse(isSuccess, statusCode, stream);

        Assert.Equal(isSuccess, apiResponse.IsSuccess);
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Получение ожидаемого значения {nameof(ApiResponse.ResponseStream)}")]
    [MemberData(nameof(GetValidValuesForApiResponse))]
    public async Task ApiResponse_Gets_ResponseStream_Value(bool isSuccess, int statusCode, Stream stream)
    {
        await using ApiResponse apiResponse = GetResponse(isSuccess, statusCode, stream);

        byte[] expectedBuffer = await StreamHelper.GetStreamBufferAsync(stream);
        byte[] actualBuffer = await StreamHelper.GetStreamBufferAsync(apiResponse.ResponseStream);


        Assert.Equal(isSuccess, apiResponse.IsSuccess);
    }

    #endregion

    private static ApiResponse GetDefaultApiResponseObject()
    {
        return new ApiResponse
        {
            IsSuccess = true,
            StatusCode = 200,
            ResponseStream = Stream.Null
        };
    }

    private static Stream GetStream(string strContent)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(strContent);

        return new MemoryStream(buffer);
    }

    private static ApiResponse GetResponse(bool isSuccess, int statusCode, Stream stream)
    {
        MemoryStream responseStream = new();
        stream.CopyTo(responseStream);

        return new ApiResponse
        {
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            ResponseStream = responseStream
        };
    }

    public static IEnumerable<object[]> GetValidValuesForApiResponse()
    {
        byte[] case1Buffer = Encoding.UTF8.GetBytes("Hello");
        yield return new object[]
        {
            true,
            200,
            new MemoryStream(case1Buffer)
        };

        yield return new object[]
        {
            false,
            500,
            Stream.Null
        };
    }
}
