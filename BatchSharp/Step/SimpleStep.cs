using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

namespace BatchSharp.Step;

/// <summary>
/// Represents a simple step in a batch.
/// </summary>
/// <typeparam name="T1">Type of the input.</typeparam>
/// <typeparam name="T2">Type of the output.</typeparam>
public class SimpleStep<T1, T2> : IStep<T1, T2>, IDisposable
    where T1 : notnull
    where T2 : notnull
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IReader<T1> _reader;
    private readonly IProcessor<T1, T2> _processor;
    private readonly IWriter<T2> _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleStep{T1, T2}"/> class.
    /// </summary>
    /// <param name="reader">Instance of reader class for load the data from data source.</param>
    /// <param name="processor">Instance of processor.</param>
    /// <param name="writer">Instance of writer for output processor result.</param>
    public SimpleStep(IReader<T1> reader, IProcessor<T1, T2> processor, IWriter<T2> writer)
    {
        _reader = reader;
        _processor = processor;
        _writer = writer;
    }

    /// <summary>
    /// Release resources.
    /// </summary>
    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="IStep" />
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}