namespace LoafThePenguin.ApiRequest.Abstracts;

/// <summary>
/// Представляет тип запроса к Api.
/// </summary>
public interface IApiRequestMessage : IDisposable
{
    /// <summary>
    /// Конфигурирует заголовки запроса.
    /// </summary>
    /// <param name="name">Название заголовка.</param>
    /// <param name="values">Перечисление значений заголовка.</param>
    /// <returns>Экземпляр запроса к Api.</returns>
    IApiRequestMessage ConfigureHeaders(string name, IEnumerable<string> values);

    /// <summary>
    /// Конфигурирует заголовки запроса.
    /// </summary>
    /// <param name="name">Название заголовка.</param>
    /// <param name="value">Значение заголовка.</param>
    /// <returns>Экземпляр запроса к Api.</returns>
    IApiRequestMessage ConfigureHeaders(string name, string value);

    /// <summary>
    /// Асинхронно отправляет запрос к Api.
    /// </summary>
    /// <returns>Экземпляр ответа на запрос от Api.</returns>
    Task<IApiResponse> SendAsync();
}