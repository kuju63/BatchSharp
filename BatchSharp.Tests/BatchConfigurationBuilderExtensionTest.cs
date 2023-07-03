using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Moq;

namespace BatchSharp.Tests;

/// <summary>
/// Test class for <see cref="BatchConfigurationBuilderExtension">BatchConfigurationBuilderExtension</see>.
/// </summary>
public class BatchConfigurationBuilderExtensionTest
{
    private readonly ConfigurationBuilder _builder = new();

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithoutArgs()
    {
        _builder.AddBatchConfiguration();
        _builder.Build().Should().NotBeNull();
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithEnvironmentName()
    {
        _builder.AddBatchConfiguration("Test");
        _builder.Build().Should().NotBeNull();
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithEnvironment()
    {
        _builder.AddBatchConfiguration("Test", "BATCH_");
        _builder.Build().Should().NotBeNull();
    }

    /// <summary>
    /// Test for <see cref="BatchConfigurationBuilderExtension.AddBatchConfiguration(IConfigurationBuilder, string, string, string)"/>.
    /// </summary>
    [Fact]
    public void ShouldNotThrownExceptionWithFullArguments()
    {
        var env = new Mock<IHostEnvironment>();
        env.SetupGet(x => x.EnvironmentName).Returns("Test");
        _builder.AddBatchConfiguration(env.Object, "BATCH_", "application");
        _builder.Build().Should().NotBeNull();

        env.Verify(x => x.EnvironmentName, Times.Once);
    }
}