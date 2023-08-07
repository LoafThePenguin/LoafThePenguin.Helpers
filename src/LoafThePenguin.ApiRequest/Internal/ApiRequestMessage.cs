using System;
using System.Net.Http.Headers;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.Helpers;

namespace LoafThePenguin.ApiRequest.Internal;

internal sealed class ApiRequestMessage : IDisposable, IApiRequestMessage
{
    private const string IOE_STRING_EMPTY_MESSAGE = "Параметр \"{0}\" не может быть пустой строкой";

    private readonly HttpRequestMessage _httpRequest;
    private readonly ApiRequestSender _apiRequestSender;

    internal HttpRequestHeaders Headers
    {
        get
        {
            DisposeCheck();

            return _httpRequest.Headers;
        }
    }

    private bool _isDisposed;

    public ApiRequestMessage(ApiRequestSender apiRequestSender, string uri, HttpMethod method)
    {

        _ = ThrowHelper.ThrowIfArgumentNull(apiRequestSender);
        _ = ThrowHelper.ThrowIfArgumentNull(uri);
        _ = ThrowHelper.ThrowIfArgumentNull(method);
        if (string.IsNullOrWhiteSpace(uri))
        {
            ThrowHelper.Throw<InvalidOperationException>(string.Format(IOE_STRING_EMPTY_MESSAGE, nameof(uri)));
        }

        _apiRequestSender = apiRequestSender;
        _apiRequestSender.Disposed += OnApiRequestSenderDisposed;
        _httpRequest = new HttpRequestMessage(method, uri);
    }

    public IApiRequestMessage ConfigureHeaders(string name, string value)
    {
        DisposeCheck();

        _ = ThrowHelper.ThrowIfArgumentNull(name);
        _ = ThrowHelper.ThrowIfArgumentNull(value);
        if (string.IsNullOrWhiteSpace(name))
        {
            ThrowHelper.Throw<InvalidOperationException>(string.Format(IOE_STRING_EMPTY_MESSAGE, nameof(name)));
        }

        Headers.Add(name, value);

        return this;
    }

    public IApiRequestMessage ConfigureHeaders(string name, IEnumerable<string> values)
    {
        DisposeCheck();

        _ = ThrowHelper.ThrowIfArgumentNull(name);
        _ = ThrowHelper.ThrowIfArgumentNull(values);
        if (string.IsNullOrWhiteSpace(name))
        {
            ThrowHelper.Throw<InvalidOperationException>(IOE_STRING_EMPTY_MESSAGE);
        }

        string[] valuesArray = values as string[] ?? values.ToArray();
        _ = ThrowHelper.ThrowIfAnyItemIsNull(valuesArray, argumentName: nameof(values));
        
        Headers.Add(name, valuesArray);

        return this;
    }

    public async Task<IApiResponse> SendAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        DisposeCheck();

        return await _apiRequestSender
            .SendAsync(_httpRequest, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
    }

    private void OnApiRequestSenderDisposed(object? sender, EventArgs e) => Dispose();

    private void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            _httpRequest.Dispose();
        }

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
