namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the set of comparison operators a <see cref="SearchQueryRule{T}"/> 
  /// can be configured to use, when comparing its selected property against its
  /// dynamically provided value or default value.
  /// </summary>
  public enum ComparisonOperator
  {
    /// <summary>
    /// The operator represents a "greater than" comparison.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// The operator represents a "less than" comparison.
    /// </summary>
    LessThan,

    /// <summary>
    /// The operator represents a "greater than or equal" comparison.
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// The operator represents a "less than or equal" comparison.
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// The operator represents a "equal" comparison.
    /// </summary>
    Equal,

    /// <summary>
    /// The operator represents a "not equal" comparison.
    /// </summary>
    NotEqual,

    /// <summary>
    /// The operator represents a "contains" comparison.
    /// </summary>
    Contains,

    /// <summary>
    /// The operator represents a "not contains" comparison.
    /// </summary>
    NotContains,

    /// <summary>
    /// The operator represents a "set that contains just the specified" comparison.
    /// </summary>
    Between,

    /// <summary>
    /// The operator represents a "set that contains everything except the specified" comparison.
    /// </summary>
    NotBetween
  }
}