using BatchSharp;
using BatchSharp.Example;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddBatchConfiguration();
    })
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<BatchHostedService>()
            .AddScoped<IBatchApplication, ExampleBatchApplication>();
    });

var app = builder.Build();
await app.RunAsync();