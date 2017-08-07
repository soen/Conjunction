using System;
using System.ComponentModel;
using Conjunction.Core.Model;

namespace Conjunction.Sitecore.Model.Factories
{
  /// <summary>
  /// Represents the default factory for building types of <see cref="LogicalOperator"/>.
  /// </summary>
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