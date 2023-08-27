using System.Runtime.CompilerServices;

using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Step;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

namespace BatchSharp;

/// <summary>
/// Class for running a batch application.
/// </summary>
public class DefaultBatchApplication : IBatchApplication
{
    private readonly ILogger<DefaultBatchApplication> _logger;
    private readonly StepCollection _steps;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBatchApplication"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="steps">Steps.</param>
    public DefaultBatchApplication(
        ILogger<DefaultBatchApplication> logger,
        StepCollection steps)
    {
        _logger = logger;
        _steps = steps;
    }

    /// <inheritdoc/>
    public async Task RunAsync()
    {
        await RunAsync(default);
    }

    /// <inheritdoc/>
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogDebug("Start batch application.");
            _steps.ForEach(async step => await step.ExecuteAsync(cancellationToken));
            _logger.LogDebug("End batch application.");
        }
        else
        {
            await Task.FromCanceled(cancellationToken);
        }
    }
}