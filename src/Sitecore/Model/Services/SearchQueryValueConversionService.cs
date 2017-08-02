using System.ComponentModel;
using Conjunction.Sitecore.Infrastructure.TypeConverters;
using Sitecore.Data;

namespace Conjunction.Sitecore.Model.Services
{
  /// <summary>
  /// Provides functionalities for converting raw <see cref="string"/> values into typed values.
  /// </summary>
  public class SearchQueryValueConversionService : Core.Model.Services.SearchQueryValueConversionService
  {
    static SearchQueryValueConversionService()
    {
      TypeDescriptor.AddAttributes(typeof(ID), new TypeConverterAttribute(typeof(SitecoreIDConverter)));
    }    
  }
}