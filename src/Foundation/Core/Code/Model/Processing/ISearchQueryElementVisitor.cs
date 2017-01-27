namespace Conjunction.Foundation.Core.Model.Processing
{
  /// <summary>
  /// Represents a visitor for search query elements.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public interface ISearchQueryElementVisitor<T> where T : IndexableEntity, new()
  {
    /// <summary>
    /// Visits the <see cref="SearchQueryGrouping{T}" /> before it visits its children.
    /// </summary>
    /// <param name="searchQueryGrouping">The search query grouping to visit.</param>
    void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping);

    /// <summary>
    /// Visits the <see cref="SearchQueryGrouping{T}" /> after it has visited its children.
    /// </summary>
    void VisitSearchQueryGroupingEnd();

    /// <summary>
    /// Visits the <see cref="SearchQueryRule{T}" />.
    /// </summary>
    /// <param name="searchQueryRule">The search query rule to visit.</param>    
    void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule);
  }
}