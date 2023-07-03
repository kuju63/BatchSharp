using System.Buffers;
using System.Text;

using BatchSharp.Tests.Mock;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests.Writer;

/// <summary>
/// Test class for <see cref="FlatFileWriter{T}"/>.
/// </summary>
public class FlatFileWriterTest : IDisposable
{
    private readonly Mock<IFileWriterSetting> _setting = new();
    private readonly MemoryStream _memoryStream = new();
    private readonly StreamWriter _streamWriter;
    private readonly IMemoryOwner<byte> _buffer = MemoryPool<byte>.Shared.Rent();

    /// <summary>
    /// Initializes a new instance of the <see cref="FlatFileWriterTest"/> class.
    /// </summary>
    public FlatFileWriterTest()
    {
        _streamWriter = new StreamWriter(_memoryStream);
        _setting.Setup(p => p.GetWriter()).Returns(_streamWriter);
    }

    /// <summary>
    /// Test for writing string content.
    /// </summary>
    /// <returns>Asynchronous result.</returns>
    [Fact]
    public async Task ShouldWriteStringContentAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<string>>>();
        using var writer = new FlatFileWriter<string>(logger.Object, _setting.Object);
        await writer.WriteAsync("test");

        _memoryStream.Seek(0, SeekOrigin.Begin);
        var length = await _memoryStream.ReadAsync(_buffer.Memory);
        var content = Encoding.UTF8.GetString(_buffer.Memory.Span[..length]);

        content.Should().Be($"test{Environment.NewLine}");

        _setting.Verify(p => p.GetWriter(), Times.Once);
    }

    /// <summary>
    /// Test for writing object.
    /// </summary>
    /// <returns>Asynchronous result.</returns>
    [Fact]
    public async Task ShouldWriteToStringContentAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<SampleWriterContent>>>();
        using var writer = new FlatFileWriter<SampleWriterContent>(logger.Object, _setting.Object);
        await writer.WriteAsync(new SampleWriterContent());

        _memoryStream.Seek(0, SeekOrigin.Begin);
        var length = await _memoryStream.ReadAsync(_buffer.Memory);
        var content = Encoding.UTF8.GetString(_buffer.Memory.Span[..length]);

        content.Should().Be($"test{Environment.NewLine}");

        _setting.Verify(p => p.GetWriter(), Times.Once);
    }

    /// <summary>
    /// Test for writing string content with cancellation token.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact(DisplayName = "Should write content with cancellation token.")]
    public async Task ShouldWriteContentWithCancellationTokenAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<SampleWriterContent>>>();
        using var writer = new FlatFileWriter<SampleWriterContent>(logger.Object, _setting.Object);
        await writer.WriteAsync(new SampleWriterContent(), CancellationToken.None);

        _memoryStream.Seek(0, SeekOrigin.Begin);
        var length = await _memoryStream.ReadAsync(_buffer.Memory);
        var content = Encoding.UTF8.GetString(_buffer.Memory.Span[..length]);

        content.Should().Be($"test{Environment.NewLine}");

        _setting.Verify(p => p.GetWriter(), Times.Once);
    }

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="FlatFileWriterTest"/> class.
    /// </summary>
    public void Dispose()
    {
        _memoryStream.Dispose();
        _streamWriter.Dispose();
        _buffer.Dispose();
        GC.SuppressFinalize(this);
    }
}