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
    private readonly Mock<ILogger<FlatFileReader>> _logger = new();
    private readonly Mock<IFileReaderSetting> _setting = new();

    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return empty list.
    /// </summary>
    [Fact(DisplayName = "Should return empty list when read file is empty")]
    public void ShouldReturnEmptyWhenFileIsEmpty()
    {
        // Arrange
        using var fileReader = new StreamReader(new MemoryStream());
        _setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        _setting.SetupGet(x => x.LineReadCount).Returns(1);
        using var reader = new FlatFileReader(_logger.Object, _setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        result.Should().BeEmpty().And.BeAssignableTo<IEnumerable<string>>();

        _setting.Verify(x => x.GetStreamReader(), Times.Once);
        _setting.VerifyGet(x => x.LineReadCount, Times.Never);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return single line.
    /// </summary>
    [Fact(DisplayName = "Should return single line when read file has single line")]
    public void ShouldReturnSingleLineWhenFileHasSingleLine()
    {
        // Arrange
        var line = "012345"u8.ToArray();
        using var fileReader = new StreamReader(new MemoryStream(line));
        _setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        _setting.SetupGet(x => x.LineReadCount).Returns(1);
        using var reader = new FlatFileReader(_logger.Object, _setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        result.Should().Equal("012345").And.BeAssignableTo<IEnumerable<string>>();

        _setting.Verify(x => x.GetStreamReader(), Times.Once);
        _setting.VerifyGet(x => x.LineReadCount);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReader.Read"/>.
    /// It is expected to return multiple line.
    /// </summary>
    [Fact(DisplayName = "Should return multiple line when line read count is 2")]
    public void ShouldReturnMultiLine()
    {
        // Arrange
        var line = Encoding.UTF8.GetBytes($"012345{Environment.NewLine}98765");
        using var fileReader = new StreamReader(new MemoryStream(line));
        _setting.Setup(x => x.GetStreamReader()).Returns(fileReader);
        _setting.SetupGet(x => x.LineReadCount).Returns(2);
        using var reader = new FlatFileReader(_logger.Object, _setting.Object);

        // Act
        var result = reader.Read();

        // Assert
        result.Should().Equal("012345", "98765").And.BeAssignableTo<IEnumerable<string>>();

        _setting.Verify(x => x.GetStreamReader(), Times.Once);
        _setting.VerifyGet(x => x.LineReadCount);
    }
}