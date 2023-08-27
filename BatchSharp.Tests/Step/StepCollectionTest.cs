using BatchSharp.Step;

using Moq;

namespace BatchSharp.Tests.Step;

/// <summary>
/// Test class for <see cref="StepCollection"/>.
/// </summary>
public class StepCollectionTest
{
    /// <summary>
    /// Test for <see cref="StepCollection.IsEmpty"/>.
    /// </summary>
    [Fact]
    public void ShouldReturnTrueWhenEmpty()
    {
        var collection = new StepCollection();

        Assert.True(collection.IsEmpty);
    }

    /// <summary>
    /// Test for <see cref="StepCollection.IsEmpty"/>.
    /// </summary>
    [Fact]
    public void ShouldReturnFalseWhenCollectionIsNotEmpty()
    {
        var collection = new StepCollection(new List<IStep> { new Mock<IStep>().Object });

        Assert.False(collection.IsEmpty);
    }
}