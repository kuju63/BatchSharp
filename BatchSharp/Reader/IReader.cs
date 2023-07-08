namespace BatchSharp.Reader;

/// <summary>
/// Interface for reading data from a source.
/// </summary>
/// <typeparam name="T">DataBinding type.</typeparam>
public interface IReader<out T> : IDisposable
    where T : notnull
{
    /// <summary>
    /// Read data from a data source.
    /// </summary>
    /// <returns>Data.</returns>
    IAsyncEnumerable<T> ReadAsync();
}