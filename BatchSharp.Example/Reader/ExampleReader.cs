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
    public List<string> Read()
    {
        var result = _exampleDataSource.Skip(_index).Take(1).ToList();
        _index++;
        return result;
    }
}