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
    private readonly Type _indexNameProviderType;
    private readonly Type _searchQueryPredicateBuilderType;

    public SearchResultRepositoryBuilder() 
      : this(typeof(SitecoreMasterOrWebIndexNameProvider), typeof(SearchQueryPredicateBuilder<T>))
    {
    }

    private SearchResultRepositoryBuilder(Type indexNameProviderType, Type searchQueryPredicateBuilderType)
    {
      Assert.ArgumentNotNull(indexNameProviderType, "indexNameProviderType");
      Assert.ArgumentNotNull(searchQueryPredicateBuilderType, "searchQueryPredicateBuilderType");

      _indexNameProviderType = indexNameProviderType;
      _searchQueryPredicateBuilderType = searchQueryPredicateBuilderType;
    }

    public SearchResultRepositoryBuilder<T> WithIndexNameProvider<TIndexNameProvider>()
      where TIndexNameProvider : IIndexNameProvider
    {
      return new SearchResultRepositoryBuilder<T>(typeof(TIndexNameProvider), _searchQueryPredicateBuilderType);
    }

    public SearchResultRepositoryBuilder<T> WithPredicateBuilder<TSearchQueryPredicateBuilder>()
      where TSearchQueryPredicateBuilder : ISearchQueryPredicateBuilder<T>
    {
      return new SearchResultRepositoryBuilder<T>(_indexNameProviderType, typeof(TSearchQueryPredicateBuilder));
    }

    public ISearchResultRepository<T> Create(ISearchQueryElementProvider searchQueryElementProvider,
                                             ISearchQueryValueProvider searchQueryValueProvider)
    {
      Assert.ArgumentNotNull(searchQueryElementProvider, "searchQueryElementProvider");
      Assert.ArgumentNotNull(searchQueryValueProvider, "searchQueryValueProvider");

      var indexNameProvider = GetIndexNameProvider();
      var predicateBuilder = GetSearchQueryPredicateBuilder(searchQueryValueProvider);

      return new SearchResultRepository<T>(searchQueryElementProvider, indexNameProvider, predicateBuilder);
    }

    private IIndexNameProvider GetIndexNameProvider()
    {
      var instance = Activator.CreateInstance(_indexNameProviderType);
      return (IIndexNameProvider) instance;
    }

    private ISearchQueryPredicateBuilder<T> GetSearchQueryPredicateBuilder(ISearchQueryValueProvider searchQueryValueProvider)
    {
      var instance = Activator.CreateInstance(_searchQueryPredicateBuilderType, searchQueryValueProvider);
      return (ISearchQueryPredicateBuilder<T>) instance;
    }
  }
}