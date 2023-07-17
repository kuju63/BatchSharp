namespace BatchSharp.Step;

/// <summary>
/// Represents status of step.
/// </summary>
public class StepState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StepState"/> class.
    /// </summary>
    public StepState()
    {
        IsCompleted = false;
        IsCanceled = false;
        IsErrored = false;
    }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is canceled.
    /// </summary>
    public bool IsCanceled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is completed.
    /// </summary>
    public bool IsCompleted { get; private set; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is errored.
    /// </summary>
    public bool IsErrored { get; private set; }

    /// <summary>
    /// Gets a value indicating whether gets a value indicating the status which the step is canceled.
    /// </summary>
    public Exception? HandledException { get; private set; }

    /// <summary>
    /// Cancel the step.
    /// </summary>
    public void CancelStep()
    {
        Cancel(null);
    }

    /// <summary>
    /// Cancel the step with handled exception.
    /// </summary>
    /// <param name="exception">Handled exception.</param>
    public void CancelStep(Exception exception)
    {
        Cancel(exception);
        IsErrored = true;
    }

    private void Cancel(Exception? exception)
    {
        IsCanceled = true;
        if (exception is not null)
        {
            HandledException = exception;
        }
    }
}