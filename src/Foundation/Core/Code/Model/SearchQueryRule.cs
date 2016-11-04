using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model.Processing;

namespace Conjunction.Foundation.Core.Model
{
  // TODO: This class could eventually also include whether to use fuzzy search or not, and if so, how much fuzzyness to use (number)
  public class SearchQueryRule<T> : ISearchQueryElement<T> where T : IndexableEntity, new()
  {
    public SearchQueryRule(Expression<Func<T, object>> propertySelector, 
                           ComparisonOperator comparisonOperator, 
                           string dynamicValueProvidingParameter = null,
                           string defaultValue = null)
    {
      PropertySelector = propertySelector;
      ComparisonOperator = comparisonOperator;
      DynamicValueProvidingParameter = dynamicValueProvidingParameter;
      DefaultValue = defaultValue;
    }

    public Expression<Func<T, object>> PropertySelector { get; }

    public ComparisonOperator ComparisonOperator { get; }

    public string DynamicValueProvidingParameter { get; }

    public string DefaultValue { get; }

    public void Accept(ISearchQueryElementVisitor<T> visitor)
    {
      visitor.VisitSearchQueryRule(this);
    }
  }
}