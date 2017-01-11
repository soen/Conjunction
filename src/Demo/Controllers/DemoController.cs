using System.Web.Mvc;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using Conjunction.Foundation.Core.Model.Repositories;
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
      var elementProvider = new SitecoreSearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item);
      var valueProvider = new NameValuePairSearchQueryValueProvider(() => Request.QueryString);

      var builder = new SearchResultRepositoryBuilder<T>()
                       .WithIndexNameProvider<DefaultSitecoreIndexNameProvider>();

      return builder.Create(elementProvider, valueProvider);
    }

    public ActionResult Index()
    {
      var searchResult = _searchResultRepository.GetSearchResult(new SearchCriteria());

      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);
      return View(viewModel);
    }
  }
}