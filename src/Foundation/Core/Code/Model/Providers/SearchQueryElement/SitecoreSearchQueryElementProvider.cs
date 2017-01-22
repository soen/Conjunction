﻿using System;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Factories;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryElement
{
  /// <summary>
  /// Represents a Sitecore configured search query element provider, accepting a 
  /// <see cref="Item"/> root, which gets transformed into a <see cref="ISearchQueryElement{T}"/> root.
  /// </summary>
  public class SitecoreSearchQueryElementProvider : ISearchQueryElementProvider
  {
    private readonly Func<Item> _searchQueryRootItemFactory;

    public SitecoreSearchQueryElementProvider(Func<Item> searchQueryRootItemFactory)
      : this(searchQueryRootItemFactory, 
             Locator.Current.GetInstance<ISearchQueryRuleFactory>(),
             Locator.Current.GetInstance<ISearchQueryGroupingFactory>())
    {
    }

    public SitecoreSearchQueryElementProvider(Func<Item> searchQueryRootItemFactory,
                                              ISearchQueryRuleFactory searchQueryRuleFactory,
                                              ISearchQueryGroupingFactory searchQueryGroupingFactory)
    {
      Assert.ArgumentNotNull(searchQueryRootItemFactory, "searchQueryRootItemFactory");
      Assert.ArgumentNotNull(searchQueryRuleFactory, "searchQueryRuleFactory");
      Assert.ArgumentNotNull(searchQueryGroupingFactory, "searchQueryGroupingFactory");

      _searchQueryRootItemFactory = searchQueryRootItemFactory;
      SearchQueryRuleFactory = searchQueryRuleFactory;
      SearchQueryGroupingFactory = searchQueryGroupingFactory;
    }

    public ISearchQueryRuleFactory SearchQueryRuleFactory { get; }
    public ISearchQueryGroupingFactory SearchQueryGroupingFactory { get; }

    public ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new()
    {
      Item searchQueryRootItem = _searchQueryRootItemFactory();

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

      Type type;
      try
      {
        type = Type.GetType(configuredIndexableEntityType);        
      }
      catch (Exception ex)
      {
        var errorMessage = $"Cannot find the configured IndexableEntity type <{configuredIndexableEntityType}>";

        Log.Error(errorMessage, this);
        throw new InvalidOperationException(errorMessage, ex);
      }

      if (type != typeof(T))
        throw new ArgumentException($"The configured IndexableEntity type <{type}> does not match the specified generic type T: {typeof(T)}");      
    }

    private ISearchQueryElement<T> GetSearchQueryElementFor<T>(Item item)
      where T : IndexableEntity, new()
    {
      if (item.IsDerived(Constants.Templates._SearchQueryRule.TemplateId))
        return GetSearchQueryElementFromItem<T>(item);

      var searchQueryGrouping = GetSearchQueryGroupingFromItem<T>(item);

      foreach (Item child in item.Children)
      {
        var searchQueryElement = GetSearchQueryElementFor<T>(child);
        searchQueryGrouping.SearchQueryElements.Add(searchQueryElement);
      }

      return searchQueryGrouping;
    }

    private SearchQueryRule<T> GetSearchQueryElementFromItem<T>(Item item) where T : IndexableEntity, new()
    {
      var associatedPropertyName =
        item.Fields[Constants.Fields._SearchQueryRule.AssociatedPropertyName].Value;
      var configuredComparisonOperator =
        item.Fields[Constants.Fields._SearchQueryRule.ComparisonOperator].Value;
      var dynamicValueProvidingParameter =
        item.Fields[Constants.Fields._SearchQueryRule.DynamicValueProvidingParameter].Value;
      var defaultValue =
        item.Fields[Constants.Fields._SearchQueryRule.DefaultValue].Value;

      return SearchQueryRuleFactory.Create<T>(
        associatedPropertyName, configuredComparisonOperator, dynamicValueProvidingParameter, defaultValue);
    }

    private SearchQueryGrouping<T> GetSearchQueryGroupingFromItem<T>(Item item) where T : IndexableEntity, new()
    {
      var configuredLogicalOperator =
        item.Fields[Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator].Value;

      return SearchQueryGroupingFactory.Create<T>(configuredLogicalOperator);
    }
  }
}