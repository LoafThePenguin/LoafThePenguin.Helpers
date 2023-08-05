namespace LoafThePenguin.MOEXSharp.ApiRequest.Abstracts;

/// <summary>
/// Представляет тип ответа от Api.
/// </summary>
public interface IApiResponse: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Возвращает статус-код ответа.
    /// </summary>
    int StatusCode { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если запрос был отправлен успешно и ответ был получен. 
    /// Иначе - возвращает <see langword="false"/>.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Возвращает поток с ответом на запрос.
    /// </summary>
    Stream ResponseStream { get; }
}
