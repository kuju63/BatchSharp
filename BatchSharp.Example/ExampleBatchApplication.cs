using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Step;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

namespace BatchSharp.Example;

/// <summary>
/// Class of example batch application.
/// </summary>
public class ExampleBatchApplication : DefaultBatchApplication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleBatchApplication"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="steps">Steps.</param>
    public ExampleBatchApplication(
        ILogger<ExampleBatchApplication> logger,
        StepCollection steps)
        : base(logger, steps)
    {
    }
}