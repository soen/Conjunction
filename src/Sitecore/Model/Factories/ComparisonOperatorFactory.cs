using System;
using System.ComponentModel;
using Conjunction.Core.Model;

namespace Conjunction.Sitecore.Model.Factories
{
  /// <summary>
  /// Represents the default factory for building types of <see cref="ComparisonOperator"/>.
  /// </summary>
  public class ComparisonOperatorFactory : IComparisonOperatorFactory
  {
    public ComparisonOperator Create(string rawComparisonOperator)
    {
      ComparisonOperator comparisonOperator;
      if (Enum.TryParse(rawComparisonOperator, out comparisonOperator) == false)
        throw new InvalidEnumArgumentException($"The comparison operator is not valid: {rawComparisonOperator}");

      return comparisonOperator;
    }
  }
}