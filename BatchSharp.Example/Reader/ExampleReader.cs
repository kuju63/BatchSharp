using BatchSharp.Reader;

namespace BatchSharp.Example.Reader;

/// <summary>
/// Class of example reader.
/// </summary>
public class ExampleReader : IReader<string>
{
    private readonly List<string> _exampleDataSource = new() { "sample1", "sample2", "sample3" };
    private int _index;

    /// <inheritdoc cref="IReader{T}"/>
    public async IAsyncEnumerable<string> ReadAsync()
    {
        yield return _exampleDataSource.Skip(_index).Take(1).First();
        _index++;
        await Task.Delay(10);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}