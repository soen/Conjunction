using System;
using System.Collections.Generic;
using Conjunction.Core.Model.Processing;

namespace Conjunction.Core.Model
{
  /// <summary>
  /// Represents a search query grouping that defines a group of one or more 
  /// search query elements, and their logical relationship to each other.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="Conjunction.Sitecore.Model.IndexableEntity"/> implementation to use.</typeparam>
  public class SearchQueryGrouping<T> : ISearchQueryElement<T> where T : IIndexableEntity, new()
  {
    public SearchQueryGrouping(LogicalOperator logicalOperator) : this(logicalOperator, new List<ISearchQueryElement<T>>())
    {
    }

    public SearchQueryGrouping(LogicalOperator logicalOperator, ICollection<ISearchQueryElement<T>> searchQueryElements)
    {
	    if (searchQueryElements == null) throw new ArgumentNullException(nameof(searchQueryElements));

	    LogicalOperator = logicalOperator;
      SearchQueryElements = searchQueryElements;
    }

    /// <summary>
    /// Gets the <see cref="LogicalOperator"/> that is associated with the given search query grouping.
    /// </summary>
    public LogicalOperator LogicalOperator { get; }

    /// <summary>
    /// Gets the search query elements children.
    /// </summary>
    public ICollection<ISearchQueryElement<T>> SearchQueryElements { get; }

    public void Accept(ISearchQueryElementVisitor<T> visitor)
    {
	    if (visitor == null) throw new ArgumentNullException(nameof(visitor));

	    visitor.VisitSearchQueryGroupingBegin(this);

      foreach (var searchQueryElement in SearchQueryElements)
        searchQueryElement.Accept(visitor);

      visitor.VisitSearchQueryGroupingEnd();
    }
  }
}