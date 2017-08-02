namespace Conjunction.Core.Model.Providers.SearchQueryElement
{
  /// <summary>
  /// Provides functionality to retrieve a <see cref="ISearchQueryElement{T}"/> tree  
  /// from a given configuration.
  /// </summary>
  public interface ISearchQueryElementProvider
  {
    /// <summary>
    /// Returns the search query element tree.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
    /// <returns>The search query element hierarchy</returns>
    ISearchQueryElement<T> GetSearchQueryElementTree<T>() where T : IIndexableEntity, new();
  }
}