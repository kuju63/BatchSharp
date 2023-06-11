using BatchSharp;
using BatchSharp.Example;
using BatchSharp.Example.Processor;
using BatchSharp.Example.Reader;
using BatchSharp.Example.Writer;
using BatchSharp.Processor;
using BatchSharp.Reader;
using BatchSharp.Writer;

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
        services.AddScoped<IProcessor<string, int>, ExampleProcessor>();
        services.AddScoped<IWriter<int>, ExampleWriter>();
    });

var app = builder.Build();
await app.RunAsync();