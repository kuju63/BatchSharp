using BatchSharp.Reader;

using Microsoft.Extensions.Logging;

namespace BatchSharp;

/// <summary>
/// Class for running a batch application.
/// </summary>
/// <typeparam name="TRead">DataBinding class of datasource.</typeparam>
public class DefaultBatchApplication<TRead> : IBatchApplication
    where TRead : class
{
    private readonly ILogger<DefaultBatchApplication<TRead>> _logger;
    private readonly IReader<TRead> _reader;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBatchApplication{TRead}"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="reader">Reader instance.</param>
    public DefaultBatchApplication(
        ILogger<DefaultBatchApplication<TRead>> logger,
        IReader<TRead> reader)
    {
        _logger = logger;
        _reader = reader;
    }

    /// <inheritdoc/>
    public Task RunAsync(CancellationToken cancellationToken = default)
    {
        List<TRead> readData;
        while ((readData = _reader.Read()).Any())
        {
            foreach (var item in readData)
            {
                _logger.LogInformation("Read data: {Item}", item);
            }
        }

        return Task.CompletedTask;
    }
}