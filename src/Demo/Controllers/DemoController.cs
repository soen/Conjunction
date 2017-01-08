using System.Web.Mvc;
using Conjunction.Foundation.Core.Model;
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

      // You can choose to either use the builder, where you specify the element- and value providers:

      var searchResultRepositoryBuilder = new SearchResultRepositoryBuilder<T>()
        .WithElementProvider(new SitecoreSearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item))
        .WithValueProvider(new NameValuePairSearchQueryValueProvider(() => Request.QueryString));
        
        // Note: If you want to swap out default predicate-builder, you can do so like this: 
        //.WithPredicateBuilder(new DefaultSearchQueryPredicateBuilder<T>(new NameValuePairSearchQueryValueProvider(() => Request.QueryString)))

        // Note: By default, Conjunction uses Sitecore's master/web index. You should create your own indexNameProvider, and
        //       swap the default one out, like so:
        //.WithIndexNameProvider(new CustomDomainIndexNameProvider());
      
      // Once the builder is configured, you can create the searchResultRepository by calling the 'Create()' method:  
      return searchResultRepositoryBuilder.Create();


      // Or you can simply compose the objects together yourself:

      //return new SearchResultRepository<T>(
      //    new SitecoreSearchQueryElementProvider(() => RenderingContext.Current.Rendering.Item),
      //    new DefaultSitecoreIndexNameProvider(),
      //    new DefaultSearchQueryPredicateBuilder<T>(new NameValuePairSearchQueryValueProvider(() => Request.QueryString))
      //);
    }

    public ActionResult Index()
    {
      var searchResult = _searchResultRepository.GetSearchResult(new SearchCriteria());

      var viewModel = new DemoViewModel(searchResult.TotalSearchResults, searchResult.Hits);
      return View(viewModel);
    }
  }
}