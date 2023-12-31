using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

namespace BatchSharp.Step;

/// <summary>
/// Represents a simple step in a batch.
/// </summary>
/// <typeparam name="T1">Type of the input.</typeparam>
/// <typeparam name="T2">Type of the output.</typeparam>
public class SimpleStep<T1, T2> : IStep, IDisposable
    where T1 : notnull
    where T2 : notnull
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IReader<T1> _reader;
    private readonly IProcessor<T1, T2> _processor;
    private readonly IWriter<T2> _writer;
    private readonly ILogger<SimpleStep<T1, T2>> _logger;
    private readonly IStepState _stepState;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleStep{T1, T2}"/> class.
    /// </summary>
    /// <param name="logger">Instance of logger.</param>
    /// <param name="stepState">Instance of step state.</param>
    /// <param name="reader">Instance of reader class for load the data from data source.</param>
    /// <param name="processor">Instance of processor.</param>
    /// <param name="writer">Instance of writer for output processor result.</param>
    public SimpleStep(
        ILogger<SimpleStep<T1, T2>> logger,
        IStepState stepState,
        IReader<T1> reader,
        IProcessor<T1, T2> processor,
        IWriter<T2> writer)
    {
        _reader = reader;
        _processor = processor;
        _writer = writer;
        _logger = logger;
        _stepState = stepState;
    }

    /// <inheritdoc cref="IDisposable.Dispose" />
    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="IStep.ExecuteAsync(CancellationToken)" />
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _stepState.StartStep();
        if (!cancellationToken.IsCancellationRequested)
        {
            await foreach (var readData in _reader.ReadAsync().WithCancellation(cancellationToken))
            {
                _logger.LogInformation("Read data: {Item}", readData);
                var processingResult = _processor.Process(readData);
                _logger.LogInformation("Processed data: {Item}", processingResult);
                await _writer.WriteAsync(processingResult, cancellationToken);
            }

            _stepState.CompleteStep();
        }
        else
        {
            _stepState.CancelStep();
            await Task.FromCanceled(cancellationToken);
        }
    }
}