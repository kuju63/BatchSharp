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
        Assert.Equal(1, setting.LineReadCount);
        Assert.Equal(Encoding.UTF8, setting.FileEncoding);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor with line read count.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with optional params.)")]
    public void ShouldInitializeNewInstanceWithLineReadCount()
    {
        var setting = new FlatFileReaderSetting("file.txt", 100);
        Assert.Equal(100, setting.LineReadCount);
        Assert.Equal(Encoding.UTF8, setting.FileEncoding);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor with file encoding.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with file encoding.)")]
    public void ShouldInitializeNewInstanceWithFileEncoding()
    {
        var setting = new FlatFileReaderSetting("file.txt", Encoding.ASCII);
        Assert.Equal(1, setting.LineReadCount);
        Assert.Equal(Encoding.ASCII, setting.FileEncoding);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting"/> constructor.
    /// Should succeed to initialize instance with optional params.
    /// </summary>
    [Fact(DisplayName = "Should succeed to initialize FlatFileReaderSetting instance with optional params.)")]
    public void ShouldInitializeSucceedWithOptionalParams()
    {
        var setting = new FlatFileReaderSetting("file.txt", 100, Encoding.ASCII);
        Assert.Equal(100, setting.LineReadCount);
        Assert.Equal(Encoding.ASCII, setting.FileEncoding);
    }

    /// <summary>
    /// Test for <see cref="FlatFileReaderSetting.GetStreamReader"/>.
    /// Expected to throw <see cref="FileNotFoundException"/>.
    /// </summary>
    [Fact(DisplayName = "Should thrown FileNotFoundException when file does not exist.")]
    public void ShouldThrowFileNotFoundException()
    {
        var setting = new FlatFileReaderSetting("file.txt");
        Assert.Throws<FileNotFoundException>(() => setting.GetStreamReader());
    }
}