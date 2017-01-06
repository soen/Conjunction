using System.Collections.Specialized;
using System.Web.Mvc;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using Conjunction.Foundation.Core.Model.Repositories;
using Demo.Model;
using Demo.ViewModels;
using Sitecore.Data.Items;

namespace Demo.Controllers
{
  public class DemoController : Controller
  {
    private readonly ISearchResultRepository<MyClass> _searchResultRepository;

    public DemoController()
    {
      // Setup the search criteria using the default Sitecore configured search query element provider.
      // Moreover, if we encounter search query rules that needs dynamically provided values, these will be
      // provided by the default query string value provider. Lastly we specify that we want to retrieve
      // results from the default Sitecore master/web index, using the default Sitecore index name provider.
      var configuration = new SearchConfiguration(new SitecoreConfiguredSearchQueryElementProvider(GetSearchQueryRootItem),
                                                  new QueryStringSearchQueryValueProvider(GetNameValuePairs),
                                                  new SitecoreDefaultIndexNameProvider());

      // Here we make use of a custom caching decorator
      _searchResultRepository = new SearchResultRepository<MyClass>(configuration);
    }

    public ActionResult Index()
    {
      // Retrieve the search result using the SearchResultRepository
      var searchResult = _searchResultRepository.GetSearchResult(new SearchCriteria());

      // And finally pass the search result data along to the ViewModel
      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);

      return View(viewModel);
    }

    private Item GetSearchQueryRootItem()
    {
      // Get the search query elements from the Sitecore configuration item. This can be either a specific item
      // or the data source item provided to this controller rendering. In this case, we simply use a specific
      // item that is fetched using its ID.
      return Sitecore.Context.Database.GetItem("{D6A82E1F-6687-495E-8243-D865B1BF28BA}");
    }

    private NameValueCollection GetNameValuePairs()
    {
      return Request.QueryString;
    }
  }
}