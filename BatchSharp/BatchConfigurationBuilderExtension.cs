using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BatchSharp;

/// <summary>
/// Extension class for <see cref="IConfigurationBuilder">IConfigurationBuilder</see>.
/// </summary>
public static class BatchConfigurationBuilderExtension
{
    /// <summary>
    /// Add batch configuration to the configuration builder.
    /// </summary>
    /// <param name="builder">Configuration Builder.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddBatchConfiguration(
        this IConfigurationBuilder builder)
    {
        return Configure(
            builder,
            "Prod",
            Directory.GetCurrentDirectory(),
            "BATCH_",
            "batchApplication");
    }

    /// <summary>
    /// Add batch configuration to the configuration builder.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="environment">Host environment context.</param>
    /// <param name="basePath">Base path.</param>
    /// <param name="prefix">Prefix for environment variables.</param>
    /// <param name="jsonFilePrefix">Prefix for json file of configuration.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddBatchConfiguration(
        this IConfigurationBuilder builder,
        IHostEnvironment environment,
        string? basePath = null,
        string prefix = "BATCH_",
        string jsonFilePrefix = "batchApplication")
    {
        return Configure(
            builder,
            environment.EnvironmentName,
            basePath ?? Directory.GetCurrentDirectory(),
            prefix,
            jsonFilePrefix);
    }

    private static IConfigurationBuilder Configure(
        IConfigurationBuilder builder,
        string environmentName,
        string basePath,
        string prefix,
        string jsonFilePrefix)
    {
        builder.SetBasePath(basePath);
        builder.AddEnvironmentVariables(prefix);
        builder.AddJsonFile(
            $"{jsonFilePrefix}.json",
            optional: true,
            reloadOnChange: true);
        builder.AddJsonFile(
            $"{jsonFilePrefix}.{environmentName}.json",
            optional: true,
            reloadOnChange: true);
        return builder;
    }
}