using System.Text.Json;
using LoafThePenguin.ApiRequest.Abstracts;
using LoafThePenguin.Helpers;

namespace LoafThePenguin.ApiRequest;

/// <summary>
/// Читает ответ на запрос к Api, полученный в формате Json.
/// </summary>
public sealed class ApiResponseReaderFromJson : IApiResponseReader
{
    /// <summary>
    /// Создаёт экземпляр, читающий ответ на запрос, полученный в формате Json.
    /// </summary>
    public ApiResponseReaderFromJson()
    {
        
    }

    /// <summary>
    /// Читает ответ на запрос к Api, полученный в формате Json.
    /// </summary>
    /// <typeparam name="T">Тип полученного в ответе на запрос объекта.</typeparam>
    /// <param name="response">Экземпляр ответа к Api.</param>
    /// <returns>Экземпляр объекта, полученного от Api.</returns>
    public async Task<T?> ReadAsync<T>(IApiResponse response)
    {
        _ = ThrowHelper.ThrowIfArgumentNull(response);

        T? result = await JsonSerializer
            .DeserializeAsync<T>(response.ResponseStream)
            .ConfigureAwait(continueOnCapturedContext: false);
        await response
            .DisposeAsync()
            .ConfigureAwait(continueOnCapturedContext: false);

        return result;
    }
}
