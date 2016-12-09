using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using Conjunction.Foundation.Core.Model.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Data;

namespace Conjunction.Foundation.Core.Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      // TODO: The value parameters should be provided by the query string in the URL, here it's hardcoded for testing
      /*var valueProviderParameterNameValuePairs = new NameValueCollection
      {
        {"$x", "My display name"},
        {"$y", "Test name"},
        {"$z", "1"},
        {"$a", Guid.Empty.ToString("N")},
        {"$b", Guid.Empty.ToString("N")},
        {"$c", "1:5"},
      };

      var searchQueryCriteria = 
        new SearchCriteria<MyClass>(new SitecoreConfiguredSearchQueryElementProvider(null),
                                    new QueryStringSearchQueryValueProvider(valueProviderParameterNameValuePairs),
                                    new MockIndexFieldName());

      var searchResultRepository = new SearchResultRepository();
      SearchResult<MyClass> searchResult = searchResultRepository.GetSearchResult(searchQueryCriteria);

      var myClass = new MyClass();
      var x = myClass.Name;*/
    }
  }

  class MyClass : IndexableEntity
  {
    public IEnumerable<ID> TemplateIds { get; set; }

    public int Size { get; set; }

    public bool HasSize { get; set; }
  }

  class MockIndexFieldName : IIndexNameProvider
  {
    public string IndexName => "IndexName";
  }


}
