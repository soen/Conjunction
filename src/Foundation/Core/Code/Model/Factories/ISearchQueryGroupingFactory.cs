namespace Conjunction.Foundation.Core.Model.Factories
{
  /// <summary>
  /// Provides functionality to build instances of type <see cref="SearchQueryGrouping{T}"/>.
  /// </summary>
  public interface ISearchQueryGroupingFactory
  {
    /// <summary>
    /// Gets the <see cref="ILogicalOperatorFactory"/> that is associated with the given search query grouping factory.
    /// </summary>
    ILogicalOperatorFactory LogicalOperatorFactory { get; }

    /// <summary>
    /// Creates a new <see cref="SearchQueryGrouping{T}"/> based on the logical operator.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="configuredLogicalOperator">The raw logical operator value.</param>
    /// <returns>A search query grouping instance.</returns>
    SearchQueryGrouping<T> Create<T>(string configuredLogicalOperator) where T : IndexableEntity, new();
  }
}