using System;
using System.Collections.Specialized;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Services;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// Represents a query string based value provider, where the dynamic values required by 
  /// the <see cref="SearchQueryRule{T}"/> elements are resolved from query string name/value pairs.
  /// </summary>
  public class QueryStringSearchQueryValueProvider : ISearchQueryValueProvider
  {
    private readonly Func<NameValueCollection> _getParameterNameValuePairs;

    public QueryStringSearchQueryValueProvider(Func<NameValueCollection> getParameterNameValuePairs)
    {
      Assert.ArgumentNotNull(getParameterNameValuePairs, "getParameterNameValuePairs");

      _getParameterNameValuePairs = getParameterNameValuePairs;
    }

    public object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new()
    {
      Assert.ArgumentNotNull(searchQueryRule, "searchQueryRule");

      object retVal;

      var value = GetDefaultValueOrDynamicValueProvidedByParameter(searchQueryRule);
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

    private string GetDefaultValueOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule) 
      where T : IndexableEntity, new()
    {
      string value;

      if (string.IsNullOrWhiteSpace(searchQueryRule.DynamicValueProvidingParameter))
        value = searchQueryRule.DefaultValue;
      else
      {
        var valueProviderParameterName = searchQueryRule.DynamicValueProvidingParameter;

        var parameterNameValuePairs = _getParameterNameValuePairs();

        if (parameterNameValuePairs == null)
          throw new InvalidOperationException("The parameterNameValuePairs cannot be null");

        value = parameterNameValuePairs[valueProviderParameterName];
      }

      return value;
    }
  }
}