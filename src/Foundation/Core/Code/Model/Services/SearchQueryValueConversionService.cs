using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Conjunction.Foundation.Core.Infrastructure.TypeConverters;

namespace Conjunction.Foundation.Core.Model.Services
{
  public static class SearchQueryValueConversionService
  {
    static SearchQueryValueConversionService()
    {
      TypeDescriptor.AddAttributes(typeof(Sitecore.Data.ID), new TypeConverterAttribute(typeof(SitecoreIDConverter)));
    }

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

    public static bool TryConvertToRangeValueParts(string value, out Tuple<string, string> rangeValueParts)
    {
      bool retVal;
      const string rangeValuePattern = @"^\[([^\.]+)(;|:)([^\.]+)\]$";

      var match = Regex.Match(value, rangeValuePattern);
      if (match.Success)
      {
        var lowerValue = match.Groups[1].Value;
        var upperValue = match.Groups[3].Value;
        var inclusion = match.Groups[2].Value; // TODO: Implement this, when needed, as it could be used to determine how the range is used: ':' means both ends included, ';' means lower not included but upper is, etc.

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