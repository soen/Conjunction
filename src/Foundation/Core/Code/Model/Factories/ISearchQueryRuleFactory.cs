namespace Conjunction.Foundation.Core.Model.Factories
{
  public interface ISearchQueryRuleFactory
  {
    SearchQueryRule<T> Create<T>(string associatedPropertyName, string configuredComparisonOperator, string dynamicValueProvidingParameter = null, string defaultValue = null) where T : IndexableEntity, new();
  }
}