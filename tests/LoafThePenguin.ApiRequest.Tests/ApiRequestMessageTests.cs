using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.ApiRequest.Internal;
using LoafThePenguin.ApiRequest.Tests.Foos;
using Newtonsoft.Json.Linq;

namespace LoafThePenguin.ApiRequest.Tests;

public sealed class ApiRequestMessageTests
{
    private const int TIMEOUT = 1000;

    #region Tests when ApiRequestMessage is disposed.

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс исключения {nameof(ObjectDisposedException)} " +
                      $"при попытке прочитать значение {nameof(ApiRequestMessage.Headers)}, " +
                      $"после вызова {nameof(ApiRequestMessage.Dispose)}")]
    public void ApiRequestMessage_Try_To_Gets_Headers_When_Instance_Is_Disposed()
    {
        ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, System.Net.HttpStatusCode.OK);
        ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);
        apiRequestMessage.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiRequestMessage.Headers);
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс исключения {nameof(ObjectDisposedException)} " +
                      $"при попытке вызвать метод {nameof(ApiRequestMessage.ConfigureHeaders)}, " +
                      $"после вызова {nameof(ApiRequestMessage.Dispose)}")]
    public void ApiRequestMessage_Try_To_Call_ConfigureHeaders_With_One_Value_When_Instance_Is_Disposed()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);
        apiRequestMessage.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiRequestMessage.ConfigureHeaders("1", "1"));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Выброс исключения {nameof(ObjectDisposedException)} " +
                      $"при попытке вызвать метод {nameof(ApiRequestMessage.ConfigureHeaders)}, " +
                      $"после вызова {nameof(ApiRequestMessage.Dispose)}")]
    public void ApiRequestMessage_Try_To_Call_ConfigureHeaders_With_Many_Values_When_Instance_Is_Disposed()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);
        apiRequestMessage.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = apiRequestMessage.ConfigureHeaders("1", new[] { "1", "2" }));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Вызывать {nameof(ApiRequestMessage.Dispose)} можно два раза.")]
    public void ApiRequestMessage_Can_Call_Dispose_Twice()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);
        apiRequestMessage.Dispose();

        try
        {
            apiRequestMessage.Dispose();
        }
        catch(Exception ex)
        {
            Assert.Fail($"Было выброшено исключение {ex.GetType().FullName}: {ex.Message}");
        }
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Вызывать {nameof(ApiRequestMessage.Dispose)} можно несколько раз.")]
    public void ApiRequestMessage_Can_Call_Dispose_Many_Times()
    {
        ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, System.Net.HttpStatusCode.OK);
        ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);
        apiRequestMessage.Dispose();

        try
        {
            apiRequestMessage.Dispose();
            apiRequestMessage.Dispose();
            apiRequestMessage.Dispose();
            apiRequestMessage.Dispose();
            apiRequestMessage.Dispose();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Было выброшено исключение {ex.GetType().FullName}: {ex.Message}");
        }
    }

    #endregion

    #region Tests null arguments

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать null в качестве параметра name, " +
                      $"произойдёт выброс {nameof(ArgumentNullException)} ")]
    public void ApiRequestMessage_ConfigureHeaders_One_Value_Throws_ArgumentNullException_When_Name_Is_Null()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        using ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);

        Assert.Throws<ArgumentNullException>(() => apiRequestMessage.ConfigureHeaders(name: null, value: "1"));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать null в качестве параметра value, " +
                      $"произойдёт выброс {nameof(ArgumentNullException)} ")]
    public void ApiRequestMessage_ConfigureHeaders_One_Value_Throws_ArgumentNullException_When_Value_Is_Null()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, System.Net.HttpStatusCode.OK);
        using ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);

        Assert.Throws<ArgumentNullException>(() => apiRequestMessage.ConfigureHeaders(name: "header", value: null));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать null в качестве параметра name," +
                      $"произойдёт выброс {nameof(ArgumentNullException)} ")]
    public void ApiRequestMessage_ConfigureHeaders_Many_Values_Throws_ArgumentNullException_When_Name_Is_Null()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, System.Net.HttpStatusCode.OK);
        using ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);

        Assert.Throws<ArgumentNullException>(() => apiRequestMessage.ConfigureHeaders(name: null, values: new[] { "1" }));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать null в качестве параметра value, " +
                      $"произойдёт выброс {nameof(ArgumentNullException)} ")]
    public void ApiRequestMessage_ConfigureHeaders_Many_Values_Throws_ArgumentNullException_When_Values_Is_Null()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        using ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);

        Assert.Throws<ArgumentNullException>(() => apiRequestMessage.ConfigureHeaders(name: "header", values: null));
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать null в качестве одного из элементов параметра values, " +
                      $"произойдёт выброс {nameof(NullReferenceException)} ")]
    [InlineData(null, "1", "2")]
    [InlineData("0", null, "1")]
    [InlineData("0", "1", null)]
    [InlineData(null, null, "2")]
    [InlineData(null, "1", null)]
    [InlineData("0", null, null)]
    [InlineData(null, null, null)]
    public void ApiRequestMessage_ConfigureHeaders_Many_Values_Throws_ArgumentNullException_When_Any_Element_Os_Values_Is_Null(params string[] elements)
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        using ApiRequestMessage apiRequestMessage = new(sender, "1", HttpMethod.Get);

        Assert.Throws<NullReferenceException>(() => apiRequestMessage.ConfigureHeaders(name: "header", values: elements));
    }

    #endregion

    #region Tests empty strings as arguments

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Если в конструктор {nameof(ApiRequestMessage)} " +
                      $"передать пустую строку в качестве uri, " +
                      $"произойдёт выброс {nameof(InvalidOperationException)}")]
    public void ApiRequestMessage_Ctor_With_Empty_Uri()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);

        Assert.Throws<InvalidOperationException>(() =>
        {
            using ApiRequestMessage _ = new(sender, string.Empty, HttpMethod.Get);
        });
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать пустую строку в качестве name, " +
                      $"произойдёт выброс {nameof(InvalidOperationException)}")]
    [InlineData("", "1")]
    [InlineData("   ", "1")]
    public void ApiRequestMessage_ConfigureHeaders_With_Single_Value_With_Empty_Name(string name, string value)
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        Assert.Throws<InvalidOperationException>(() =>
        {
            message.ConfigureHeaders(name, value);
        });
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Если в {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"передать пустую строку в качестве name, " +
                      $"произойдёт выброс {nameof(InvalidOperationException)}")]
    [InlineData("", "1")]
    [InlineData("", "1", "2")]
    [InlineData("   ", "1", "2", "3")]
    public void ApiRequestMessage_ConfigureHeaders_With_Many_Values_With_Empty_Name(string name, params string[] values)
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(string.Empty, HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        Assert.Throws<InvalidOperationException>(() =>
        {
            message.ConfigureHeaders(name, values);
        });
    }

    #endregion

    #region Tests SendAsync

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.ResponseStream)}")]
    [MemberData(nameof(ConfigureHttpClients))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_Correct_Value<T>(HttpClient httpClient, T expectedObject)
    {
        using ApiRequestSender sender = new(httpClient);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);

        using IApiResponse response = await message
            .SendAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        T? actual = await JsonSerializer.DeserializeAsync<T>(response.ResponseStream);

        Assert.NotNull(actual);
        Assert.Equal(expectedObject, actual, new JsonResponseTestClass.JsonResponseTestClassComparer<T>());
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.StatusCode)}")]
    [MemberData(nameof(GetHttpClientWithDifferentStatusCode))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_ResponseCode_Value<T>(HttpClient httpClient, int expected)
    {
        using ApiRequestSender sender = new(httpClient);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);

        using IApiResponse response = await message
            .SendAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        Assert.Equal(expected, response.StatusCode);
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.SendAsync)} " +
                      $"возвращает корректное значение {nameof(IApiResponse.IsSuccess)}")]
    [MemberData(nameof(GetHttpClientWithDifferentStatusCode))]
    public async Task ApiRequestSender_SendAsync_ResponseStream_IsSuccess_Value<T>(HttpClient httpClient, int expectedStatusCode)
    {
        using ApiRequestSender sender = new(httpClient);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);

        using IApiResponse response = await message
            .SendAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        bool expected = expectedStatusCode >= 200 && expectedStatusCode <= 299;

        Assert.Equal(expected, response.IsSuccess);
    }

    #endregion

    #region Tests ConfigureHeaders

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"корректно добавляет заголовок")]
    [InlineData("X-FRAME-OPTIONS", "DENY")]
    public void ApiRequestMessage_ConfigureHeaders_Single_Value_Add_Header(string name, string value)
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(new JsonResponseTestClass(), HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        _ = message.ConfigureHeaders(name, value);

        Assert.Contains(
            message.Headers, 
            header => 
                header.Key.ToUpper() == name.ToUpper() 
                && header.Value.Select(value => value.ToUpper()).Contains(value));
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"корректно добавляет заголовок")]
    [InlineData("Header", "header_value_1", "header_value_2", "header_value_3")]
    public void ApiRequestMessage_ConfigureHeaders_Many_Values_Add_Header(string name, params string[] values)
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(new JsonResponseTestClass(), HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        _ = message.ConfigureHeaders(name, values);

        Assert.True(message.Headers.All(
            header => 
                header.Key.ToUpper() == name.ToUpper() 
                && header.Value.Select(value => value.ToUpper()).SequenceEqual(values.Select(val => val.ToUpper()))));
    }

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Метод {nameof(ApiRequestMessage.ConfigureHeaders)} " +
                      $"возвращает тот же экземпляр")]
    public void ApiRequestMessage_ConfigureHeaders_Returns_Same_Object()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(new JsonResponseTestClass(), HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        using IApiRequestMessage configuredMessage = message.ConfigureHeaders("X-FRAME-OPTIONS", "DENY");

        Assert.Same(message, configuredMessage);
    }

    #endregion

    #region Tests become disposed afeter ApiRequestSender disposing

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"Экземпляр {nameof(ApiRequestMessage)} высвобождается, " +
                      $"если внедрённый у экземпляра {nameof(ApiRequestSender)} " +
                      $"был вызван {nameof(ApiRequestSender.Dispose)}")]
    public void ApiRequestMessage_Disposing_After_ApiRequestSender_Dispose()
    {
        using ApiRequestSender sender = Mocks.ApiRequestSender(new JsonResponseTestClass(), HttpStatusCode.OK);
        using ApiRequestMessage message = new(sender, Constants.LOCALHOST_TEST_URI, HttpMethod.Get);
        sender.Dispose();

        Assert.Throws<ObjectDisposedException>(() => message.Headers);
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
        foreach (HttpStatusCode httpStatusCode in Enum.GetValues<HttpStatusCode>())
        {
            yield return new object[] { Mocks.HttpClient(new JsonResponseTestClass(), httpStatusCode), (int)httpStatusCode };
        }
    }
}
