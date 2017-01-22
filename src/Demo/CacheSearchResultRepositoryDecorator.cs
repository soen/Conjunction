using System;
using System.Runtime.Caching;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Repositories;

namespace Demo
{
  /// <summary>
  /// Example implementation of a custom decorator for giving search result repository implementations the option
  /// of using caching.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class CacheSearchResultRepositoryDecorator<T> : ISearchResultRepository<T>
    where T : IndexableEntity, new()
  {
    private const string CacheItemName = "__CachedSearchResult__";
    private const int CacheTimeInMinutes = 5;

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
      Func<SearchResult<T>> getSearchResult = () => _searchResultRepository.GetSearchResult(searchCriteria);
      return GetSearchResultFromCache(getSearchResult);
    }

    private SearchResult<T> GetSearchResultFromCache(Func<SearchResult<T>> getSearchResult)
    {
      ObjectCache cache = MemoryCache.Default;
      var cachedSearchResult = (SearchResult<T>)cache[CacheItemName];

      if (cachedSearchResult == null)
      {
        CacheItemPolicy policy = new CacheItemPolicy();
        policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(CacheTimeInMinutes);
        cachedSearchResult = getSearchResult();
        cache.Set(CacheItemName, cachedSearchResult, policy);
      }

      return cachedSearchResult;
    }
  }
}