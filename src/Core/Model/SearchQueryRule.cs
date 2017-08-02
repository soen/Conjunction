using System;
using System.Linq.Expressions;
using Conjunction.Core.Model.Processing;

namespace Conjunction.Core.Model
{
  /// <summary>
  /// Represents a search query rule that defines how a given property of type <see cref="T"/>
  /// should be compared againts either a dynamically provided value or default value.
  /// </summary>
  /// <remarks>
  /// This class could eventually also include whether to use fuzzy search or not, 
  /// and if so, how much fuzzyness to use (number).
  /// </remarks>
  /// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
  public class SearchQueryRule<T> : ISearchQueryElement<T> where T : IIndexableEntity, new()
  {
    public SearchQueryRule(Expression<Func<T, object>> propertySelector, 
                           ComparisonOperator comparisonOperator, 
                           string dynamicValueProvidingParameter = null,
                           string defaultValue = null)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));

	    PropertySelector = propertySelector;
      ComparisonOperator = comparisonOperator;
      DynamicValueProvidingParameter = dynamicValueProvidingParameter;
      DefaultValue = defaultValue;
    }

    /// <summary>
    /// Gets the property selector that is associated with the given search query rule.
    /// </summary>
    public Expression<Func<T, object>> PropertySelector { get; }

    /// <summary>
    /// Gets the <see cref="ComparisonOperator"/> that is associated with the given search query rule.
    /// </summary>
    public ComparisonOperator ComparisonOperator { get; }

    /// <summary>
    /// Gets the dynamic value providing parameter that is associated with the given search query rule.
    /// </summary>
    public string DynamicValueProvidingParameter { get; }

    /// <summary>
    /// Gets the default value that is associated with the given search query rule.
    /// </summary>
    public string DefaultValue { get; }

    public void Accept(ISearchQueryElementVisitor<T> visitor)
    {
      visitor.VisitSearchQueryRule(this);
    }
  }
}