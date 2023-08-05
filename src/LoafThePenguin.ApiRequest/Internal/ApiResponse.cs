using LoafThePenguin.Helpers;
using LoafThePenguin.MOEXSharp.ApiRequest.Abstracts;

namespace LoafThePenguin.MOEXSharp.ApiRequest.Internal;

internal sealed class ApiResponse : IApiResponse
{
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
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

            return _statusCode;
        }

        set
        {
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

            _statusCode = value;
        }
    }
    public required bool IsSuccess
    {
        get
        {
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

            return _isSuccess;
        }

        set
        {
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

            _isSuccess = value;
        }
    }
    public required Stream ResponseStream
    {
        get
        {
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

            return _responseStream;
        }

        set
        {
            if (_disposed)
            {
                ThrowHelper.ThrowDisposed(this);
            }

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
}