﻿using System;
using System.Collections.Specialized;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryValue
{
  /// <summary>
  /// Represents a name/value pair based value provider, where the dynamic values required by 
  /// the <see cref="SearchQueryRule{T}"/> elements are resolved from name/value pairs, like query strings etc.
  /// </summary>
  public class NameValuePairSearchQueryValueProvider : SearchQueryValueProviderBase
  {
    private readonly Func<NameValueCollection> _nameValuePairFactory;

    public NameValuePairSearchQueryValueProvider(Func<NameValueCollection> nameValuePairFactory)
    {
      Assert.ArgumentNotNull(nameValuePairFactory, "nameValuePairFactory");

      _nameValuePairFactory = nameValuePairFactory;
    }
    
    protected override string GetRawDefaultOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule)
    {
      string value;

      if (string.IsNullOrWhiteSpace(searchQueryRule.DynamicValueProvidingParameter))
        value = searchQueryRule.DefaultValue;
      else
      {
        var valueProviderParameterName = searchQueryRule.DynamicValueProvidingParameter;
        var nameValuePairs = _nameValuePairFactory();

        if (nameValuePairs == null)
          throw new InvalidOperationException("The nameValuePairs cannot be null");

        value = nameValuePairs[valueProviderParameterName];
      }

      return value;
    }
  }
}