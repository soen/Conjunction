using Conjunction.Core.Model;

namespace Conjunction.Sitecore.Model.Factories
{
  /// <summary>
  /// Provides functionality to build types of <see cref="LogicalOperator"/>.
  /// </summary>
  public interface ILogicalOperatorFactory
  {
    /// <summary>
    /// Creates a new <see cref="LogicalOperator"/> based on the raw logical operator value.
    /// </summary>
    /// <param name="rawLogicalOperator">The raw logical operator value.</param>
    /// <returns>A logical operator type.</returns>
    LogicalOperator Create(string rawLogicalOperator);
  }
}