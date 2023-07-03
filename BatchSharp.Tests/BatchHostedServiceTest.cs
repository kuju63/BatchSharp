using FluentAssertions;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class of <see cref="BatchHostedService"/>.
/// </summary>
public class BatchHostedServiceTest : IDisposable
{
    private readonly Mock<ILogger<BatchHostedService>> _logger = new();
    private readonly Mock<IHostApplicationLifetime> _appLifetime = new();
    private readonly Mock<IBatchApplication> _batchApplication = new();
    private readonly BatchHostedService _hostedService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchHostedServiceTest"/> class.
    /// </summary>
    public BatchHostedServiceTest()
    {
        _hostedService = new BatchHostedService(
            _logger.Object,
            _appLifetime.Object,
            _batchApplication.Object);
    }

    /// <summary>
    /// Test <see cref="BatchHostedService.StartAsync(CancellationToken)"/>.
    /// Should return task completed when start async.
    /// </summary>
    [Fact]
    public void ShouldReturnTaskCompletedWhenStartAsync()
    {
        // Act
        var result = _hostedService.StartAsync(CancellationToken.None);

        // Assert
        result.IsCompleted.Should().BeTrue();
    }

    /// <summary>
    /// Test <see cref="BatchHostedService.StopAsync(CancellationToken)"/>.
    /// </summary>
    [Fact]
    public void ShouldReturnTaskCompletedWhenStopAsync()
    {
        // Act
        var result = _hostedService.StopAsync(CancellationToken.None);

        // Assert
        result.IsCompleted.Should().BeTrue();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="BatchHostedServiceTest"/> class.
    /// </summary>
    public void Dispose()
    {
        _hostedService.Dispose();
        GC.SuppressFinalize(this);
    }
}