using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class for <see cref="DefaultBatchApplication{TRead,TResult}"/>.
/// </summary>
public class DefaultBatchApplicationTest
{
    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync"/>.
    /// If reader returns single element list, should return completed.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string, int>>>();
        var reader = new Mock<IReader<string>>();
        reader.SetupSequence(x => x.Read())
            .Returns(new List<string> { "test" })
            .Returns(new List<string>());
        var processor = new Mock<IProcessor<string, int>>();
        processor.SetupSequence(x => x.Process(It.IsIn("test")))
            .Returns(4);
        var writer = new Mock<IWriter<int>>();
        writer.SetupSequence(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                reader.Object,
                processor.Object,
                writer.Object);

        await application.RunAsync();

        reader.Verify((r) => r.Read(), Times.Exactly(2));
        processor.Verify(p => p.Process(It.IsIn("test")), Times.Once());
        writer.Verify(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()), Times.Once());
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync"/>.
    /// Should return completed when reader returns empty list.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadEmptyAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string, int>>>();
        var reader = new Mock<IReader<string>>();
        reader.SetupSequence(x => x.Read())
            .Returns(new List<string>());
        var processor = new Mock<IProcessor<string, int>>();
        processor.SetupSequence(x => x.Process(It.IsAny<string>()));
        var writer = new Mock<IWriter<int>>();
        writer.SetupSequence(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                reader.Object,
                processor.Object,
                writer.Object);

        await application.RunAsync();

        reader.Verify(r => r.Read(), Times.Once());
        processor.Verify(x => x.Process(It.IsAny<string>()), Times.Never());
        writer.Verify(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync"/>.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleWithCancellationTokenAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string, int>>>();
        var reader = new Mock<IReader<string>>();
        reader.SetupSequence(x => x.Read())
            .Returns(new List<string> { "test" })
            .Returns(new List<string>());
        var processor = new Mock<IProcessor<string, int>>();
        processor.SetupSequence(x => x.Process(It.IsIn("test")))
            .Returns(4);
        var writer = new Mock<IWriter<int>>();
        writer.SetupSequence(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                reader.Object,
                processor.Object,
                writer.Object);
        using var cancellationTokenSource = new CancellationTokenSource();
        await application.RunAsync(cancellationTokenSource.Token);

        reader.Verify((r) => r.Read(), Times.Exactly(2));
        processor.Verify(p => p.Process(It.IsIn("test")), Times.Once());
        writer.Verify(x => x.WriteAsync(It.IsIn(4), cancellationTokenSource.Token), Times.Once());
    }
}