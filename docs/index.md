# BatchSharp

Refer to [Markdown](http://daringfireball.net/projects/markdown/) for how to write markdown files.

## Quick Start Guide

1. Install BatchSharp

    BatchSharp can be installed using the Nuget package manager or the `dotnet` CLI.

    - Package Manager

        ```powershell
        Install-Package BatchSharp
        ```

    - dotnet CLI

        ```bash
        dotnet add package BatchSharp
        ```

2. Create processor class

   ```csharp
   using BatchSharp.Processor;

    namespace BatchSharp.Example.Processor;

    public class ExampleProcessor : IProcessor<string, int>
    {
        // Convert input data to output data
        // This method is called for each input data
        public int Process(string source)
        {
            return source.Length;
        }
    }
   ```

3. Create the main class

    Configure input and output type of batch application.
    And register processor class that convert input data to output data.

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
            // Add BatchSharp configuration
            builder.AddBatchConfiguration();
        })
        .ConfigureServices((_, services) =>
        {
            services.AddHostedService<BatchHostedService>();
            services.AddScoped<IBatchApplication, ExampleBatchApplication>();
            // Register reader setting
            services.AddScoped<IFileReaderSetting, FlatFileReaderSetting>( c => new FlatFileReaderSetting("input.txt"));
            // Register reader class
            services.AddScoped<IReader<string>, FlatFileReader>();
            // Register processor class
            services.AddScoped<IProcessor<string, int>, ExampleProcessor>();
            // Register writer setting
            services.AddScoped<IFileWriterSetting, FileWriterSetting>(c => new FileWriterSetting("output.txt"));
            // Register writer class
            services.AddScoped<IWriter<int>, ExampleWriter>();
            services.AddScoped<IStep, SimpleStep<string, int>>();
            services.AddScoped<IStepState, StepState>();
        });

    var app = builder.Build();
    await app.RunAsync();
    ```

4. Run batch application

    ```bash
    dotnet run
    ```
