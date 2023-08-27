namespace BatchSharp.Step;

/// <summary>
/// Represents a collection of <see cref="IStep"/>.
/// </summary>
public class StepCollection : List<IStep>, IReadOnlyList<IStep>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StepCollection"/> class.
    /// </summary>
    public StepCollection()
        : this(Enumerable.Empty<IStep>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StepCollection"/> class with initial collection.
    /// </summary>
    /// <param name="steps">Initial step collections.</param>
    public StepCollection(IEnumerable<IStep> steps)
        : base(steps)
    {
    }

    /// <summary>
    /// Gets a value indicating whether the collection is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            return Count == 0;
        }
    }
}