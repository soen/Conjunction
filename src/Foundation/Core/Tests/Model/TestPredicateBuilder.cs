using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;

namespace Conjunction.Foundation.Core.Tests.Model
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
