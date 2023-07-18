using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Step;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests.Step;

/// <summary>
/// Test class for <see cref="SimpleStep{T1,T2}"/>.
/// </summary>
public class SimpleStepTest : IDisposable
{
    private readonly Mock<IReader<string>> _reader = new();
    private readonly Mock<IProcessor<string, int>> _processor = new();
    private readonly Mock<IWriter<int>> _writer = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly Mock<ILogger<SimpleStep<string, int>>> _logger = new();
    private readonly Mock<IStepState> _stepState = new();

    /// <summary>
    /// Release resources.
    /// </summary>
    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Should return canceled task.
    /// </summary>
    [Fact]
    public async void ShouldReturnCanceledTask()
    {
        _reader.Setup(r => r.ReadAsync());
        _processor.Setup(p => p.Process(It.IsAny<string>()));
        _writer.Setup(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        _stepState.Setup(s => s.CancelStep());
        _cancellationTokenSource.Cancel();

        var step = new SimpleStep<string, int>(
            _logger.Object,
            _stepState.Object,
            _reader.Object,
            _processor.Object,
            _writer.Object);

        await Assert.ThrowsAsync<TaskCanceledException>(async () => await step.ExecuteAsync(_cancellationTokenSource.Token));

        _reader.Verify((r) => r.ReadAsync(), Times.Never());
        _processor.Verify(p => p.Process(It.IsAny<string>()), Times.Never());
        _writer.Verify(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
        _stepState.Verify(s => s.CancelStep(), Times.Once());
    }

    /// <summary>
    /// Test for <see cref="SimpleStep{T1,T2}.ExecuteAsync(CancellationToken)"/>.
    /// If reader returns single element list, should return completed.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleAsync()
    {
        _reader.SetupSequence(x => x.ReadAsync())
            .Returns(new[] { "test" }.ToAsyncEnumerable())
            .Returns(AsyncEnumerable.Empty<string>());

        _processor.SetupSequence(x => x.Process("test"))
            .Returns(4);
        _writer.SetupSequence(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var step =
            new SimpleStep<string, int>(
                _logger.Object,
                _stepState.Object,
                _reader.Object,
                _processor.Object,
                _writer.Object);

        await step.ExecuteAsync(_cancellationTokenSource.Token);

        _reader.Verify((r) => r.ReadAsync(), Times.Once());
        _processor.Verify(p => p.Process("test"), Times.Once());
        _writer.Verify(x => x.WriteAsync(It.IsIn(4), It.IsAny<CancellationToken>()), Times.Once());
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync()"/>.
    /// Should return completed when reader returns empty list.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadEmptyAsync()
    {
        _reader.SetupSequence(x => x.ReadAsync())
            .Returns(AsyncEnumerable.Empty<string>());
        _processor.SetupSequence(x => x.Process(It.IsAny<string>()));
        _writer.SetupSequence(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var step =
            new SimpleStep<string, int>(
                _logger.Object,
                _stepState.Object,
                _reader.Object,
                _processor.Object,
                _writer.Object);

        await step.ExecuteAsync(_cancellationTokenSource.Token);

        _reader.Verify(r => r.ReadAsync(), Times.Once());
        _processor.Verify(x => x.Process(It.IsAny<string>()), Times.Never());
        _writer.Verify(x => x.WriteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}