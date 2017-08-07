using System.Web.Mvc;
using Conjunction.Core.Model;
using Conjunction.Core.Model.Providers.SearchQueryValue;
using Conjunction.Core.Model.Repositories;
using Conjunction.Sitecore;
using Conjunction.Sitecore.Model;
using Conjunction.Sitecore.Model.Providers.Indexing;
using Conjunction.Sitecore.Model.Providers.SearchQueryElement;
using Demo.Model;
using Demo.ViewModels;
using Sitecore.Mvc.Presentation;

namespace Demo.Controllers
{
  public class DemoController : Controller
  {
    private readonly ISearchResultRepository<MyClass> _searchResultRepository;

    public DemoController()
    {
      _searchResultRepository = CreateSearchResultRepository<MyClass>();
    }

    private ISearchResultRepository<T> CreateSearchResultRepository<T>()
      where T : IndexableEntity, new()
    {
      var elementProvider = new SearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item);
      var valueProvider = new NameValuePairSearchQueryValueProvider(() => Request.QueryString);

      var builder = new SearchResultRepositoryBuilder<T>()
                       .WithIndexNameProvider<MasterOrWebIndexNameProvider>();

      return builder.Create(elementProvider, valueProvider);
    }

    public ActionResult Index()
    {
	    var searchResult =
		    _searchResultRepository.GetSearchResult(new SearchParameters()
		    {
			    SearchPath = Constants.SearchOptions.DefaultSearchPath
		    });

      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);
      return View(viewModel);
    }
  }
}