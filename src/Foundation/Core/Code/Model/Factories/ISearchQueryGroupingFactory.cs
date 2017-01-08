namespace Conjunction.Foundation.Core.Model.Factories
{
  public interface ISearchQueryGroupingFactory
  {
    SearchQueryGrouping<T> Create<T>(string configuredLogicalOperator) where T : IndexableEntity, new();
  }
}