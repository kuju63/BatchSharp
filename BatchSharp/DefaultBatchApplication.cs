using BatchSharp.Processor;
using BatchSharp.Reader;

using Microsoft.Extensions.Logging;

namespace BatchSharp;

/// <summary>
/// Class for running a batch application.
/// </summary>
/// <typeparam name="TRead">DataBinding class of datasource.</typeparam>
/// <typeparam name="TResult">Processing result type.</typeparam>
public class DefaultBatchApplication<TRead, TResult> : IBatchApplication
    where TRead : notnull
    where TResult : notnull
{
    private readonly ILogger<DefaultBatchApplication<TRead, TResult>> _logger;
    private readonly IReader<TRead> _reader;
    private readonly IProcessor<TRead, TResult> _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBatchApplication{TRead, TResult}"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="reader">Reader instance.</param>
    /// <param name="processor">Processor instance.</param>
    public DefaultBatchApplication(
        ILogger<DefaultBatchApplication<TRead, TResult>> logger,
        IReader<TRead> reader,
        IProcessor<TRead, TResult> processor)
    {
        _logger = logger;
        _reader = reader;
        _processor = processor;
    }

    /// <inheritdoc/>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TRead> readData;
        while ((readData = _reader.Read()).Any())
        {
            await Task.Run(
                () =>
                {
                    foreach (var item in readData)
                    {
                        _logger.LogInformation("Read data: {Item}", item);
                        var processingResult = _processor.Process(item);
                        _logger.LogInformation("Processed data: {Item}", processingResult);
                    }
                },
                cancellationToken);
        }
    }
}