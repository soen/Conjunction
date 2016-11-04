using System.Collections.Generic;
using Conjunction.Foundation.Core.Model.Processing;

namespace Conjunction.Foundation.Core.Model
{
  public class SearchQueryGrouping<T> : ISearchQueryElement<T> where T : IndexableEntity, new()
  {
    public SearchQueryGrouping(LogicalOperator logicalOperator) : this(logicalOperator, new List<ISearchQueryElement<T>>())
    {
    }

    public SearchQueryGrouping(LogicalOperator logicalOperator, ICollection<ISearchQueryElement<T>> searchQueryElements)
    {
      LogicalOperator = logicalOperator;
      SearchQueryElements = searchQueryElements;
    }

    public LogicalOperator LogicalOperator { get; }

    public ICollection<ISearchQueryElement<T>> SearchQueryElements { get; }

    public void Accept(ISearchQueryElementVisitor<T> visitor)
    {
      visitor.VisitSearchQueryGroupingBegin(this);

      foreach (var searchQueryElement in SearchQueryElements)
        searchQueryElement.Accept(visitor);

      visitor.VisitSearchQueryGroupingEnd();
    }
  }
}