using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Repositories;
using Sitecore.ContentSearch;

namespace Demo
{
  /// <summary>
  /// Example implementation of a custom decorator for giving search result repository implementations the option
  /// of using caching.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class CacheSearchResultRepositoryDecorator<T> : ISearchResultRepository<T> where T : IndexableEntity, new()
  {
    private readonly ISearchResultRepository<T> _searchResultRepository;

    public CacheSearchResultRepositoryDecorator(ISearchResultRepository<T> searchResultRepository)
    {
      _searchResultRepository = searchResultRepository;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider => _searchResultRepository.SearchQueryElementProvider;

    public IIndexNameProvider IndexNameProvider => _searchResultRepository.IndexNameProvider;

    public ISearchQueryPredicateBuilder<T> SearchQueryPredicateBuilder => _searchResultRepository.SearchQueryPredicateBuilder;

    public SearchResult<T> GetSearchResult(SearchCriteria searchCriteria)
    {
      // TODO: Add caching...

      return _searchResultRepository.GetSearchResult(searchCriteria);
    }
  }
}