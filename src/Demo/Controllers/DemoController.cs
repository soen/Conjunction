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
      Item searchQueryConfigurationItem = Sitecore.Context.Database.GetItem("{D6A82E1F-6687-495E-8243-D865B1BF28BA}");

      var criteria = new SearchCriteria<MyClass>(new SitecoreConfiguredSearchQueryElementProvider(searchQueryConfigurationItem),
                                                 new QueryStringSearchQueryValueProvider(Request.QueryString),
                                                 new SitecoreDefaultIndexNameProvider());

      var searchResult = _searchResultRepository.GetSearchResult(criteria);

      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);

      return View(viewModel);
    }
  }
}