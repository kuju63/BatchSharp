using BatchSharp.Writer;

using Microsoft.Extensions.Logging;

namespace BatchSharp.Example.Writer;

/// <summary>
/// Class for output processing result.
/// This is example writer class.
/// </summary>
public class ExampleWriter : IWriter<int>
{
    private readonly ILogger<ExampleWriter> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleWriter"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    public ExampleWriter(ILogger<ExampleWriter> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IWriter{TResult}.WriteAsync"/>
    public Task WriteAsync(int result)
    {
        return WriteAsync(result, default);
    }

    /// <inheritdoc cref="IWriter{TResult}.WriteAsync"/>
    public Task WriteAsync(int result, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Write data: {Result}", result);
        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
    }
}