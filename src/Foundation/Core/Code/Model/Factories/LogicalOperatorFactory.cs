using System;
using System.ComponentModel;

namespace Conjunction.Foundation.Core.Model.Factories
{
  public class LogicalOperatorFactory : ILogicalOperatorFactory
  {
    public LogicalOperator Create(string rawLogicalOperator)
    {
      LogicalOperator logicalOperator;
      if (Enum.TryParse(rawLogicalOperator, out logicalOperator) == false)
        throw new InvalidEnumArgumentException($"The logical operator is not valid: {rawLogicalOperator}");

      return logicalOperator;
    }
  }
}