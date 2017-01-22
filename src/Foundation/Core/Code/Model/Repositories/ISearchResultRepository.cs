using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;

namespace Conjunction.Foundation.Core.Model.Repositories
{
  public interface ISearchResultRepository<T> where T : IndexableEntity, new()
  {
    ISearchQueryElementProvider SearchQueryElementProvider { get; }

    IIndexNameProvider IndexNameProvider { get; }

    ISearchQueryPredicateBuilder<T> SearchQueryPredicateBuilder { get; }

    SearchResult<T> GetSearchResult(SearchCriteria searchCriteria);
  }
}