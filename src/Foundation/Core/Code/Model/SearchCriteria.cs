using Conjunction.Foundation.Core.Model.Providers;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchCriteria<T> where T : IndexableEntity, new()
  {
    public SearchCriteria(ISearchQueryElementProvider searchQueryElementProvider,
                          ISearchQueryValueProvider searchQueryValueProvider,
                          IIndexNameProvider indexNameProvider,
                          string searchPath = Constants.SearchOptions.DefaultSearchPath)
    {
      SearchQueryElementProvider = searchQueryElementProvider;
      SearchQueryValueProvider = searchQueryValueProvider;
      IndexNameProvider = indexNameProvider;
      SearchPath = searchPath;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider { get; }

    public ISearchQueryValueProvider SearchQueryValueProvider { get; }

    public IIndexNameProvider IndexNameProvider { get; }

    public ISearchQueryElement<T> SearchQueryElementRoot => SearchQueryElementProvider.GetSearchQueryElementRoot<T>();

    public string IndexName => IndexNameProvider.IndexName;

    public string SearchPath { get; }
  }
}