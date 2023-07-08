using Microsoft.Extensions.Logging;

namespace BatchSharp.Reader;

/// <summary>
/// Reader class for flat file.
/// </summary>
public class FlatFileReader : IReader<string>
{
    private readonly ILogger<FlatFileReader> _logger;
    private readonly IFileReaderSetting _setting;

    private readonly StreamReader _reader;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlatFileReader"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="setting">Reader configuration.</param>
    public FlatFileReader(ILogger<FlatFileReader> logger, IFileReaderSetting setting)
    {
        _logger = logger;
        _setting = setting;
        logger.LogInformation("FlatFileReader is initialized");
        _reader = setting.GetStreamReader();
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> ReadAsync()
    {
        if (!_reader.EndOfStream)
        {
            for (int i = 0; i < _setting.LineReadCount && _reader.Peek() >= 0; i++)
            {
                var line = await _reader.ReadLineAsync();
                if (line is not null)
                {
                    yield return line;
                }
            }
        }
        else
        {
            _logger.LogInformation("FlatFileReader reached end of file");
        }
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        _reader.Dispose();
        GC.SuppressFinalize(this);
    }
}