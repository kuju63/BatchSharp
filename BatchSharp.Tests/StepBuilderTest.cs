using BatchSharp.Step;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Represents test class for <see cref="StepBuilder"/>.
/// </summary>
public class StepBuilderTest
{
    /// <summary>
    /// Should return empty step collection when no step added.
    /// </summary>
    [Fact]
    public void ShouldReturnEmptyStepCollectionWhenNoStepAdded()
    {
        // Arrange
        var services = new Mock<IServiceProvider>().Object;
        var jobBuilder = new StepBuilder(services);

        // Act
        var steps = jobBuilder.Build();

        // Assert
        Assert.Empty(steps);
    }

    /// <summary>
    /// Should return step collection with one step when one step added.
    /// </summary>
    [Fact]
    public void ShouldReturnStepCollectionWithOneStepWhenOneStepAdded()
    {
        // Arrange
        var services = new Mock<IServiceProvider>().Object;
        var jobBuilder = new StepBuilder(services);
        jobBuilder.AddStep(_ => new Mock<IStep>().Object);

        // Act
        var steps = jobBuilder.Build();

        // Assert
        Assert.Single(steps);
    }
}