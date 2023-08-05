using System.Text;

namespace LoafThePenguin.Helpers.Extensions.Tests;

public sealed class StreamExtensionsTests
{
    private const int TIMEOUT = 1000;

    #region GetStreamBufferAsync pure tests

    [Fact(
        Timeout = TIMEOUT,
        DisplayName = $"При вызове {nameof(StreamExtensions.GetStreamBufferAsync)}, " +
                      $"если в качестве параметра передать null, " +
                      $"то произойдёт выброс {nameof(ArgumentNullException)}")]
    public void GetStreamBufferAsync_Throws_ArgumentNullException_Pure()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => _ = await ((Stream)null).GetStreamBufferAsync());
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Вызов {nameof(StreamExtensions.GetStreamBufferAsync)}, " +
                      $"корректно возвращает буфер")]
    [MemberData(nameof(GetStreams))]
    public async Task GetStreamBufferAsync_Works_Well_Pure(Stream stream, int _)
    {
        MemoryStream streamCopy = new();
        stream.CopyTo(streamCopy);
        stream.Position = 0;

        byte[] expected = streamCopy.ToArray();
        byte[] actual = await stream.GetStreamBufferAsync();

        try
        {
            Assert.Equal(expected, actual);
        }
        finally
        {
            await streamCopy.DisposeAsync();
            await stream.DisposeAsync();
        }
    }

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Вызов {nameof(StreamExtensions.GetStreamBufferAsync)}, " +
                      $"не меняет позицию потока Stream")]
    [MemberData(nameof(GetStreamsWithPosition))]
    public async Task GetStreamBufferAsync_Position_Do_Not_Changes_When_Buffer_Gets_Pure(Stream stream, int position)
    {
        stream.Position = position;

        long expected = position;
        _ = await stream.GetStreamBufferAsync();

        try
        {
            Assert.Equal(expected, stream.Position);
        }
        finally
        {
            await stream.DisposeAsync();
        }
    }

    #endregion

    #region GetStreamBufferAsync pure through StreamHelper

    [Theory(
        Timeout = TIMEOUT,
        DisplayName = $"Проверяется через {nameof(StreamHelper)}. Вызов {nameof(StreamExtensions.GetStreamBufferAsync)}, " +
                      $"корректно возвращает буфер.")]
    [MemberData(nameof(GetStreams))]
    public async Task GetStreamBufferAsync_Works_Well_Through_StreamHelper(Stream stream, int _)
    {
        byte[] expected = await StreamHelper.GetStreamBufferAsync(stream);
        byte[] actual = await stream.GetStreamBufferAsync();

        try
        {
            Assert.Equal(expected, actual);
        }
        finally
        {
            await stream.DisposeAsync();
        }
    }

    #endregion

    public static IEnumerable<object[]> GetStreamsWithPosition()
    {
        yield return new object[] { Stream.Null, 0 };

        byte[] caseBuffer = Encoding.UTF8.GetBytes("Hello world");

        yield return new object[] { new MemoryStream(caseBuffer), 0 };

        yield return new object[] { new MemoryStream(caseBuffer), 3 };

        yield return new object[] { new MemoryStream(caseBuffer), caseBuffer.Length - 1 };
    }

    public static IEnumerable<object[]> GetStreams()
    {
        yield return new object[] { Stream.Null, 1 };

        byte[] case2Buffer = Encoding.UTF8.GetBytes("Hello world");
        yield return new object[] { new MemoryStream(case2Buffer), 2 };

        string path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}test_file_case";
        string casePathFormat = "{0}_{1}";

        string case3Path = string.Format(casePathFormat, path, 3);
        FileStream case3Stream = File.Create(case3Path);
        yield return new object[] { case3Stream, 3 };

        string case4Path = string.Format(casePathFormat, path, 4);
        using (File.Create(case4Path))
        {

        }
        FileStream case4Stream = new(case4Path, FileMode.Open);
        yield return new object[] { case4Stream, 4 };

        string case5Path = string.Format(casePathFormat, path, 5);
        using (File.Create(case5Path))
        {

        }

        FileStream dummy5Stream = File.OpenWrite(case5Path);
        using (StreamWriter streamWriter = new(dummy5Stream))
        {
            streamWriter.Write("Безобидный файлик");
        }

        FileStream case5Stream = new(case5Path, FileMode.Open);
        yield return new object[] { case5Stream, 5 };
    }
}
