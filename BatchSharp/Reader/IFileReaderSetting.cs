using System.Text;

namespace BatchSharp.Reader;

/// <summary>
/// Interface for setting of file reader.
/// </summary>
public interface IFileReaderSetting
{
    /// <summary>
    /// Gets read line count.
    /// </summary>
    public int LineReadCount { get; }

    /// <summary>
    /// Gets get file encoding.
    /// </summary>
    public Encoding FileEncoding { get; }

    /// <summary>
    /// Get <see cref="StreamReader"/> instance.
    /// </summary>
    /// <returns>Stream reader.</returns>
    StreamReader GetStreamReader();
}