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
    private readonly IStep _step;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBatchApplication"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="step">Step.</param>
    public DefaultBatchApplication(
        ILogger<DefaultBatchApplication> logger,
        IStep step)
    {
        _logger = logger;
        _step = step;
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
            await _step.ExecuteAsync(cancellationToken);
            _logger.LogDebug("End batch application.");
        }
        else
        {
            await Task.FromCanceled(cancellationToken);
        }
    }
}