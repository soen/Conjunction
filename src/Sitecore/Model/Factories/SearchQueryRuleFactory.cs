using System;
using Conjunction.Core;
using Conjunction.Core.Infrastructure;
using Conjunction.Core.Model;

namespace Conjunction.Sitecore.Model.Factories
{
  /// <summary>
  /// Represents the default factory for building instances of type <see cref="SearchQueryRule{T}"/>.
  /// </summary>
  public class SearchQueryRuleFactory : ISearchQueryRuleFactory
  {
    public SearchQueryRuleFactory() 
      : this(Locator.Current.GetInstance<IComparisonOperatorFactory>())
    {
    }

    public SearchQueryRuleFactory(IComparisonOperatorFactory comparisonOperatorFactory)
    {
	    if (comparisonOperatorFactory == null)
				throw new ArgumentNullException(nameof(comparisonOperatorFactory));

	    ComparisonOperatorFactory = comparisonOperatorFactory;
    }

    public IComparisonOperatorFactory ComparisonOperatorFactory { get; }

    public SearchQueryRule<T> Create<T>(string associatedPropertyName, string configuredComparisonOperator, 
                                        string dynamicValueProvidingParameter = null, string defaultValue = null) 
      where T : IIndexableEntity, new()
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