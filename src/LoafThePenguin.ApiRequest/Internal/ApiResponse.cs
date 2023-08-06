using System.Net;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.Helpers;

namespace LoafThePenguin.ApiRequest.Internal;

internal sealed class ApiResponse : IApiResponse
{
    private const string INVALID_STATUS_CODE = "Неверный статус код ответа {0}";

    private bool _disposed;
    private int _statusCode;
    private bool _isSuccess;
    private Stream _responseStream;

    public ApiResponse()
    {
        _responseStream = Stream.Null;
    }

    public required int StatusCode
    {
        get
        {
            CheckDisposed();

            return _statusCode;
        }

        set
        {
            CheckDisposed();

            if (!EnumHelper.HasElement<HttpStatusCode>(value))
            {
                ThrowHelper.Throw<InvalidOperationException>(string.Format(INVALID_STATUS_CODE, value));
            }

            _statusCode = value;
        }
    }
    public required bool IsSuccess
    {
        get
        {
            CheckDisposed();

            return _isSuccess;
        }

        set
        {
            CheckDisposed();

            _isSuccess = value;
        }
    }
    public required Stream ResponseStream
    {
        get
        {
            CheckDisposed();

            return _responseStream;
        }

        set
        {
            CheckDisposed();

            _ = ThrowHelper.ThrowIfArgumentNull(value);

            _responseStream = value;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _responseStream.Dispose();
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        await _responseStream.DisposeAsync();
        _disposed = true;
    }

    private void CheckDisposed()
    {
        if (_disposed)
        {
            ThrowHelper.ThrowDisposed(this);
        }
    }
}