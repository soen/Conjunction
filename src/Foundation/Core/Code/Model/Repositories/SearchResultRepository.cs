using System;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Repositories
{
  /// <summary>
  /// Represents the main entry point for retrieving a <see cref="SearchResult{T}"/>
  /// from a given <see cref="SearchCriteria"/>.
  /// </summary>
  public class SearchResultRepository<T> : ISearchResultRepository<T> where T : IndexableEntity, new()
  {
    public SearchResultRepository(ISearchQueryElementProvider searchQueryElementProvider, 
                                  IIndexNameProvider indexNameProvider, 
                                  ISearchQueryPredicateBuilder<T> searchQueryPredicateBuilder)
    {
      Assert.ArgumentNotNull(searchQueryElementProvider, "searchQueryElementProvider");
      Assert.ArgumentNotNull(indexNameProvider, "indexNameProvider");
      Assert.ArgumentNotNull(searchQueryPredicateBuilder, "searchQueryPredicateBuilder");

      SearchQueryElementProvider = searchQueryElementProvider;
      IndexNameProvider = indexNameProvider;
      SearchQueryPredicateBuilder = searchQueryPredicateBuilder;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider { get; }

    public IIndexNameProvider IndexNameProvider { get; }

    public ISearchQueryPredicateBuilder<T> SearchQueryPredicateBuilder { get; }

    public ISearchIndex SearchIndex => ContentSearchManager.GetIndex(IndexNameProvider.IndexName);
    
    /// <summary>
    /// Performs a query using the provided <paramref name="searchCriteria"/> to retrieve a <see cref="SearchResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchCriteria"></param>
    /// <returns>A <see cref="SearchResult{T}"/></returns>
    public SearchResult<T> GetSearchResult(SearchCriteria searchCriteria)
    {
      Assert.ArgumentNotNull(searchCriteria, "searchCriteria");

      SearchResult<T> retVal;
      try
      {
        using (var context = SearchIndex.CreateSearchContext())
        {
          var predicate = PredicateBuilder.True<T>();
          predicate = predicate.And(GetPredicateFromSearchQueryCriteria(searchCriteria));

          var queryable = context.GetQueryable<T>()
                                 .IsContentItem()
                                 .IsContextLanguage()
                                 .IsLatestVersion()
                                 .Filter(predicate);

          var searchResults = queryable.GetResults();
          
          retVal = new SearchResult<T>(searchResults.TotalSearchResults,
                                       searchResults.Hits.Select(x => x.Document).ToList());
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex, this);
        throw;
      }
      return retVal;
    }

    private Expression<Func<T, bool>> GetPredicateFromSearchQueryCriteria(SearchCriteria searchCriteria)
    {
      var searchQueryElementRoot = SearchQueryElementProvider.GetSearchQueryElementRoot<T>();
      searchQueryElementRoot.Accept(SearchQueryPredicateBuilder);

      Expression<Func<T, bool>> searchQueryPredicate = SearchQueryPredicateBuilder.GetOutput();
      Expression<Func<T, bool>> searchPathConstraint = x => x.Path.StartsWith(searchCriteria.SearchPath.ToLower());
      
      return searchQueryPredicate.And(searchPathConstraint);
    }
  }
}