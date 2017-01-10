using Conjunction.Foundation.Core.Infrastructure;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Factories
{
  public class SearchQueryRuleFactory : ISearchQueryRuleFactory
  {
    public SearchQueryRuleFactory() 
      : this(Locator.Current.GetInstance<IComparisonOperatorFactory>())
    {
    }

    public SearchQueryRuleFactory(IComparisonOperatorFactory comparisonOperatorFactory)
    {
      Assert.ArgumentNotNull(comparisonOperatorFactory, "comparisonOperatorFactory");

      ComparisonOperatorFactory = comparisonOperatorFactory;
    }

    public IComparisonOperatorFactory ComparisonOperatorFactory { get; }

    public SearchQueryRule<T> Create<T>(string associatedPropertyName, string configuredComparisonOperator, 
                                        string dynamicValueProvidingParameter = null, string defaultValue = null) 
      where T : IndexableEntity, new()
    {
      var propertySelector = ExpressionUtils.GetPropertySelector<T, object>(associatedPropertyName);
      var comparisonOperator = ComparisonOperatorFactory.Create(configuredComparisonOperator);

      return new SearchQueryRule<T>(
        propertySelector,
        comparisonOperator,
        dynamicValueProvidingParameter,
        defaultValue
      );
    }
  }
}