using System;
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
    private readonly Func<Item> _getSearchQueryRootItem;
    private readonly ISearchQueryRuleFactory _searchQueryRuleFactory;
    private readonly ISearchQueryGroupingFactory _searchQueryGroupingFactory;

    public SitecoreSearchQueryElementProvider(Func<Item> getSearchQueryRootItem)
      : this(getSearchQueryRootItem, new SearchQueryRuleFactory(), new SearchQueryGroupingFactory())
    {
    }

    public SitecoreSearchQueryElementProvider(Func<Item> getSearchQueryRootItem,
                                              ISearchQueryRuleFactory searchQueryRuleFactory,
                                              ISearchQueryGroupingFactory searchQueryGroupingFactory)
    {
      Assert.ArgumentNotNull(getSearchQueryRootItem, "getSearchQueryRootItem");
      Assert.ArgumentNotNull(searchQueryRuleFactory, "searchQueryRuleFactory");
      Assert.ArgumentNotNull(searchQueryGroupingFactory, "searchQueryGroupingFactory");

      _getSearchQueryRootItem = getSearchQueryRootItem;
      _searchQueryRuleFactory = searchQueryRuleFactory;
      _searchQueryGroupingFactory = searchQueryGroupingFactory;
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

      return _searchQueryRuleFactory.Create<T>(
        associatedPropertyName, configuredComparisonOperator, dynamicValueProvidingParameter, defaultValue);
    }

    private SearchQueryGrouping<T> GetSearchQueryGroupingFromItem<T>(Item item) where T : IndexableEntity, new()
    {
      var configuredLogicalOperator =
        item.Fields[Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator].Value;

      return _searchQueryGroupingFactory.Create<T>(configuredLogicalOperator);
    }
  }
}