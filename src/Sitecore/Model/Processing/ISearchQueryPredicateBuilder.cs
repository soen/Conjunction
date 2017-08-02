using System;
using System.Linq.Expressions;
using Conjunction.Core.Model;
using Conjunction.Core.Model.Processing;
using Conjunction.Core.Model.Providers.SearchQueryValue;

namespace Conjunction.Sitecore.Model.Processing
{
		/// <summary>
		/// Represents a specialized visitor that can build up a predicate of type <see cref="Expression{T}" /> 
		/// of <see cref="Func{T, Boolean}" /> from search query elements.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
		public interface ISearchQueryPredicateBuilder<T> : ISearchQueryElementVisitor<T> where T : IIndexableEntity, new()
  {
    /// <summary>
    /// Gets the <see cref="Conjunction.Core.Model.Providers.SearchQueryValue.ISearchQueryValueProvider"/> that is associated with the given predicate builder.
    /// </summary>
    ISearchQueryValueProvider SearchQueryValueProvider { get; }
    
    /// <summary>
    /// Returns the aggregated output produced by the predicate builder.
    /// </summary>
    /// <returns>An aggregated predicate expression.</returns>
    Expression<Func<T, bool>> GetOutput();
  }
}