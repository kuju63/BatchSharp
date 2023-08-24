using BatchSharp.Step;

namespace BatchSharp;

/// <summary>
/// Represents a builder for batch job.
/// </summary>
public class StepBuilder
{
    private readonly IServiceProvider _services;

    private readonly StepCollection _steps = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="StepBuilder"/> class.
    /// </summary>
    /// <param name="services">Dependency service provider.</param>
    public StepBuilder(IServiceProvider services)
    {
        _services = services;
    }

    /// <summary>
    /// Adds a step to the batch job.
    /// </summary>
    /// <param name="stepFactory">Factory of creating step.</param>
    /// <returns>Instance of <see cref="StepBuilder"/>.</returns>
    public StepBuilder AddStep(Func<IServiceProvider, IStep> stepFactory)
    {
        _steps.Add(stepFactory(_services));
        return this;
    }

    /// <summary>
    /// Builds the batch job.
    /// </summary>
    /// <returns>Steps.</returns>
    public StepCollection Build()
    {
        return _steps;
    }
}