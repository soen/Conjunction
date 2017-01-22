namespace Conjunction.Foundation.Core.Model.Factories
{
  public interface IComparisonOperatorFactory
  {
    ComparisonOperator Create(string rawComparisonOperator);
  }
}