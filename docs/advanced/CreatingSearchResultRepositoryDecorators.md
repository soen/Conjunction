# Creating Search Result Repository Decorators

In this section, we'll be looking at how we can create search result repository decorators, thereby allowing behavior to be added to an default implementation, either statically or dynamically, without affecting the behavior of the default implementation.

## Decorating the default implementation with caching

The following example code shows how you can decorate the default search result repository to include caching:

```csharp
  public class CacheSearchResultRepositoryDecorator<T> : ISearchResultRepository<T> 
    where T : IndexableEntity, new()
  {
    private const string CacheItemName = "__CachedSearchResult__";
    private const string int CacheTimeInMinutes = 5;

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

    private T GetSearchResultFromCache<T>(Func<SearchResult<T>> getSearchResult)
    {
      ObjectCache cache = MemoryCache.Default;
      var cachedSearchResult = (T)cache[CacheItemName];
      
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
```