﻿using System;
using System.ComponentModel;
using Conjunction.Foundation.Core.Infrastructure;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryElement
{
  /// <summary>
  /// Represents a Sitecore configured search query element provider, accepting a 
  /// <see cref="Item"/> root, which gets transformed into a <see cref="ISearchQueryElement{T}"/> root.
  /// </summary>
  public class SitecoreConfiguredSearchQueryElementProvider : ISearchQueryElementProvider
  {
    private readonly Func<Item> _getSearchQueryRootItem;
    
    public SitecoreConfiguredSearchQueryElementProvider(Func<Item> getSearchQueryRootItem)
    {
      Assert.ArgumentNotNull(getSearchQueryRootItem, "getSearchQueryRootItem");

      _getSearchQueryRootItem = getSearchQueryRootItem;
    }

    public ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new()
    {
      Item searchQueryRootItem = _getSearchQueryRootItem();

      if (searchQueryRootItem == null)
        throw new ArgumentException("The searchQueryRootItem cannot be null");

      if (searchQueryRootItem.IsDerived(Constants.Templates.SearchQueryRoot.TemplateId) == false)
        throw new ArgumentException();

      VerifyConfiguredIndexableEntityType<T>(searchQueryRootItem);
      return GetSearchQueryElementFor<T>(searchQueryRootItem);
    }

    private void VerifyConfiguredIndexableEntityType<T>(Item item) where T : IndexableEntity, new()
    {
      var configuredIndexableEntityType =
        item.Fields[Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType].Value;

      var type = Type.GetType(configuredIndexableEntityType);
      if (type != typeof(T))
        throw new ArgumentException($"The configured IndexableEntity type <{type}> does not match the specified generic type T: {typeof(T)}");
    }

    private ISearchQueryElement<T> GetSearchQueryElementFor<T>(Item item)
      where T : IndexableEntity, new()
    {
      if (item.IsDerived(Constants.Templates._SearchQueryRule.TemplateId))
        return GetSearchQueryRuleFor<T>(item);

      var searchQueryGrouping = new SearchQueryGrouping<T>(GetConfiguredLogicalOperator(item));

      foreach (Item child in item.Children)
      {
        var searchQueryElement = GetSearchQueryElementFor<T>(child);
        searchQueryGrouping.SearchQueryElements.Add(searchQueryElement);
      }

      return searchQueryGrouping;
    }
    
    private ISearchQueryElement<T> GetSearchQueryRuleFor<T>(Item item) where T : IndexableEntity, new()
    {
      var associatedPropertyName = item.Fields[Constants.Fields._SearchQueryRule.AssociatedPropertyName].Value;
      var dynamicValueProvidingParameter = item.Fields[Constants.Fields._SearchQueryRule.DynamicValueProvidingParameter].Value;
      var defaultValue = item.Fields[Constants.Fields._SearchQueryRule.DefaultValue].Value;

      return new SearchQueryRule<T>(
        ExpressionUtils.GetPropertySelector<T, object>(associatedPropertyName),
        GetConfiguredComparisonOperator(item),
        dynamicValueProvidingParameter,
        defaultValue
      );
    }

    private LogicalOperator GetConfiguredLogicalOperator(Item item)
    {
      var configuredLogicalOperator =
        item.Fields[Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator].Value;

      LogicalOperator logicalOperator;
      if (Enum.TryParse(configuredLogicalOperator, out logicalOperator) == false)
        throw new InvalidEnumArgumentException($"The configured logical operator is not valid: {configuredLogicalOperator}");

      return logicalOperator;
    }

    private ComparisonOperator GetConfiguredComparisonOperator(Item item)
    {
      var configuredComparisonOperator = item.Fields[Constants.Fields._SearchQueryRule.ComparisonOperator].Value;

      ComparisonOperator comparisonOperator;
      if (Enum.TryParse(configuredComparisonOperator, out comparisonOperator) == false)
        throw new InvalidEnumArgumentException($"The configured comparison operator is not valid: {configuredComparisonOperator}");

      return comparisonOperator;
    }
  }
}