using System.Collections.Generic;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the search result returned from querying a specific <see cref="SearchCriteria"/> 
  /// using the <see cref="Repositories.SearchResultRepository{T}"/>.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public class SearchResult<T> where T : IndexableEntity, new()
  {
    public SearchResult(int totalSearchResults, IEnumerable<T> hits)
    {
      TotalSearchResults = totalSearchResults;
      Hits = hits;
    }

    /// <summary>
    /// Gets the total number of search results found.
    /// </summary>
    public int TotalSearchResults { get; }

    /// <summary>
    /// Gets the hits found.
    /// </summary>
    public IEnumerable<T> Hits { get; }
  }
}