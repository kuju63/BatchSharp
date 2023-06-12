namespace BatchSharp.Writer;

/// <summary>
/// Interface of writer.
/// </summary>
/// <typeparam name="TResult">Type of processing result.</typeparam>
public interface IWriter<in TResult> : IDisposable
    where TResult : notnull
{
    /// <summary>
    /// Output processing result.
    /// </summary>
    /// <param name="result">Type of processing result.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Asynchronous result for output result.</returns>
    Task WriteAsync(TResult result, CancellationToken cancellationToken = default);
}