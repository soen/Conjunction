using System;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Services;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// Represents the base class of an search query value provider.
  /// </summary>
  public abstract class SearchQueryValueProviderBase : ISearchQueryValueProvider
  {
    /// <summary>
    /// Returns the value needed by the search query value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchQueryRule">The specifed search query rule</param>
    /// <returns>A typed value</returns>
    public object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new()
    {
      Assert.ArgumentNotNull(searchQueryRule, "searchQueryRule");

      object retVal;

      var value = GetRawDefaultOrDynamicValueProvidedByParameter(searchQueryRule);
      if (string.IsNullOrWhiteSpace(value))
        return null;

      var propertyType = ExpressionUtils.GetPropertyTypeFromPropertySelector(searchQueryRule.PropertySelector);

      Tuple<string, string> rangeValueParts;
      if (SearchQueryValueConversionService.TryConvertToRangeValueParts(value, out rangeValueParts))
      {
        var lowerValue = SearchQueryValueConversionService.ToTypedValue(propertyType, rangeValueParts.Item1);
        var upperValue = SearchQueryValueConversionService.ToTypedValue(propertyType, rangeValueParts.Item2);

        retVal = new RangeValue(lowerValue, upperValue);
      }
      else
        retVal = SearchQueryValueConversionService.ToTypedValue(propertyType, value);

      return retVal;
    }

    /// <summary>
    /// Retruns the raw value of either the default or dynamically provided value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchQueryRule">The specifed search query rule</param>
    /// <returns>A raw string value of either the default or dynamically provided value</returns>
    protected abstract string GetRawDefaultOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new();
  }
}