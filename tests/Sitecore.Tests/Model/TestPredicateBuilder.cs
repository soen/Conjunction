using System;
using System.Linq.Expressions;
using Conjunction.Core.Model;
using Conjunction.Core.Model.Providers.SearchQueryValue;
using Conjunction.Sitecore.Model;
using Conjunction.Sitecore.Model.Processing;

namespace Conjunction.Sitecore.Tests.Model
{
  public class TestPredicateBuilder<T> : ISearchQueryPredicateBuilder<T> where T : IndexableEntity, new()
  {
    public TestPredicateBuilder(ISearchQueryValueProvider searchQueryValueProvider)
    {
      SearchQueryValueProvider = searchQueryValueProvider;
    }

    public void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping)
    {
      throw new NotImplementedException();
    }

    public void VisitSearchQueryGroupingEnd()
    {
      throw new NotImplementedException();
    }

    public void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule)
    {
      throw new NotImplementedException();
    }

    public ISearchQueryValueProvider SearchQueryValueProvider { get; }

    public Expression<Func<T, bool>> GetOutput()
    {
      throw new NotImplementedException();
    }
  }
}
