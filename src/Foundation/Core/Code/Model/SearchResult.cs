using System.Collections.Generic;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the search result returned from querying a specific <see cref="SearchCriteria{T}"/> 
  /// using the <see cref="Repositories.SearchResultRepository"/>.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public class SearchResult<T> where T : IndexableEntity, new()
  {
    public SearchResult(int totalSearchResults, IEnumerable<T> hits)
    {
      TotalSearchResults = totalSearchResults;
      Hits = hits;
    }

    public int TotalSearchResults { get; }

    public IEnumerable<T> Hits { get; }
  }
}