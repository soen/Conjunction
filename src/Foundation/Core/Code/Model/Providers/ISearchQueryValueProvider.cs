namespace Conjunction.Foundation.Core.Model.Providers
{
  /// <summary>
  /// Provides functionality for retrieving dynamically provided values used by <see cref="SearchQueryRule{T}"/> elements.
  /// </summary>
  public interface ISearchQueryValueProvider
  {
    object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new();
  }
}