using Conjunction.Foundation.Core.Model.Providers;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the search criteria that holds information about the search query elements that
  /// describes what needs to be queried, how values needed by the search query elements can be 
  /// retrived, as well as the search index resposible for delivering the results.
  /// </summary>
  /// <remarks>
  /// The search criteria is used when querying the <see cref="Repositories.SearchResultRepository"/> 
  /// to retrieve a <see cref="SearchResult{T}"/>.
  /// </remarks>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public class SearchCriteria<T> where T : IndexableEntity, new()
  {
    public SearchCriteria(ISearchQueryElementProvider searchQueryElementProvider,
                          ISearchQueryValueProvider searchQueryValueProvider,
                          IIndexNameProvider indexNameProvider,
                          string searchPath = Constants.SearchOptions.DefaultSearchPath)
    {
      SearchQueryElementProvider = searchQueryElementProvider;
      SearchQueryValueProvider = searchQueryValueProvider;
      IndexNameProvider = indexNameProvider;
      SearchPath = searchPath;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider { get; }

    public ISearchQueryValueProvider SearchQueryValueProvider { get; }

    public IIndexNameProvider IndexNameProvider { get; }

    public ISearchQueryElement<T> SearchQueryElementRoot => SearchQueryElementProvider.GetSearchQueryElementRoot<T>();

    public string IndexName => IndexNameProvider.IndexName;

    public string SearchPath { get; }
  }
}