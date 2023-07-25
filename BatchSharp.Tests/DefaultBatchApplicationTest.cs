using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Step;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class for <see cref="DefaultBatchApplication{TRead,TResult}"/>.
/// </summary>
public class DefaultBatchApplicationTest
{
    private readonly Mock<ILogger<DefaultBatchApplication>> _logger = new();
    private readonly Mock<IStep> _step = new();

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync()"/>.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenExecutionSuccess()
    {
        _step.Setup(x => x.ExecuteAsync(default));
        var application = new DefaultBatchApplication(_logger.Object, _step.Object);

        await application.RunAsync();

        _step.Verify(x => x.ExecuteAsync(default), Times.Once());
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead,TResult}.RunAsync(CancellationToken)"/>.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCancelledAsync()
    {
        _step.Setup(x => x.ExecuteAsync(It.IsAny<CancellationToken>()));
        var application =
            new DefaultBatchApplication(
                _logger.Object,
                _step.Object);

        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();
        await Assert.ThrowsAsync<TaskCanceledException>(() => application.RunAsync(cancellationTokenSource.Token));

        _step.Verify(x => x.ExecuteAsync(It.IsAny<CancellationToken>()), Times.Never());
    }
}