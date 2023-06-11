namespace BatchSharp.Processor;

/// <summary>
/// Interface for a processor that takes a source and returns a result.
/// </summary>
/// <typeparam name="TSource">DataSource type.</typeparam>
/// <typeparam name="TResult">Result type.</typeparam>
public interface IProcessor<in TSource, out TResult>
    where TSource : notnull
    where TResult : notnull
{
    /// <summary>
    /// Execute the processor.
    /// </summary>
    /// <param name="source">DataSource.</param>
    /// <returns>Process result.</returns>
    TResult Process(TSource source);
}