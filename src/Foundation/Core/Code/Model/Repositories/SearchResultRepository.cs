using System;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Processing.Processors;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Repositories
{
  public class SearchResultRepository
  {
    public SearchResult<T> GetSearchResult<T>(SearchCriteria<T> searchCriteria) 
      where T : IndexableEntity, new()
    {
      SearchResult<T> retVal;

      try
      {
        using (var context = ContentSearchManager.GetIndex(searchCriteria.IndexName).CreateSearchContext())
        {
          var predicate = PredicateBuilder.True<T>();
          predicate = predicate.And(GetPredicateFromSearchQueryCriteria(searchCriteria));

          var queryable = context.GetQueryable<T>()
                                 .IsContentItem()
                                 .IsContextLanguage()
                                 .IsLatestVersion()
                                 .Filter(predicate);

          // TODO: Implement sorting and paging (as part of the SearchQueryCriteria<> class)

          var searchResults = queryable.GetResults();

          // TODO: Implement support for facets
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

    private static Expression<Func<T, bool>> GetPredicateFromSearchQueryCriteria<T>(SearchCriteria<T> searchCriteria)
      where T : IndexableEntity, new()
    {
      var searchQueryPredicateBuilder = new SearchQueryPredicateBuilder<T>(searchCriteria.SearchQueryValueProvider);      
      searchCriteria.SearchQueryElementRoot.Accept(searchQueryPredicateBuilder);

      Expression<Func<T, bool>> searchQueryPredicate = searchQueryPredicateBuilder.GetPredicate();
      Expression<Func<T, bool>> searchPathConstraint = x => x.Path.StartsWith(searchCriteria.SearchPath.ToLower());
      
      return searchQueryPredicate.And(searchPathConstraint);
    }
  }
}