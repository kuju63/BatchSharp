using BatchSharp.Processor;

namespace BatchSharp.Example.Processor;

/// <summary>
/// Class for processing data.
/// This class is example.
/// </summary>
public class ExampleProcessor : IProcessor<string, int>
{
    /// <inheritdoc cref="IProcessor{TSource,TResult}"/>
    public int Process(string source)
    {
        return source.Length;
    }
}