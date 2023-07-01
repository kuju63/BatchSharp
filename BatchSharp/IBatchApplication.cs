namespace BatchSharp;

/// <summary>
/// Interface of batch application logic class.
/// </summary>
public interface IBatchApplication
{
    /// <summary>
    /// Execute batch logic by asynchronous.
    /// </summary>
    /// <returns>Task result.</returns>
    Task RunAsync();

    /// <summary>
    /// Execute batch logic by asynchronous.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>task result.</returns>
    Task RunAsync(CancellationToken cancellationToken);
}