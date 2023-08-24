using BatchSharp.Step;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchSharp;

/// <summary>
/// Represents a extension for batch configuration.
/// </summary>
public static class BatchHost
{
    /// <summary>
    /// Configures batch job.
    /// </summary>
    /// <param name="hostBuilder">Host builder.</param>
    /// <param name="configure">Function for configure job.</param>
    /// <returns>Host builder.</returns>
    public static IHostBuilder ConfigureBatch(this IHostBuilder hostBuilder, Func<StepBuilder, StepCollection> configure)
    {
        hostBuilder.ConfigureServices((context, services) => services.AddSingleton(sp =>
        {
            var builder = new StepBuilder(sp);
            return configure(builder);
        }));
        return hostBuilder;
    }
}