using System.Text;

using BatchSharp.Reader;

namespace BatchSharp.Tests.Reader;

/// <summary>
/// Test class for <see cref="FlatFileReaderSetting"/>.
/// </summary>
public class FlatFileReaderSettingTest
{
    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor.
    /// Should succeed to initialize instance.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance.")]
    public void ShouldInitializeSucceed()
    {
        var setting = new FlatFileReaderSetting("file.txt");

        setting.LineReadCount.Should().Be(1);
        setting.FileEncoding.Should().Be(Encoding.UTF8);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor with line read count.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with optional params.)")]
    public void ShouldInitializeNewInstanceWithLineReadCount()
    {
        var setting = new FlatFileReaderSetting("file.txt", 100);

        setting.LineReadCount.Should().Be(100);
        setting.FileEncoding.Should().Be(Encoding.UTF8);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor with file encoding.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with file encoding.)")]
    public void ShouldInitializeNewInstanceWithFileEncoding()
    {
        var setting = new FlatFileReaderSetting("file.txt", Encoding.ASCII);

        setting.LineReadCount.Should().Be(1);
        setting.FileEncoding.Should().Be(Encoding.ASCII);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor.
    /// Should succeed to initialize instance with optional params.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with optional params.)")]
    public void ShouldInitializeSucceedWithOptionalParams()
    {
        var setting = new FlatFileReaderSetting("file.txt", 100, Encoding.ASCII);
        setting.LineReadCount.Should().Be(100);
        setting.FileEncoding.Should().Be(Encoding.ASCII);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting.GetStreamReader"/>.
    /// Expected to throw <see cref="FileNotFoundException"/>.
    /// </summary>
    [Fact(DisplayName = "Should thrown FileNotFoundException when file does not exist.")]
    public void ShouldThrowFileNotFoundException()
    {
        var setting = new FlatFileReaderSetting("file.txt");
        setting.Invoking(x => x.GetStreamReader()).Should().Throw<FileNotFoundException>();
    }
}