using Sitecore.ContentSearch;

namespace Conjunction.Foundation.Core.Model.Repositories
{
  public interface ISearchResultRepository<T> where T : IndexableEntity, new()
  {
    ISearchIndex SearchIndex { get; }

    SearchResult<T> GetSearchResult(SearchCriteria searchCriteria);
  }
}