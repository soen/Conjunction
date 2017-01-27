namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the set of logical operators a <see cref="SearchQueryGrouping{T}"/> 
  /// can be configured to use, in order to determine the logical relationship between 
  /// its children.
  /// </summary>
  public enum LogicalOperator
  {
    /// <summary>
    /// The operator represents a "logical AND".
    /// </summary>
    And,

    /// <summary>
    /// The operator represents a "logical OR".
    /// </summary>
    Or
  }
}