using Microsoft.Extensions.Logging;

namespace BatchSharp.Writer;

/// <summary>
/// Class for writing the result to flat file.
/// </summary>
/// <typeparam name="T">Output result type.</typeparam>
public class FlatFileWriter<T> : IWriter<T>
    where T : notnull
{
    private readonly ILogger<FlatFileWriter<T>> _logger;
    private readonly StreamWriter _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlatFileWriter{T}"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="setting">Setting for writing file.</param>
    public FlatFileWriter(ILogger<FlatFileWriter<T>> logger, IFileWriterSetting setting)
    {
        _logger = logger;
        _writer = setting.GetWriter();
    }

    /// <inheritdoc/>
    public Task WriteAsync(T result)
    {
        return WriteAsync(result, default);
    }

    /// <summary>
    /// Writes the result to flat file.
    /// </summary>
    /// <param name="result">Output result.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Asynchronous result.</returns>
    public async Task WriteAsync(T result, CancellationToken cancellationToken)
    {
        if (result is string content)
        {
            await _writer.WriteLineAsync(content.AsMemory(), cancellationToken);
        }
        else
        {
            await _writer.WriteLineAsync(result.ToString().AsMemory(), cancellationToken);
        }

        await _writer.FlushAsync();
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        _writer.Dispose();
        GC.SuppressFinalize(this);
    }
}