namespace Conjunction.Foundation.Core.Model
{
  public class SearchCriteria
  {
    public SearchCriteria(string searchPath = Constants.SearchOptions.DefaultSearchPath)
    {
      SearchPath = searchPath;
    }

    public string SearchPath { get; }
  }
}