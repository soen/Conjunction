namespace Conjunction.Foundation.Core.Model.Factories
{
  public interface ISearchQueryGroupingFactory
  {
    ILogicalOperatorFactory LogicalOperatorFactory { get; }

    SearchQueryGrouping<T> Create<T>(string configuredLogicalOperator) where T : IndexableEntity, new();
  }
}