using Moq.Protected;
using Moq;
using System.Net;
using System.Text.Json;

namespace LoafThePenguin.ApiRequest.Tests;

public static class Mocks
{
    public static HttpClient HttpClient(object responseObj, HttpStatusCode httpStatusCode)
    {
        Mock<HttpMessageHandler> handlerMock = new();

        HttpResponseMessage response = new()
        {
            StatusCode = httpStatusCode,
            Content = new StringContent(JsonSerializer.Serialize(responseObj))
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return new HttpClient(handlerMock.Object);
    }

    public static ApiRequestSender ApiRequestSender(object responseObj, HttpStatusCode httpStatusCode)
    {
        return new ApiRequestSender(HttpClient(responseObj, httpStatusCode));
    }
}
