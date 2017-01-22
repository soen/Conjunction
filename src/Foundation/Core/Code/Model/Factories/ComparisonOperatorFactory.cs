using System;
using System.ComponentModel;

namespace Conjunction.Foundation.Core.Model.Factories
{
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