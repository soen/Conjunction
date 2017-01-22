using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchCriteria
  {
    public SearchCriteria(string searchPath = Constants.SearchOptions.DefaultSearchPath)
    {
      Assert.ArgumentNotNullOrEmpty(searchPath, "searchPath");

      SearchPath = searchPath;
    }

    public string SearchPath { get; }
  }
}