using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;

namespace Conjunction.Foundation.Core.Model.Processing
{
  /// <summary>
  /// Represents a specialized visitor that can build up a predicate of type <see cref="Expression{T}" /> 
  /// of <see cref="Func{T, Boolean}" /> from search query elements.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public interface ISearchQueryPredicateBuilder<T> : ISearchQueryElementVisitor<T> where T : IndexableEntity, new()
  {
    ISearchQueryValueProvider SearchQueryValueProvider { get; }
    
    Expression<Func<T, bool>> GetOutput();
  }
}