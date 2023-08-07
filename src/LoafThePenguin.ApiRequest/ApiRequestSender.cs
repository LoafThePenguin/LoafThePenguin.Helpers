using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.ApiRequest.Internal;
using LoafThePenguin.Helpers;

namespace LoafThePenguin.ApiRequest;

/// <summary>
/// Отправляет запрос к Api.
/// </summary>
/// <exception cref="ObjectDisposedException" />
public sealed class ApiRequestSender : IDisposable
{
    private readonly HttpClient _httpClient;

    private bool _isDisposed;

    internal event EventHandler<EventArgs>? Disposed;

    /// <summary>
    /// Создаёт экземпляр для отправки запросов к Api.
    /// </summary>
    public ApiRequestSender() : this(new HttpClient())
    {
    }

    internal ApiRequestSender(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Конфигурирует запрос, добавляя к нему адрес отправки и метод отправки <see cref="HttpMethod"/>.
    /// </summary>
    /// <param name="uri">Адрес, по которому отправится запрос.</param>
    /// <param name="method">Метод отправки запроса.</param>
    /// <returns>
    /// Экземпляр запроса к Api.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// Выбрасывается, при попытки вызова у высвобожденного объекта.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается если <paramref name="uri"/> или <paramref name="method"/>
    /// являются <see langword="null"/>.
    /// </exception>
    public IApiRequestMessage ConfigureRequest(string uri, HttpMethod method)
    {
        const string IOE_STRING_EMPTY_MESSAGE = $"Параметр \"{nameof(uri)}\" не может быть пустой строкой";
        DisposeCheck();

        _ = ThrowHelper.ThrowIfArgumentNull(uri);
        _ = ThrowHelper.ThrowIfArgumentNull(method);
        if (string.IsNullOrWhiteSpace(uri))
        {
            ThrowHelper.Throw<InvalidOperationException>(IOE_STRING_EMPTY_MESSAGE);
        }

        return new ApiRequestMessage(this, uri, method);
    }

    internal async Task<IApiResponse> SendAsync(
        HttpRequestMessage httpRequestMessage, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        DisposeCheck();

        _ = ThrowHelper.ThrowIfArgumentNull(httpRequestMessage);

        using HttpResponseMessage responseMessage = await _httpClient
            .SendAsync(httpRequestMessage, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        byte[] responseBuffer = await responseMessage
            .Content
            .ReadAsByteArrayAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
        MemoryStream responseStream = new(responseBuffer);

        return new ApiResponse
        {
            ResponseStream = responseStream,
            IsSuccess = responseMessage.IsSuccessStatusCode,
            StatusCode = (int)responseMessage.StatusCode
        };
    }

    /// <summary>
    /// Высвобождает текущий объект.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _httpClient.Dispose();
        Disposed?.Invoke(this, EventArgs.Empty);

        _isDisposed = true;
    }

    private void DisposeCheck()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ApiRequestMessage));
        }
    }
}
