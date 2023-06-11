using BatchSharp;
using BatchSharp.Example;
using BatchSharp.Example.Reader;
using BatchSharp.Reader;

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
            .AddScoped<IBatchApplication, ExampleBatchApplication>()
            .AddScoped<IReader<string>, ExampleReader>();
    });

var app = builder.Build();
await app.RunAsync();