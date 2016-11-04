using System.Collections.Generic;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchResult<T>
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