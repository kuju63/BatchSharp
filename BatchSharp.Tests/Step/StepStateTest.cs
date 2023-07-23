using BatchSharp.Step;

namespace BatchSharp.Tests.Step;

/// <summary>
/// Test class for <see cref="StepState"/>.
/// </summary>
public class StepStateTest
{
    /// <summary>
    /// Test for <see cref="StepState.CancelStep()"/>.
    /// </summary>
    [Fact]
    public void ShouldInitializeNewInstance()
    {
        var state = new StepState();

        state.IsCanceled.Should().BeFalse();
        state.IsCompleted.Should().BeFalse();
        state.IsErrored.Should().BeFalse();
        state.HandledException.Should().BeNull();
        state.IsWorking.Should().BeFalse();
    }

    /// <summary>
    /// Test for <see cref="StepState.CancelStep()"/>.
    /// </summary>
    [Fact]
    public void ShouldCancelStep()
    {
        var state = new StepState();

        state.CancelStep();

        state.IsCanceled.Should().BeTrue();
        state.IsCompleted.Should().BeFalse();
        state.IsErrored.Should().BeFalse();
        state.HandledException.Should().BeNull();
        state.IsWorking.Should().BeFalse();
    }

    /// <summary>
    /// Test for <see cref="StepState.CancelStep(Exception)"/>.
    /// </summary>
    [Fact]
    public void ShouldCancelStepWithHandledException()
    {
        var state = new StepState();

        var handledException = new Exception();
        state.CancelStep(handledException);

        state.IsCanceled.Should().BeTrue();
        state.IsCompleted.Should().BeFalse();
        state.IsErrored.Should().BeTrue();
        state.HandledException.Should().Be(handledException);
        state.IsWorking.Should().BeFalse();
    }

    /// <summary>
    /// Test for <see cref="StepState.StartStep()"/>.
    /// </summary>
    [Fact]
    public void ShouldStartStep()
    {
        var state = new StepState();

        state.StartStep();

        state.IsCanceled.Should().BeFalse();
        state.IsCompleted.Should().BeFalse();
        state.IsErrored.Should().BeFalse();
        state.HandledException.Should().BeNull();
        state.IsWorking.Should().BeTrue();
    }

    /// <summary>
    /// Test for <see cref="StepState.StartStep()"/>.
    /// Expected thrown <see cref="InvalidOperationException"/> when status is cancel.
    /// </summary>
    [Fact]
    public void ShouldThrownInvalidOperationExceptionWhenStartStep()
    {
        var state = new StepState();
        state.CancelStep();

        Action action = () => state.StartStep();

        action.Should().Throw<InvalidOperationException>();
    }
}