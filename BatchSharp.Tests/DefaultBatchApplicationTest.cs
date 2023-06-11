using BatchSharp.Processor;
using BatchSharp.Reader;

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
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                reader.Object,
                processor.Object);

        await application.RunAsync();

        reader.Verify((r) => r.Read(), Times.Exactly(2));
        processor.Verify(p => p.Process(It.IsIn("test")), Times.Once());
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
        var application =
            new DefaultBatchApplication<string, int>(
                logger.Object,
                reader.Object,
                processor.Object);

        await application.RunAsync();

        reader.Verify(r => r.Read(), Times.Once());
        processor.Verify(x => x.Process(It.IsAny<string>()), Times.Never());
    }
}