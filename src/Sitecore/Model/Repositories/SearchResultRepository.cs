using System;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Core;
using Conjunction.Core.Infrastructure.Logging.Logging;
using Conjunction.Core.Model;
using Conjunction.Core.Model.Processing;
using Conjunction.Core.Model.Providers.Indexing;
using Conjunction.Core.Model.Providers.SearchQueryElement;
using Conjunction.Core.Model.Repositories;
using Conjunction.Sitecore.Infrastructure;
using Conjunction.Sitecore.Model.Processing;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;

namespace Conjunction.Sitecore.Model.Repositories
{
  /// <summary>
  /// Represents the main entry point for retrieving a <see cref="SearchResult{T}"/>
  /// from a given <see cref="SearchParameters"/>.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public class SearchResultRepository<T> : ISearchResultRepository<T> where T : IndexableEntity, new()
  {
	  private readonly ILog _logger;
		
		public SearchResultRepository(ISearchQueryElementProvider searchQueryElementProvider, 
                                  IIndexNameProvider indexNameProvider,
																	ISearchQueryPredicateBuilder<T> searchQueryPredicateBuilder)
			: this(searchQueryElementProvider,
						 indexNameProvider, 
						 searchQueryPredicateBuilder, 
						 Locator.Current.GetInstance<ILog>())
    {
    }
		
	  public SearchResultRepository(ISearchQueryElementProvider searchQueryElementProvider, 
                                  IIndexNameProvider indexNameProvider,
																	ISearchQueryPredicateBuilder<T> searchQueryPredicateBuilder,
																	ILog logger)
    {
	    if (searchQueryElementProvider == null)
				throw new ArgumentNullException(nameof(searchQueryElementProvider));
	    if (indexNameProvider == null)
				throw new ArgumentNullException(nameof(indexNameProvider));
	    if (searchQueryPredicateBuilder == null)
				throw new ArgumentNullException(nameof(searchQueryPredicateBuilder));
	    if (logger == null)
				throw new ArgumentNullException(nameof(logger));

	    SearchQueryElementProvider = searchQueryElementProvider;
      IndexNameProvider = indexNameProvider;
			SearchQueryElementVisitor = searchQueryPredicateBuilder;
			_logger = logger;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider { get; }

    public IIndexNameProvider IndexNameProvider { get; }

		public ISearchQueryElementVisitor<T> SearchQueryElementVisitor { get; }

    public ISearchIndex SearchIndex => ContentSearchManager.GetIndex(IndexNameProvider.IndexName);

    public SearchResult<T> GetSearchResult(SearchParameters searchParameters)
    {
	    if (searchParameters == null) throw new ArgumentNullException(nameof(searchParameters));
			
	    SearchResult<T> retVal;
      try
      {
        using (var context = SearchIndex.CreateSearchContext())
        {
          var predicate = PredicateBuilder.True<T>();
          predicate = predicate.And(GetPredicateFromSearchQueryCriteria(searchParameters));

          var queryable = context.GetQueryable<T>()
                                 .IsContentItem()
                                 .IsContextLanguage()
                                 .IsLatestVersion()
                                 .Where(predicate);

          var searchResults = queryable.GetResults();
          
          retVal = new SearchResult<T>(searchResults.TotalSearchResults,
                                       searchResults.Hits.Select(x => x.Document).ToList());
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message, ex, this);
        throw;
      }
      return retVal;
    }

    private Expression<Func<T, bool>> GetPredicateFromSearchQueryCriteria(SearchParameters searchParameters)
    {
      var searchQueryElementTree = SearchQueryElementProvider.GetSearchQueryElementTree<T>();
      searchQueryElementTree.Accept(SearchQueryElementVisitor);

      Expression<Func<T, bool>> searchQueryPredicate = (SearchQueryElementVisitor as ISearchQueryPredicateBuilder<T>)?.GetOutput();
      Expression<Func<T, bool>> searchPathConstraint = x => x.Path.StartsWith(searchParameters.SearchPath.ToLower());
      
      return searchQueryPredicate.And(searchPathConstraint);
    }	  
  }
}