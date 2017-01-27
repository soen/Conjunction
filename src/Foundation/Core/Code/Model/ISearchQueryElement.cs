using Conjunction.Foundation.Core.Model.Processing;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// The base abstraction for all search query elements.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public interface ISearchQueryElement<T> where T : IndexableEntity, new()
  {
    /// <summary>
    /// Dispatches to the specific visit method for this search query element type. 
    /// For example, <see cref="SearchQueryRule{T}" /> will call into
    /// <see cref="ISearchQueryElementVisitor{T}.VisitSearchQueryRule" />.
    /// </summary>
    /// <param name="visitor">The visitor to visit this search query element with.</param>
    void Accept(ISearchQueryElementVisitor<T> visitor);
  }
}