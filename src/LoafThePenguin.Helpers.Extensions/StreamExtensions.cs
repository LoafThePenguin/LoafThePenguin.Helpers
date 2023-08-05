namespace LoafThePenguin.Helpers.Extensions;

/// <summary>
/// Методы расширения для <see cref="Stream"/>.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Асинхронно возвращает буфер потока <paramref name="stream"/> без потери <see cref="Stream.Position"/>.
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    /// <returns>
    /// Буфер потока <paramref name="stream"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, когда <paramref name="stream"/> является <see langword="null"/>.
    /// </exception>
    public static async Task<byte[]> GetStreamBufferAsync(this Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        return await StreamHelper
            .GetStreamBufferAsync(stream)
            .ConfigureAwait(continueOnCapturedContext: false);
    }
}
