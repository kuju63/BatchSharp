using BatchSharp.Step;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Represents test class for <see cref="JobBuilder"/>.
/// </summary>
public class JobBuilderTest
{
    /// <summary>
    /// Should return empty step collection when no step added.
    /// </summary>
    public void ShouldReturnEmptyStepCollectionWhenNoStepAdded()
    {
        // Arrange
        var services = new Mock<IServiceProvider>().Object;
        var jobBuilder = new JobBuilder(services);

        // Act
        var steps = jobBuilder.Build();

        // Assert
        Assert.Empty(steps);
    }

    /// <summary>
    /// Should return step collection with one step when one step added.
    /// </summary>
    public void ShouldReturnStepCollectionWithOneStepWhenOneStepAdded()
    {
        // Arrange
        var services = new Mock<IServiceProvider>().Object;
        var jobBuilder = new JobBuilder(services);
        jobBuilder.AddStep(_ => new Mock<IStep>().Object);

        // Act
        var steps = jobBuilder.Build();

        // Assert
        Assert.Single(steps);
    }
}