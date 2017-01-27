using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;

namespace Conjunction.Foundation.Core.Model.Repositories
{
  /// <summary>
  /// Provides functionality to retrievin a <see cref="SearchResult{T}"/> from a given <see cref="SearchCriteria"/>.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public interface ISearchResultRepository<T> where T : IndexableEntity, new()
  {
    /// <summary>
    /// Gets the <see cref="ISearchQueryElementProvider"/> that is associated with the given search result repository.
    /// </summary>
    ISearchQueryElementProvider SearchQueryElementProvider { get; }

    /// <summary>
    /// Gets the <see cref="IIndexNameProvider"/> that is associated with the given search result repository.
    /// </summary>
    IIndexNameProvider IndexNameProvider { get; }

    /// <summary>
    /// Gets the <see cref="ISearchQueryPredicateBuilder{T}"/> that is associated with the given search result repository.
    /// </summary>
    ISearchQueryPredicateBuilder<T> SearchQueryPredicateBuilder { get; }

    /// <summary>
    /// Performs a query using the provided <paramref name="searchCriteria"/> to retrieve a <see cref="SearchResult{T}"/>.
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <returns>A <see cref="SearchResult{T}"/> containing the search result</returns>
    SearchResult<T> GetSearchResult(SearchCriteria searchCriteria);
  }
}