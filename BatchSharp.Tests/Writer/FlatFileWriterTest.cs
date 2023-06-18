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
public class FlatFileWriterTest
{
    /// <summary>
    /// Test for writing string content.
    /// </summary>
    /// <returns>Asynchronous result.</returns>
    [Fact]
    public async Task ShouldWriteStringContentAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<string>>>();
        var setting = new Mock<IFileWriterSetting>();
        using var mem = new MemoryStream();
        await using var baseWriter = new StreamWriter(mem);
        setting.Setup(p => p.GetWriter()).Returns(baseWriter);
        using var writer = new FlatFileWriter<string>(logger.Object, setting.Object);
        await writer.WriteAsync("test");

        mem.Seek(0, SeekOrigin.Begin);
        using var buffer = MemoryPool<byte>.Shared.Rent();
        var length = await mem.ReadAsync(buffer.Memory);
        var content = Encoding.UTF8.GetString(buffer.Memory.Span[..length]);
        Assert.Equal($"test{Environment.NewLine}", content);
    }

    /// <summary>
    /// Test for writing object.
    /// </summary>
    /// <returns>Asynchronous result.</returns>
    [Fact]
    public async Task ShouldWriteToStringContentAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<SampleWriterContent>>>();
        var setting = new Mock<IFileWriterSetting>();
        using var mem = new MemoryStream();
        await using var baseWriter = new StreamWriter(mem);
        setting.Setup(p => p.GetWriter()).Returns(baseWriter);
        using var writer = new FlatFileWriter<SampleWriterContent>(logger.Object, setting.Object);
        await writer.WriteAsync(new SampleWriterContent());

        mem.Seek(0, SeekOrigin.Begin);
        using var buffer = MemoryPool<byte>.Shared.Rent();
        var length = await mem.ReadAsync(buffer.Memory);
        var content = Encoding.UTF8.GetString(buffer.Memory.Span[..length]);
        Assert.Equal($"test{Environment.NewLine}", content);
    }

    /// <summary>
    /// Test for writing string content with cancellation token.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact(DisplayName = "Should write content with cancellation token.")]
    public async Task ShouldWriteContentWithCancellationTokenAsync()
    {
        var logger = new Mock<ILogger<FlatFileWriter<SampleWriterContent>>>();
        var setting = new Mock<IFileWriterSetting>();
        using var mem = new MemoryStream();
        await using var baseWriter = new StreamWriter(mem);
        setting.Setup(p => p.GetWriter()).Returns(baseWriter);
        using var writer = new FlatFileWriter<SampleWriterContent>(logger.Object, setting.Object);
        await writer.WriteAsync(new SampleWriterContent(), CancellationToken.None);

        mem.Seek(0, SeekOrigin.Begin);
        using var buffer = MemoryPool<byte>.Shared.Rent();
        var length = await mem.ReadAsync(buffer.Memory);
        var content = Encoding.UTF8.GetString(buffer.Memory.Span[..length]);
        Assert.Equal($"test{Environment.NewLine}", content);
    }
}