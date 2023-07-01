using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BatchSharp;

/// <summary>
/// Class of batch host service.
/// </summary>
public sealed class BatchHostedService : IHostedService, IDisposable
{
    private readonly ILogger<BatchHostedService> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IBatchApplication _batchApplication;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchHostedService"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="appLifetime">Host application lifetime manager.</param>
    /// <param name="batchApplication">Application execution endpoint.</param>
    public BatchHostedService(
        ILogger<BatchHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IBatchApplication batchApplication)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _batchApplication = batchApplication;
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start batch application");
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop batch application");
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    private void OnStopped()
    {
        _logger.LogInformation("Stop batch application");
    }

    private void OnStopping()
    {
        _logger.LogInformation("Batch application is stopping");
        _cancellationTokenSource.Cancel();
    }

    private void OnStarted()
    {
        _logger.LogInformation("Batch application is started");
        _batchApplication.RunAsync(_cancellationTokenSource.Token).Wait();
        _appLifetime.StopApplication();
    }
}