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
            "application");
    }

    /// <summary>
    /// Add batch configuration to the configuration builder.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="environmentName">Environment name.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddBatchConfiguration(
        this IConfigurationBuilder builder,
        string environmentName)
    {
        return Configure(
            builder,
            environmentName,
            Directory.GetCurrentDirectory(),
            "BATCH_",
            "application");
    }

    /// <summary>
    /// Add batch configuration to the configuration builder.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="environmentName">Environment name.</param>
    /// <param name="environmentVariablePrefix">Prefix of environment variables for configuration.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddBatchConfiguration(
        this IConfigurationBuilder builder,
        string environmentName,
        string environmentVariablePrefix)
    {
        return Configure(
            builder,
            environmentName,
            Directory.GetCurrentDirectory(),
            environmentVariablePrefix,
            "application");
    }

    /// <summary>
    /// Add batch configuration to the configuration builder.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="environment">Host environment context.</param>
    /// <param name="environmentVariablePrefix">Prefix of environment variables for configuration.</param>
    /// <param name="configFilePrefix">Prefix of JSON file name.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddBatchConfiguration(
        this IConfigurationBuilder builder,
        IHostEnvironment environment,
        string environmentVariablePrefix,
        string configFilePrefix)
    {
        return Configure(
            builder,
            environment.EnvironmentName,
            Directory.GetCurrentDirectory(),
            environmentVariablePrefix,
            configFilePrefix);
    }

    private static IConfigurationBuilder Configure(
        IConfigurationBuilder builder,
        string environmentName,
        string basePath,
        string environmentVariablePrefix,
        string jsonFilePrefix)
    {
        builder.SetBasePath(basePath);
        builder.AddEnvironmentVariables(environmentVariablePrefix);
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