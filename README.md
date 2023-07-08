# BatchSharp

[![Nuget](https://img.shields.io/nuget/v/BatchSharp)](https://www.nuget.org/packages/BatchSharp)
[![Nuget](https://img.shields.io/nuget/dt/BatchSharp)](https://www.nuget.org/packages/BatchSharp)
[![CI](https://github.com/kuju63/BatchSharp/actions/workflows/ci.yml/badge.svg)](https://github.com/kuju63/BatchSharp/actions/workflows/ci.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/c0652607bd5a4dffafd2ffa954ac7d54)](https://app.codacy.com/gh/kuju63/BatchSharp/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/c0652607bd5a4dffafd2ffa954ac7d54)](https://app.codacy.com/gh/kuju63/BatchSharp/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage)

BatchSharp is a simple framework for batch application.
This is inspired from Spring Batch.

## Getting Started

BatchSharp can be installed using the Nuget package manager or the `dotnet` CLI.

- Package Manager

    ```powershell
    Install-Package BatchSharp
    ```

- dotnet CLI

    ```bash
    dotnet add package BatchSharp
    ```

## Using BatchSharp

1. Create the main class

    ```csharp
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
    ```

2. Add the batch application endpoint

    Configure batch application input and output type.

    Add the class of application endpoint that extend `DefaultBatchApplication` class. First type parameter is input type, second type parameter is output type.

    `IReader` is used to read input data. `IProcessor` is used to process input data. `IWriter` is used to write output data. These class are injected by DI container.

    ```csharp
    /// <summary>
    /// Class of example batch application.
    /// </summary>
    public class ExampleBatchApplication : DefaultBatchApplication<string, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleBatchApplication"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="reader">Reader.</param>
        /// <param name="processor">Processor.</param>
        /// <param name="writer">Writer.</param>
        public ExampleBatchApplication(
            ILogger<ExampleBatchApplication> logger,
            IReader<string> reader,
            IProcessor<string, int> processor,
            IWriter<int> writer)
            : base(logger, reader, processor, writer)
        {
        }
    }
    ```
