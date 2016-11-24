using System;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Sitecore.ContentSearch.Linq;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Services
{
  /// <summary>
  /// Provides functionalities for constructing expression trees, based on a given property selector and value. 
  /// </summary>
  public static class ExpressionConversionService
  {
    public static Expression<Func<T, bool>> ToEquals<T>(Expression<Func<T, object>> propertySelector, object value)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");

      var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var valueExp = Expression.Constant(value);

      var equalsExp = Expression.Equal(propertyExp, valueExp);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    public static Expression<Func<T, bool>> ToGreaterThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");
      Assert.ArgumentNotNull(value, "The specified value cannot be null");

      var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var equalsExp = Expression.GreaterThanOrEqual(propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    public static Expression<Func<T, bool>> ToLessThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");
      Assert.ArgumentNotNull(value, "The specified value cannot be null");

      var parameterExp = Expression.Parameter(typeof (T), "s");

      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var equalsExp = Expression.LessThanOrEqual(propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(equalsExp, parameterExp);
    }

    public static Expression<Func<T, bool>> ToContains<T>(Expression<Func<T, object>> propertySelector, string value)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");

      var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));

      var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

      var valueExp = Expression.Constant(value, typeof(string));
      var containsMethodExp = Expression.Call(propertyExp, method, valueExp);

      return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    }

    public static Expression<Func<T, bool>> ToEnumerableContains<T>(Expression<Func<T, object>> propertySelector, object value)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");

      var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
      var val = Expression.Constant(value);

      var containsMethodExp = Expression.Call(typeof(Enumerable), "Contains", new[] { value.GetType() }, propertyExp, val);

      return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    }

    public static Expression<Func<T, bool>> ToBetween<T>(Expression<Func<T, object>> propertySelector, object lowerValue, object upperValue, Inclusion inclusion = Inclusion.Both)
    {
      Assert.ArgumentNotNull(propertySelector, "The specified property selector cannot be null");
      Assert.ArgumentNotNull(lowerValue, "The specified lower-value cannot be null");
      Assert.ArgumentNotNull(upperValue, "The specified upper-value cannot be null");

      var parameterExp = Expression.Parameter(typeof(T), "s");
      var propertyExp = Expression.Property(parameterExp, ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector));
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