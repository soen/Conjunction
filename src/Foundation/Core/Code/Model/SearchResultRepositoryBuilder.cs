using System;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using Conjunction.Foundation.Core.Model.Repositories;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchResultRepositoryBuilder<T> where T : IndexableEntity, new()
  {
    private readonly ISearchQueryElementProvider _searchQueryElementProvider;
    private readonly IIndexNameProvider _indexNameProvider;
    private readonly ISearchQueryValueProvider _searchQueryValueProvider;
    private readonly ISearchQueryPredicateBuilder<T> _searchQueryPredicateBuilder;

    public SearchResultRepositoryBuilder()
    {
    }

    private SearchResultRepositoryBuilder(ISearchQueryElementProvider searchQueryElementProvider,
                                          IIndexNameProvider indexNameProvider,
                                          ISearchQueryValueProvider searchQueryValueProvider,
                                          ISearchQueryPredicateBuilder<T> searchQueryPredicateBuilder)
    {
      _searchQueryElementProvider = searchQueryElementProvider;
      _indexNameProvider = indexNameProvider;
      _searchQueryValueProvider = searchQueryValueProvider;
      _searchQueryPredicateBuilder = searchQueryPredicateBuilder;
    }

    public SearchResultRepositoryBuilder<T> WithElementProvider(ISearchQueryElementProvider searchQueryElementProvider)
    {
      return new SearchResultRepositoryBuilder<T>(
        searchQueryElementProvider, 
        _indexNameProvider, 
        _searchQueryValueProvider,
        _searchQueryPredicateBuilder
      );
    }

    public SearchResultRepositoryBuilder<T> WithIndexNameProvider(IIndexNameProvider indexNameProvider)
    {
      return new SearchResultRepositoryBuilder<T>(
        _searchQueryElementProvider, 
        indexNameProvider,
        _searchQueryValueProvider, 
        _searchQueryPredicateBuilder
      );
    }

    public SearchResultRepositoryBuilder<T> WithValueProvider(ISearchQueryValueProvider searchQueryValueProvider)
    {
      return new SearchResultRepositoryBuilder<T>(
        _searchQueryElementProvider, 
        _indexNameProvider, 
        searchQueryValueProvider,
        _searchQueryPredicateBuilder
      );
    }

    public SearchResultRepositoryBuilder<T> WithPredicateBuilder(ISearchQueryPredicateBuilder<T> searchQueryPredicateBuilder)
    {
      return new SearchResultRepositoryBuilder<T>(
        _searchQueryElementProvider, 
        _indexNameProvider,
        _searchQueryValueProvider, 
        searchQueryPredicateBuilder
      );
    }

    public ISearchResultRepository<T> Create()
    {
      Assert.IsNotNull(_searchQueryElementProvider, GetInitializedErrorMessageFor(typeof(ISearchQueryElementProvider)));
      Assert.IsNotNull(_searchQueryValueProvider, GetInitializedErrorMessageFor(typeof(ISearchQueryValueProvider)));

      var indexNameProvider = GetConfiguredOrDefaultSitecoreIndexProvider();
      var searchQueryPredicateBuilder = GetConfiguredOrDefaultSearchQueryPredicateBuilder();

      return new SearchResultRepository<T>(_searchQueryElementProvider, indexNameProvider, searchQueryPredicateBuilder);
    }

    private string GetInitializedErrorMessageFor(Type type)
    {
      return $"The {type.FullName} has not been configured. " +
             $"Please ensure that you have configured the {type.FullName} before calling the SearchResultRepositoryBuilder<T>.Create() method.";
    }

    private IIndexNameProvider GetConfiguredOrDefaultSitecoreIndexProvider()
    {
      return _indexNameProvider ?? new DefaultSitecoreIndexNameProvider();
    }

    private ISearchQueryPredicateBuilder<T> GetConfiguredOrDefaultSearchQueryPredicateBuilder()
    {
      return _searchQueryPredicateBuilder ?? new DefaultSearchQueryPredicateBuilder<T>(_searchQueryValueProvider);
    }
  }
}