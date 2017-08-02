using System;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Core.Infrastructure;
using Sitecore.ContentSearch.Linq;

namespace Conjunction.Sitecore.Model.Services
{
  /// <summary>
  /// Provides functionalities for constructing expression trees, based on a given property selector and value. 
  /// </summary>
  public static class ExpressionConversionService
  {
    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> to an 'equals' expression
    /// using the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector</param>
    /// <param name="value">The value</param>
    /// <returns>An expression on the form 'nameOfPropertySelector equals value'</returns>
    public static Expression<Func<T, bool>> ToEquals<T>(Expression<Func<T, object>> propertySelector, object value)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));
			
	    var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var valueExp = Expression.Constant(value);

      var equalsExp = Expression.Equal(propertyExp, valueExp);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> to an 'greater-than-or-equals' expression
    /// using the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector</param>
    /// <param name="value">The value</param>
    /// <returns>An expression on the form 'nameOfPropertySelector greater-than-or-equals value'</returns>
    public static Expression<Func<T, bool>> ToGreaterThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));
	    if (value == null) throw new ArgumentNullException(nameof(value));

	    var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var equalsExp = Expression.GreaterThanOrEqual(propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> to an 'less-than-or-equals' expression
    /// using the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector</param>
    /// <param name="value">The value</param>
    /// <returns>An expression on the form 'nameOfPropertySelector less-than-or-equals value'</returns>
    public static Expression<Func<T, bool>> ToLessThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));
	    if (value == null) throw new ArgumentNullException(nameof(value));

	    var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var equalsExp = Expression.LessThanOrEqual(propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> to an 'contains' expression 
    /// using the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector</param>
    /// <param name="value">The value</param>
    /// <returns>An expression on the form 'nameOfPropertySelector contains value'</returns>
    public static Expression<Func<T, bool>> ToContains<T>(Expression<Func<T, object>> propertySelector, string value)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));

	    var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));

      var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

      var valueExp = Expression.Constant(value, typeof(string));
      var containsMethodExp = Expression.Call(propertyExp, method, valueExp);

      return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    }

    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> of type <see cref="System.Collections.IEnumerable"/>
    /// to an 'contains' expression using the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector (must be of type <see cref="System.Collections.IEnumerable"/>)</param>
    /// <param name="value">The value</param>
    /// <returns>An expression on the form 'nameOfPropertySelector contains value'</returns>
    public static Expression<Func<T, bool>> ToEnumerableContains<T>(Expression<Func<T, object>> propertySelector, object value)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));

	    var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var containsMethodExp = Expression.Call(typeof(Enumerable), "Contains", new[] { value.GetType() }, propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    }

    /// <summary>
    /// Converts the specified <paramref name="propertySelector"/> to an 'between' expression using the given 
    /// <paramref name="lowerValue"/> and <paramref name="upperValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="propertySelector">The property selector</param>
    /// <param name="lowerValue">The lower-bound value</param>
    /// <param name="upperValue">The upper-bound value</param>
    /// <param name="inclusion">The state of how the bounds are included</param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> ToBetween<T>(Expression<Func<T, object>> propertySelector, object lowerValue, object upperValue, Inclusion inclusion = Inclusion.Both)
    {
	    if (propertySelector == null) throw new ArgumentNullException(nameof(propertySelector));
	    if (lowerValue == null) throw new ArgumentNullException(nameof(lowerValue));
	    if (upperValue == null) throw new ArgumentNullException(nameof(upperValue));

	    var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property((Expression) parameterExp, (string) ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var lowerValueExp = Expression.Constant(lowerValue);
      var upperValueExp = Expression.Constant(upperValue);
      var inclusionValueExp = Expression.Constant(inclusion);

      var betweenMethodExp = Expression.Call(
        typeof(MethodExtensions),
        "Between", new[] { propertyExp.Type },
        propertyExp,
        lowerValueExp, upperValueExp, inclusionValueExp);

      return Expression.Lambda<Func<T, bool>>(betweenMethodExp, parameterExp);
    }
  }
}