using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

namespace BatchSharp.Example;

/// <summary>
/// Class of example batch application.
/// </summary>
public class ExampleBatchApplication : DefaultBatchApplication<string, int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleBatchApplication"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="reader">Reader.</param>
    /// <param name="processor">Processor.</param>
    /// <param name="writer">Writer.</param>
    public ExampleBatchApplication(
        ILogger<ExampleBatchApplication> logger,
        IReader<string> reader,
        IProcessor<string, int> processor,
        IWriter<int> writer)
        : base(logger, reader, processor, writer)
    {
    }
}