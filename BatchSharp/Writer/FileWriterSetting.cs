using System.Text;

namespace BatchSharp.Writer;

/// <summary>
/// Class of setting for FlatFileWriter.
/// </summary>
public class FileWriterSetting : IFileWriterSetting
{
    private readonly string _filePath;
    private readonly Encoding _encoding;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileWriterSetting"/> class.
    /// </summary>
    /// <param name="filePath">output file path.</param>
    /// <param name="encoding">Output file encoding.</param>
    public FileWriterSetting(string filePath, Encoding? encoding = null)
    {
        _filePath = filePath;
        _encoding = encoding ?? Encoding.UTF8;
    }

    /// <inheritdoc cref="IFileWriterSetting.GetWriter"/>
    public StreamWriter GetWriter()
    {
        var fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

        return new StreamWriter(fs, _encoding);
    }
}