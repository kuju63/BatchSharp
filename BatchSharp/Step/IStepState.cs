namespace BatchSharp.Step;

/// <summary>
/// Defines the status of step.
/// </summary>
public interface IStepState
{
    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is canceled.
    /// </summary>
    public bool IsCanceled { get; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is completed.
    /// </summary>
    public bool IsCompleted { get; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is errored.
    /// </summary>
    public bool IsErrored { get; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is canceled.
    /// </summary>
    public Exception? HandledException { get; }

    /// <summary>
    /// Cancel the step.
    /// </summary>
    public void CancelStep();

    /// <summary>
    /// Cancel the step with handled exception.
    /// </summary>
    /// <param name="exception">Handled exception.</param>
    public void CancelStep(Exception exception);
}