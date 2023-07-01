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
    private readonly Mock<IReader<string>> _reader = new();
    private readonly Mock<IProcessor<string, int>> _processor = new();
    private readonly Mock<IWriter<int>> _writer = new();

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync"/>.
    /// If reader returns single element list, should return completed.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string, int>>>();
        _reader.SetupSequence(x => x.Read())
            .Returns(new List<string> { "test" })
            .Returns(new List<string>());

        _processor.SetupSequence(x => x.Process(It.IsIn("test")))
            .Returns(4);
        _writer.SetupSequence(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                _reader.Object,
                _processor.Object,
                _writer.Object);

        await application.RunAsync();

        _reader.Verify((r) => r.Read(), Times.Exactly(2));
        _processor.Verify(p => p.Process(It.IsIn("test")), Times.Once());
        _writer.Verify(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()), Times.Once());
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
        _reader.SetupSequence(x => x.Read())
            .Returns(new List<string>());
        _processor.SetupSequence(x => x.Process(It.IsAny<string>()));
        _writer.SetupSequence(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                _reader.Object,
                _processor.Object,
                _writer.Object);

        await application.RunAsync();

        _reader.Verify(r => r.Read(), Times.Once());
        _processor.Verify(x => x.Process(It.IsAny<string>()), Times.Never());
        _writer.Verify(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync"/>.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleWithCancellationTokenAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string, int>>>();
        _reader.SetupSequence(x => x.Read())
            .Returns(new List<string> { "test" })
            .Returns(new List<string>());
        _processor.SetupSequence(x => x.Process(It.IsIn("test")))
            .Returns(4);
        _writer.SetupSequence(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                _reader.Object,
                _processor.Object,
                _writer.Object);
        using var cancellationTokenSource = new CancellationTokenSource();
        await application.RunAsync(cancellationTokenSource.Token);

        _reader.Verify((r) => r.Read(), Times.Exactly(2));
        _processor.Verify(p => p.Process(It.IsIn("test")), Times.Once());
        _writer.Verify(x => x.WriteAsync(It.IsIn(4), cancellationTokenSource.Token), Times.Once());
    }
}