using System;
using System.Linq.Expressions;
using System.Reflection;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Infrastructure
{
  /// <summary>
  /// Provides functionalities for working with property selector expressions.
  /// </summary>
  public static class ExpressionUtils
  {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static Expression<Func<TIn, TOut>> GetPropertySelector<TIn, TOut>(string propertyName)
    {
      Assert.ArgumentNotNullOrEmpty(propertyName, "propertyName");

      ParameterExpression param = Expression.Parameter(typeof(TIn));
      Expression body = Expression.Property(param, propertyName);

      // Unfortunately, expecting auto-boxing of value-types to match the return type of the expression will not work.
      // As such, the boxing is performed manually, unless the property is already a reference-type.
      // http://stackoverflow.com/questions/8974837/expression-of-type-system-datetime-cannot-be-used-for-return-type-system-obje
      if (body.Type.IsValueType)
        body = Expression.Convert(body, typeof(object));

      return Expression.Lambda<Func<TIn, TOut>>(body, param);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertySelector"></param>
    /// <returns></returns>
    public static string GetPropertyNameFromPropertySelector<T>(Expression<Func<T, object>> propertySelector)
    {
      Assert.ArgumentNotNull(propertySelector, "propertySelector");

      var body = GetBodyFromExpression(propertySelector);
      return body.Member.Name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertySelector"></param>
    /// <returns></returns>
    public static Type GetPropertyTypeFromPropertySelector<T>(Expression<Func<T, object>> propertySelector)
    {
      Assert.ArgumentNotNull(propertySelector, "propertySelector");

      var body = GetBodyFromExpression(propertySelector);
      var propertyInfo = (PropertyInfo)body.Member;

      return propertyInfo.PropertyType;
    }

    private static MemberExpression GetBodyFromExpression<T>(Expression<Func<T, object>> expression)
    {
      MemberExpression body = expression.Body as MemberExpression;

      if (body == null)
      {
        UnaryExpression ubody = (UnaryExpression)expression.Body;
        body = ubody.Operand as MemberExpression;
      }
      return body;
    }
  }
}