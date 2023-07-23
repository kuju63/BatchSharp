namespace BatchSharp.Step;

/// <summary>
/// Represents status of step.
/// </summary>
public class StepState : IStepState
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

    /// <inheritdoc cref="IStepState.IsCanceled" />
    public bool IsCanceled { get; private set; }

    /// <inheritdoc cref="IStepState.IsCompleted"/>
    public bool IsCompleted { get; private set; }

    /// <inheritdoc cref="IStepState.IsErrored"/>
    public bool IsErrored { get; private set; }

    /// <inheritdoc cref="IStepState.HandledException"/>
    public Exception? HandledException { get; private set; }

    /// <inheritdoc cref="IStepState.IsWorking"/>
    public bool IsWorking { get; private set; }

    /// <inheritdoc cref="IStepState.CancelStep()"/>
    public void CancelStep()
    {
        Cancel(null);
    }

    /// <inheritdoc cref="IStepState.CancelStep(Exception)"/>
    public void CancelStep(Exception exception)
    {
        Cancel(exception);
        IsErrored = true;
    }

    /// <inheritdoc cref="IStepState.StartStep()"/>
    public void StartStep()
    {
        if (IsCanceled || IsCompleted)
        {
            throw new InvalidOperationException("The step is already canceled or completed.");
        }

        IsWorking = true;
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