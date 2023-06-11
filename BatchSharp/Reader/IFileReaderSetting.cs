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
    /// Get <see cref="StreamReader"/> instance.
    /// </summary>
    /// <returns>Stream reader.</returns>
    StreamReader GetStreamReader();
}