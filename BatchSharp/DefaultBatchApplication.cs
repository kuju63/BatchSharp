using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

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
    private readonly IWriter<TResult> _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBatchApplication{TRead, TResult}"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="reader">Reader instance.</param>
    /// <param name="processor">Processor instance.</param>
    /// <param name="writer">Writer instance.</param>
    public DefaultBatchApplication(
        ILogger<DefaultBatchApplication<TRead, TResult>> logger,
        IReader<TRead> reader,
        IProcessor<TRead, TResult> processor,
        IWriter<TResult> writer)
    {
        _logger = logger;
        _reader = reader;
        _processor = processor;
        _writer = writer;
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
            await foreach (var readData in _reader.ReadAsync().WithCancellation(cancellationToken))
            {
                _logger.LogInformation("Read data: {Item}", readData);
                var processingResult = _processor.Process(readData);
                _logger.LogInformation("Processed data: {Item}", processingResult);
                await _writer.WriteAsync(processingResult, cancellationToken);
            }
        }
        else
        {
            await Task.FromCanceled(cancellationToken);
        }
    }
}