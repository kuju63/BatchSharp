using Microsoft.Extensions.Logging;

namespace BatchSharp.Example;

/// <summary>
/// Class of example batch application.
/// </summary>
public class ExampleBatchApplication : IBatchApplication
{
    private readonly ILogger<ExampleBatchApplication> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleBatchApplication"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    public ExampleBatchApplication(ILogger<ExampleBatchApplication> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IBatchApplication.RunAsync"/>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Run batch asynchronous process");
        await Task.Run(() => Thread.Sleep(10000), cancellationToken);
    }
}