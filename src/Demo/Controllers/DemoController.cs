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
    private readonly SearchResultRepository _searchResultRepository;

    public DemoController()
    {
      _searchResultRepository = new SearchResultRepository();
    }

    public ActionResult Index()
    {
      // Get the search query elements from the Sitecore configuration item. This can be either a specific item
      // or the data source item provided to this controller rendering. In this case, we simply use a specific
      // item that is fetched using its ID.
      Item searchQueryConfigurationItem = Sitecore.Context.Database.GetItem("{D6A82E1F-6687-495E-8243-D865B1BF28BA}");

      // Setup the search criteria using the default Sitecore configured search query element provider.
      // Moreover, if we encounter search query rules that needs dynamically provided values, these will be
      // provided by the default query string value provider. Lastly we specify that we want to retrieve
      // results from the default Sitecore master/web index, using the default Sitecore index name provider.
      var criteria = new SearchCriteria<MyClass>(new SitecoreConfiguredSearchQueryElementProvider(searchQueryConfigurationItem),
                                                 new QueryStringSearchQueryValueProvider(Request.QueryString),
                                                 new SitecoreDefaultIndexNameProvider());

      // Retrieve the search result using the SearchReRepository
      var searchResult = _searchResultRepository.GetSearchResult(criteria);

      // And finally pass the search result data along to the ViewModel
      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);

      return View(viewModel);
    }
  }
}