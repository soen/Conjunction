using System;
using Conjunction.Core.Infrastructure;
using Conjunction.Core.Model.Services;

namespace Conjunction.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// Represents the base class of an search query value provider.
  /// </summary>
  public abstract class SearchQueryValueProviderBase : ISearchQueryValueProvider
  {
	  private readonly ISearchQueryValueConversionService _conversionService;

	  protected SearchQueryValueProviderBase(ISearchQueryValueConversionService conversionService)
	  {
		  _conversionService = conversionService;
	  }

    /// <summary>
    /// Returns the value needed by the search query value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchQueryRule">The specifed search query rule</param>
    /// <returns>A typed value</returns>
    public object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IIndexableEntity, new()
    {
	    if (searchQueryRule == null) throw new ArgumentNullException(nameof(searchQueryRule));
			
	    object retVal;

      var value = GetRawDefaultOrDynamicValueProvidedByParameter(searchQueryRule);
      if (string.IsNullOrWhiteSpace(value))
        return null;

      var propertyType = ExpressionUtils.GetPropertyTypeFromPropertySelector(searchQueryRule.PropertySelector);

      Tuple<string, string> rangeValueParts;
      if (_conversionService.TryConvertToRangeValueParts(value, out rangeValueParts))
      {
        var lowerValue = _conversionService.ToTypedValue(propertyType, rangeValueParts.Item1);
        var upperValue = _conversionService.ToTypedValue(propertyType, rangeValueParts.Item2);

        retVal = new RangeValue(lowerValue, upperValue);
      }
      else
        retVal = _conversionService.ToTypedValue(propertyType, value);

      return retVal;
    }

    /// <summary>
    /// Retruns the raw value of either the default or dynamically provided value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IIndexableEntity"/> implementation to use.</typeparam>
    /// <param name="searchQueryRule">The specifed search query rule</param>
    /// <returns>A raw string value of either the default or dynamically provided value</returns>
    protected abstract string GetRawDefaultOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule) where T : IIndexableEntity, new();
  }
}