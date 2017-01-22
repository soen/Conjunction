namespace Conjunction.Foundation.Core.Model.Factories
{
  /// <summary>
  /// Provides functionality to build types of <see cref="ComparisonOperator"/>.
  /// </summary>
  public interface IComparisonOperatorFactory
  {
    /// <summary>
    /// Creates a new <see cref="ComparisonOperator"/> based on the raw comparison operator value.
    /// </summary>
    /// <param name="rawComparisonOperator">The raw logical operator value.</param>
    /// <returns>A comparison operator type.</returns>
    ComparisonOperator Create(string rawComparisonOperator);
  }
}