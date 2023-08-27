using Microsoft.Extensions.Hosting;

namespace BatchSharp.Tests;

/// <summary>
/// Represents test class for <see cref="BatchHost"/>.
/// </summary>
public class BatchHostTest
{
    /// <summary>
    /// Should return batch host.
    /// </summary>
    [Fact]
    public void ShouldReturnBatchHost()
    {
        // Arrange
        _ = Host.CreateDefaultBuilder().ConfigureBatch(builder => builder.Build());
    }
}