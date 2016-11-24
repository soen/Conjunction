using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Conjunction.Foundation.Core.Infrastructure.TypeConverters;

namespace Conjunction.Foundation.Core.Model.Services
{
  /// <summary>
  /// Provides functionalities for converting raw <see cref="string"/> values into typed values.
  /// </summary>
  public static class SearchQueryValueConversionService
  {
    static SearchQueryValueConversionService()
    {
      TypeDescriptor.AddAttributes(typeof(Sitecore.Data.ID), new TypeConverterAttribute(typeof(SitecoreIDConverter)));
    }

    /// <summary>
    /// Converts the <paramref name="value"/> to the specified <paramref name="valueType"/>.
    /// </summary>
    /// <param name="valueType">The value type that the specified <paramref name="value"/> needs to be converted into.</param>
    /// <param name="value">The value that needs to be converted.</param>
    /// <returns></returns>
    public static object ToTypedValue(Type valueType, string value)
    {
      object retVal = null;
      var type = valueType;

      if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof (IEnumerable<>))
        type = valueType.GenericTypeArguments[0];
      
      try
      {
        var typeConverter = TypeDescriptor.GetConverter(type);
        if (typeConverter.CanConvertFrom(value.GetType()))
          retVal = typeConverter.ConvertFromString(value);
      }
      catch
      {
        // If the value can't be converted into the given type, due to parsing issues, ignore it
      }

      return retVal;
    }

    /// <summary>
    /// Tries to convert the specified <paramref name="value"/> into range value parts.
    /// </summary>
    /// <remarks>
    /// The output parameter <paramref name="rangeValueParts"/> is intended to be used within
    /// the <see cref="RangeValue"/> type.
    /// </remarks>
    /// <param name="value">The value that needs to be converted.</param>
    /// <param name="rangeValueParts">The range value parts ressembling the range values, if converted.</param>
    /// <returns></returns>
    public static bool TryConvertToRangeValueParts(string value, out Tuple<string, string> rangeValueParts)
    {
      bool retVal;
      const string rangeValuePattern = @"^\[([^\.]+)(;|:)([^\.]+)\]$";

      var match = Regex.Match(value, rangeValuePattern);
      if (match.Success)
      {
        var lowerValue = match.Groups[1].Value;
        var upperValue = match.Groups[3].Value;
        
        // TODO: Implement this, when needed, as it could be used to determine how the range is used: ':' means both ends included, ';' means lower not included but upper is, etc.
        var inclusion = match.Groups[2].Value; 

        rangeValueParts = new Tuple<string, string>(lowerValue, upperValue);
        retVal = true;
      }
      else
      {
        rangeValueParts = null;
        retVal = false;
      }
      return retVal;
    }
  }
}