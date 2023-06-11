namespace BatchSharp.Writer;

/// <summary>
/// Interface of setting for file writer class.
/// </summary>
public interface IFileWriterSetting
{
    /// <summary>
    /// Get StreamWriter.
    /// </summary>
    /// <returns>StreamWriter instance.</returns>
    StreamWriter GetWriter();
}