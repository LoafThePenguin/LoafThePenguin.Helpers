namespace LoafThePenguin.Helpers;

/// <summary>
/// Содержит методы работы с потоками <see cref="Stream"/>.
/// </summary>
public static class StreamHelper
{
    /// <summary>
    /// Асинхронно возвращает буфер потока без потери <see cref="Stream.Position"/>.
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
    public static async Task<byte[]> GetStreamBufferAsync(Stream stream)
    {
        ThrowHelper.ThrowIfArgumentNull(stream);

        long position = stream.Position;
        stream.Position = 0;
        await using MemoryStream memoryStream = new();
        memoryStream.ConfigureAwait(continueOnCapturedContext: false);
        await stream
            .CopyToAsync(memoryStream)
            .ConfigureAwait(continueOnCapturedContext: false);
        stream.Position = position;

        return memoryStream.ToArray();
    }
}
