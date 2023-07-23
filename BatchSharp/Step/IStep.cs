namespace BatchSharp.Step;

/// <summary>
/// Represents a step in a batch.
/// </summary>
public interface IStep
{
    /// <summary>
    /// Execute the step.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Asynchronous result.</returns>
    Task ExecuteAsync(CancellationToken cancellationToken);
}