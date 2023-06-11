using System.Text;

using BatchSharp.Reader;

using Microsoft.Extensions.Logging;

using Moq;

namespace BatchSharp.Tests.Reader;

/// <summary>
/// Test class for <see cref="FlatFileReader"/>.
/// </summary>
public class FlatFileReaderTest
{
    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return empty list.
    /// </summary>
    [Fact(DisplayName = "Should return empty list when read file is empty")]
    public void ShouldReturnEmptyWhenFileIsEmpty()
    {
        // Arrange
        var logger = new Mock<ILogger<FlatFileReader>>();
        var setting = new Mock<IFileReaderSetting>();
        using var fileReader = new StreamReader(new MemoryStream());
        setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        setting.SetupGet(x => x.LineReadCount).Returns(1);
        using var reader = new FlatFileReader(logger.Object, setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        Assert.Empty(result);

        setting.Verify(x => x.GetStreamReader(), Times.Once);
        setting.VerifyGet(x => x.LineReadCount, Times.Never);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return single line.
    /// </summary>
    [Fact(DisplayName = "Should return single line when read file has single line")]
    public void ShouldReturnSingleLineWhenFileHasSingleLine()
    {
        // Arrange
        var logger = new Mock<ILogger<FlatFileReader>>();
        var setting = new Mock<IFileReaderSetting>();
        var line = "012345"u8.ToArray();
        using var fileReader = new StreamReader(new MemoryStream(line));
        setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        setting.SetupGet(x => x.LineReadCount).Returns(1);
        using var reader = new FlatFileReader(logger.Object, setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        Assert.Collection(result, v => Assert.Equal("012345", v));

        setting.Verify(x => x.GetStreamReader(), Times.Once);
        setting.VerifyGet(x => x.LineReadCount);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return multiple line.
    /// </summary>
    [Fact(DisplayName = "Should return multiple line when line read count is 2")]
    public void ShouldReturnMultiLine()
    {
        // Arrange
        var logger = new Mock<ILogger<FlatFileReader>>();
        var setting = new Mock<IFileReaderSetting>();
        var line = Encoding.UTF8.GetBytes($"012345{Environment.NewLine}98765");
        using var fileReader = new StreamReader(new MemoryStream(line));
        setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        setting.SetupGet(x => x.LineReadCount).Returns(2);
        using var reader = new FlatFileReader(logger.Object, setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        Assert.Collection(
            result,
            v => Assert.Equal("012345", v),
            v => Assert.Equal("98765", v));

        setting.Verify(x => x.GetStreamReader(), Times.Once);
        setting.VerifyGet(x => x.LineReadCount);
    }
}