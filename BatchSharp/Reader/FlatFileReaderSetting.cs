using System.Text;

namespace BatchSharp.Reader;

/// <summary>
/// Setting class for <see cref="FlatFileReader"/>.
/// </summary>
public class FlatFileReaderSetting : IFileReaderSetting
{
    private readonly string _filePath;

    private readonly Encoding _encoding;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlatFileReaderSetting"/> class.
    /// </summary>
    /// <param name="filePath">Input file path.</param>
    /// <param name="lineReadCount">Read line count.</param>
    /// <param name="encoding">Input file character encoding.</param>
    /// <exception cref="FileNotFoundException">The file does not exists on file path.</exception>
    public FlatFileReaderSetting(string filePath, int lineReadCount = 1, Encoding? encoding = null)
    {
        _filePath = filePath;
        LineReadCount = lineReadCount;
        _encoding = encoding ?? Encoding.UTF8;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Input file does not exist.", filePath);
        }
    }

    /// <summary>
    /// Gets read line count.
    /// </summary>
    public int LineReadCount { get; init; }

    /// <summary>
    /// Get <see cref="StreamReader"/> instance.
    /// </summary>
    /// <returns>Reader instance.</returns>
    public StreamReader GetStreamReader()
    {
        return new StreamReader(_filePath, _encoding);
    }
}