namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// Provides functionality for retrieving dynamically provided values used by <see cref="SearchQueryRule{T}"/> elements.
  /// </summary>
  public interface ISearchQueryValueProvider
  {
    /// <summary>
    /// Returns the value needed by the search query rule, 
    /// which can either be a default or dynamically provided value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchQueryRule">The specified search query rule.</param>
    /// <returns>A typed value.</returns>
    object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new();
  }
}