namespace LoafThePenguin.ApiRequest.Abstracts;

/// <summary>
/// Представляет тип, способный читать ответ от Api.
/// </summary>
public interface IApiResponseReader
{
    /// <summary>
    /// Асинхронно читает ответ от Api и возвращает объект с данными.
    /// </summary>
    /// <typeparam name="TResponse">
    /// Тип экземпляра, который представляет данные ответа.
    /// </typeparam>
    /// <param name="response">
    /// Экземпляр типа ответа от Api.
    /// </param>
    /// <returns>
    /// Результат чтения ответа в представлении типа <typeparamref name="TResponse"/>.
    /// </returns>
    Task<TResponse?> ReadAsync<TResponse>(IApiResponse response);
}
