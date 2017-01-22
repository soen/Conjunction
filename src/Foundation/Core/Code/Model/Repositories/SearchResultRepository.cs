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
  // TODO: Rewrite this class the following way:
  // 1) Make it implement an interface that exposes a ISearchIndex property and IProviderSearchContext property
  // 2) Refactor the GetPredicateFromSearchQueryCriteria to use the properties defined in 1)
  // 3) Let the ISearchElementVisitor expose a new method 'GetResult' or something, where it is generically typed what the input and output is.
  // 4) Let this class take in an ISearchElementVisitor interface object, and use that instead of new'ing up the concrete implementation directly.
  // 5) Use Bastard DI such that the default ctor uses the SearchQueryPredicateBuilder unless another ISearchElementVisitor is defined.
  // 6) Create a Decorator component, that allows the client to consume this implementation and make it even more strong, in terms of custom caching and alike...

  /// <summary>
  /// Represents the main entry point for retrieving a <see cref="SearchResult{T}"/>
  /// from a given <see cref="SearchCriteria{T}"/>.
  /// </summary>
  public class SearchResultRepository
  {
    /// <summary>
    /// Performs a query using the provided <paramref name="searchCriteria"/> to retrieve a <see cref="SearchResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchCriteria"></param>
    /// <returns>A <see cref="SearchResult{T}"/></returns>
    public SearchResult<T> GetSearchResult<T>(SearchCriteria<T> searchCriteria) where T : IndexableEntity, new()
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

          // TODO: Implement support for facets
          // TODO: Implement sorting and paging (as part of the SearchQueryCriteria<> class)

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