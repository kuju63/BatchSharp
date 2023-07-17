namespace BatchSharp.Step;

/// <summary>
/// Represents a step in a batch.
/// </summary>
/// <typeparam name="T1">Type of the input.</typeparam>
/// <typeparam name="T2">Type of the output.</typeparam>
public interface IStep<T1, T2>
    where T1 : notnull
    where T2 : notnull
{
    /// <summary>
    /// Execute the step.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Asynchronous result.</returns>
    Task ExecuteAsync(CancellationToken cancellationToken);
}