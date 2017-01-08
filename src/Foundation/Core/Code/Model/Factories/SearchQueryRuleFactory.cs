using Conjunction.Foundation.Core.Infrastructure;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Factories
{
  public class SearchQueryRuleFactory : ISearchQueryRuleFactory
  {
    private readonly IComparisonOperatorFactory _comparisonOperatorFactory;

    public SearchQueryRuleFactory() 
      : this(new ComparisonOperatorFactory())
    {
    }

    public SearchQueryRuleFactory(IComparisonOperatorFactory comparisonOperatorFactory)
    {
      Assert.ArgumentNotNull(comparisonOperatorFactory, "comparisonOperatorFactory");

      _comparisonOperatorFactory = comparisonOperatorFactory;
    }

    public SearchQueryRule<T> Create<T>(string associatedPropertyName, string configuredComparisonOperator, 
                                        string dynamicValueProvidingParameter = null, string defaultValue = null) 
      where T : IndexableEntity, new()
    {
      var propertySelector = ExpressionUtils.GetPropertySelector<T, object>(associatedPropertyName);
      var comparisonOperator = _comparisonOperatorFactory.Create(configuredComparisonOperator);

      return new SearchQueryRule<T>(
        propertySelector,
        comparisonOperator,
        dynamicValueProvidingParameter,
        defaultValue
      );
    }
  }
}