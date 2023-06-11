using System.Text;

using BatchSharp.Writer;

namespace BatchSharp.Tests.Writer;

/// <summary>
/// Test class of <see cref="FileWriterSetting"/>.
/// </summary>
public class FileWriterSettingTest
{
    /// <summary>
    /// Should return writer instance with default encoding.
    /// </summary>
    [Fact(DisplayName = "Should return writer instance with default encoding")]
    public void ShouldReturnWriterWithDefaultEncoding()
    {
        var setting = new FileWriterSetting("sample.txt");
        using var writer = setting.GetWriter();
        Assert.Equal(Encoding.UTF8, writer.Encoding);
    }

    /// <summary>
    /// Should return writer instance with specified encoding.
    /// </summary>
    [Fact(DisplayName = "Should return writer instance with specified encoding")]
    public void ShouldReturnWriterWithSpecifiedEncoding()
    {
        var setting = new FileWriterSetting("sample.txt", Encoding.ASCII);
        using var writer = setting.GetWriter();
        Assert.Equal(Encoding.ASCII, writer.Encoding);
    }
}