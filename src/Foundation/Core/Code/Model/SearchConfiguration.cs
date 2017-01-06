using Conjunction.Foundation.Core.Model.Providers;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchConfiguration
  {
    public SearchConfiguration(ISearchQueryElementProvider searchQueryElementProvider,
      ISearchQueryValueProvider searchQueryValueProvider,
      IIndexNameProvider indexNameProvider)
    {
      SearchQueryElementProvider = searchQueryElementProvider;
      SearchQueryValueProvider = searchQueryValueProvider;
      IndexNameProvider = indexNameProvider;
    }

    public ISearchQueryElementProvider SearchQueryElementProvider { get; }

    public ISearchQueryValueProvider SearchQueryValueProvider { get; }

    public IIndexNameProvider IndexNameProvider { get; }
  }
}