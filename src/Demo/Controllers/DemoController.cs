using System.Web.Mvc;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
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
      // == Constructing the SearchResultRepository instance ==

      //// You can choose to either use the builder, where you specify the element- and value providers:

      //var builder = new SearchResultRepositoryBuilder<T>();

      //var searchResultRepository = builder.Create(
      //  new SitecoreSearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item),
      //  new NameValuePairSearchQueryValueProvider(() => Request.QueryString)
      //);

      //return searchResultRepository;

      // Note: By default, the builder uses the built-in predicate builder implemenation. 
      // If you want to swap out default predicate-builder, you can do so like this: 
      //.WithPredicateBuilder(new DefaultSearchQueryPredicateBuilder<T>(new NameValuePairSearchQueryValueProvider(() => Request.QueryString)))

      // Note: By default, Conjunction uses Sitecore's master/web index. You should create your own indexNameProvider, and
      //       swap the default one out, like so:
      //.WithIndexNameProvider(new CustomDomainIndexNameProvider());

      // Or you can simply compose the objects together yourself:

      return new SearchResultRepository<T>(
          new SitecoreSearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item),
          new DefaultSitecoreIndexNameProvider(),
          new DefaultSearchQueryPredicateBuilder<T>(new NameValuePairSearchQueryValueProvider(() => Request.QueryString))
      );
    }

    public ActionResult Index()
    {
      var searchResult = _searchResultRepository.GetSearchResult(new SearchCriteria());

      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);
      return View(viewModel);
    }
  }
}