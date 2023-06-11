namespace BatchSharp.Reader;

/// <summary>
/// Interface for reading data from a source.
/// </summary>
/// <typeparam name="T">DataBinding type.</typeparam>
public interface IReader<T>
    where T : class
{
    /// <summary>
    /// Read data from a data source.
    /// </summary>
    /// <returns>Data.</returns>
    List<T> Read();
}