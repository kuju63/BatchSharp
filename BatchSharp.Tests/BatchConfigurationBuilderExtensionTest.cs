using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class for <see cref="BatchConfigurationBuilderExtension">BatchConfigurationBuilderExtension</see>.
/// </summary>
public class BatchConfigurationBuilderExtensionTest
{
    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithoutArgs()
    {
        var builder = new ConfigurationBuilder();
        builder.AddBatchConfiguration();
        Assert.NotNull(builder.Build());
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithEnvironmentName()
    {
        var builder = new ConfigurationBuilder();
        builder.AddBatchConfiguration("Test");
        Assert.NotNull(builder.Build());
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithEnvironment()
    {
        var builder = new ConfigurationBuilder();
        builder.AddBatchConfiguration("Test", "BATCH_");
        Assert.NotNull(builder.Build());
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string, string, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithFullArguments()
    {
        var builder = new ConfigurationBuilder();
        var env = new Mock<IHostEnvironment>();
        env.SetupGet(x => x.EnvironmentName).Returns("Test");
        builder.AddBatchConfiguration(env.Object, "BATCH_", "application");
        Assert.NotNull(builder.Build());

        env.Verify(x => x.EnvironmentName, Times.Once);
    }
}