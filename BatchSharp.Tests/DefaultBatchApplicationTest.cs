using BatchSharp.Reader;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class for <see cref="DefaultBatchApplication{TRead}"/>.
/// </summary>
public class DefaultBatchApplicationTest
{
    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead}.RunAsync"/>.
    /// If reader returns single element list, should return completed.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadSingleAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string>>>();
        var reader = new Mock<IReader<string>>();
        reader.SetupSequence(x => x.Read())
            .Returns(new List<string> { "test" })
            .Returns(new List<string>());
        var application = new DefaultBatchApplication<string>(logger.Object, reader.Object);

        await application.RunAsync();

        reader.Verify((r) => r.Read(), Times.Exactly(2));
    }

    /// <summary>
    /// Test for <see cref="DefaultBatchApplication{TRead}.RunAsync"/>.
    /// Should return completed when reader returns empty list.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    [Fact]
    public async Task ShouldReturnCompletedWhenReadEmptyAsync()
    {
        var logger = new Mock<ILogger<DefaultBatchApplication<string>>>();
        var reader = new Mock<IReader<string>>();
        reader.SetupSequence(x => x.Read())
            .Returns(new List<string>());
        var application = new DefaultBatchApplication<string>(logger.Object, reader.Object);

        await application.RunAsync();

        reader.Verify((r) => r.Read(), Times.Once());
    }
}