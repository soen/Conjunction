using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Sitecore.Data;

namespace Conjunction.Foundation.Core.Infrastructure.TypeConverters
{
  /// <summary>
  /// Provides a type converter to convert <see cref="ID"/> objects to and from various other representations.
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class SitecoreIDConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
      if (value != null && value is string)
      {
        string text = ((string)value).Trim();
        return new ID(text);
      }
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof(string))
      {
        return ((ID)value).ToString();
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}