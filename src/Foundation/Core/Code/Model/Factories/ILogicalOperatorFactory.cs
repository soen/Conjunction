namespace Conjunction.Foundation.Core.Model.Factories
{
  public interface ILogicalOperatorFactory
  {
    LogicalOperator Create(string rawLogicalOperator);
  }
}