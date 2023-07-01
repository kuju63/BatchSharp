using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class of <see cref="BatchHostedService"/>.
/// </summary>
public class BatchHostedServiceTest
{
    private readonly Mock<ILogger<BatchHostedService>> _logger = new();
    private readonly Mock<IHostApplicationLifetime> _appLifetime = new();
    private readonly Mock<IBatchApplication> _batchApplication = new();

    /// <summary>
    /// Test <see cref="BatchHostedService.StartAsync(CancellationToken)"/>.
    /// Should return task completed when start async.
    /// </summary>
    [Fact]
    public void ShouldReturnTaskCompletedWhenStartAsync()
    {
        // Arrange
        using var batchHostedService = new BatchHostedService(
            _logger.Object,
            _appLifetime.Object,
            _batchApplication.Object);

        // Act
        var result = batchHostedService.StartAsync(CancellationToken.None);

        // Assert
        Assert.True(result.IsCompleted);
    }

    /// <summary>
    /// Test <see cref="BatchHostedService.StopAsync(CancellationToken)"/>.
    /// </summary>
    [Fact]
    public void ShouldReturnTaskCompletedWhenStopAsync()
    {
        // Arrange
        using var batchHostedService = new BatchHostedService(
            _logger.Object,
            _appLifetime.Object,
            _batchApplication.Object);

        // Act
        var result = batchHostedService.StopAsync(CancellationToken.None);

        // Assert
        Assert.True(result.IsCompleted);
    }
}