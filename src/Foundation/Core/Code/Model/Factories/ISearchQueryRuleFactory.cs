namespace Conjunction.Foundation.Core.Model.Factories
{
  /// <summary>
  /// Provides functionality to build instances of type <see cref="SearchQueryRule{T}"/>.
  /// </summary>
  public interface ISearchQueryRuleFactory
  {
    /// <summary>
    /// Gets the <see cref="IComparisonOperatorFactory"/> that is associated with the given search query rule factory.
    /// </summary>
    IComparisonOperatorFactory ComparisonOperatorFactory { get; }

    /// <summary>
    /// Creates a new <see cref="SearchQueryRule{T}"/> based on the comparison operator.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="associatedPropertyName">The associated property name of <typeparamref name="T"/>.</param>
    /// <param name="configuredComparisonOperator">The configured comparison operator.</param>
    /// <param name="dynamicValueProvidingParameter">The dynamic value providing parameter (optional).</param>
    /// <param name="defaultValue">The default value (optional).</param>
    /// <returns>A search query rule instance.</returns>
    SearchQueryRule<T> Create<T>(string associatedPropertyName, string configuredComparisonOperator, string dynamicValueProvidingParameter = null, string defaultValue = null) where T : IndexableEntity, new();
  }
}