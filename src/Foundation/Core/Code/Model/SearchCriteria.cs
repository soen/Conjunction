using Conjunction.Foundation.Core.Model.Repositories;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the search criteria used by <see cref="ISearchResultRepository{T}"/> implementations
  /// </summary>
  public class SearchCriteria
  {
    public SearchCriteria(string searchPath = Constants.SearchOptions.DefaultSearchPath)
    {
      Assert.ArgumentNotNullOrEmpty(searchPath, "searchPath");

      SearchPath = searchPath;
    }

    /// <summary>
    /// Gets the search path used to constraint where to look for documents in the search index.
    /// </summary>
    public string SearchPath { get; }
  }
}