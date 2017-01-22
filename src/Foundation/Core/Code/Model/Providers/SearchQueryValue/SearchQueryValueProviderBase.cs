using System;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Services;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class SearchQueryValueProviderBase : ISearchQueryValueProvider
  {
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

    protected abstract string GetRawDefaultOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new();
  }
}