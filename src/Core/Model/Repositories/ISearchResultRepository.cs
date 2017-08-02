using Conjunction.Core.Model.Processing;
using Conjunction.Core.Model.Providers.Indexing;
using Conjunction.Core.Model.Providers.SearchQueryElement;

namespace Conjunction.Core.Model.Repositories
{
  /// <summary>
  /// Provides functionality to retrievin a <see cref="SearchResult{T}"/> from a given <see cref="SearchParameters"/>.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
  public interface ISearchResultRepository<T> where T : IIndexableEntity, new()
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
    /// Gets the <see cref="Processing.ISearchQueryElementVisitor{T}"/> that is associated with the given search result repository.
    /// </summary>
    ISearchQueryElementVisitor<T> SearchQueryElementVisitor { get; }

    /// <summary>
    /// Performs a query using the provided <paramref name="searchParameters"/> to retrieve a <see cref="SearchResult{T}"/>.
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns>A <see cref="SearchResult{T}"/> containing the search result</returns>
    SearchResult<T> GetSearchResult(SearchParameters searchParameters);
  }
}