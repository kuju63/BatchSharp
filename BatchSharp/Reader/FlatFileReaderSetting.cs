using System.Text;

namespace BatchSharp.Reader;

/// <summary>
/// Setting class for <see cref="FlatFileReader"/>.
/// </summary>
public class FlatFileReaderSetting : IFileReaderSetting
{
    private readonly string _filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlatFileReaderSetting"/> class.
    /// </summary>
    /// <param name="filePath">Input file path.</param>
    /// <param name="lineReadCount">Read line count.</param>
    /// <param name="encoding">Input file character encoding.</param>
    public FlatFileReaderSetting(string filePath, int lineReadCount = 1, Encoding? encoding = null)
    {
        _filePath = filePath;
        LineReadCount = lineReadCount;
        FileEncoding = encoding ?? Encoding.UTF8;
    }

    /// <summary>
    /// Gets read line count.
    /// </summary>
    public int LineReadCount { get; }

    /// <summary>
    /// Gets file encoding.
    /// </summary>
    public Encoding FileEncoding { get; }

    /// <summary>
    /// Get <see cref="StreamReader"/> instance.
    /// </summary>
    /// <returns>Reader instance.</returns>
    /// <exception cref="FileNotFoundException">The file does not exists on file path.</exception>
    public StreamReader GetStreamReader()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Input file does not exist.", _filePath);
        }

        return new StreamReader(_filePath, FileEncoding);
    }
}