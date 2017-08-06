﻿using System;
using Conjunction.Core;
using Conjunction.Core.Infrastructure.Logging.Logging;
using Conjunction.Core.Model;
using Conjunction.Sitecore.Infrastructure;
using Conjunction.Sitecore.Model.Factories;
using Sitecore.Data.Items;

namespace Conjunction.Sitecore.Model.Providers.SearchQueryElement
{
	/// <summary>
	/// Represents a Sitecore configured search query element provider, accepting a 
	/// <see cref="Item"/> tree root, which gets transformed into a <see cref="ISearchQueryElement{T}"/> tree.
	/// </summary>
	public class SearchQueryElementProvider : ISitecoreItemSearchQueryElementProvider
	{
		private readonly Func<Item> _searchQueryRootItemFactory;
		private readonly ILog _logger;

		public SearchQueryElementProvider(Func<Item> searchQueryRootItemFactory)
			: this(searchQueryRootItemFactory,
				Locator.Current.GetInstance<ISearchQueryRuleFactory>(),
				Locator.Current.GetInstance<ISearchQueryGroupingFactory>(),
				Locator.Current.GetInstance<ILog>())
		{
		}

		public SearchQueryElementProvider(Func<Item> searchQueryRootItemFactory,
																			ISearchQueryRuleFactory searchQueryRuleFactory,
																			ISearchQueryGroupingFactory searchQueryGroupingFactory,
																			ILog logger)
		{
			if (searchQueryRootItemFactory == null)
				throw new ArgumentNullException(nameof(searchQueryRootItemFactory));
			if (searchQueryRuleFactory == null)
				throw new ArgumentNullException(nameof(searchQueryRuleFactory));
			if (searchQueryGroupingFactory == null)
				throw new ArgumentNullException(nameof(searchQueryGroupingFactory));
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			_searchQueryRootItemFactory = searchQueryRootItemFactory;
			SearchQueryRuleFactory = searchQueryRuleFactory;
			SearchQueryGroupingFactory = searchQueryGroupingFactory;
			_logger = logger;
		}

		public ISearchQueryRuleFactory SearchQueryRuleFactory { get; }
		public ISearchQueryGroupingFactory SearchQueryGroupingFactory { get; }

		public ISearchQueryElement<T> GetSearchQueryElementTree<T>() where T : IIndexableEntity, new()
		{
			Item searchQueryRootItem = _searchQueryRootItemFactory();

			if (searchQueryRootItem == null)
				throw new ArgumentException("The searchQueryRootItem cannot be null");

			if (searchQueryRootItem.IsDerived(Constants.Templates.SearchQueryRoot.TemplateId) == false)
				throw new ArgumentException();

			VerifyConfiguredIndexableEntityType<T>(searchQueryRootItem);

			return GetSearchQueryElementFor<T>(searchQueryRootItem);
		}

		private void VerifyConfiguredIndexableEntityType<T>(Item item) where T : IIndexableEntity, new()
		{
			var configuredIndexableEntityType =
				item.Fields[Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType].Value;

			var cannotFindTypeErrorMessage = $"Cannot find the configured IndexableEntity type <{configuredIndexableEntityType}>";

			Type type;
			try
			{
				type = Type.GetType(configuredIndexableEntityType);
			}
			catch (Exception ex)
			{
				_logger.Error(ex.Message, ex, this);
				throw new InvalidOperationException(cannotFindTypeErrorMessage, ex);
			}

			if (type == null)
			{
				_logger.Error(cannotFindTypeErrorMessage, this);
				throw new ArgumentNullException(cannotFindTypeErrorMessage);
			}

			if (type != typeof(T))
				throw new ArgumentException($"The configured IndexableEntity type <{type}> does not match the specified generic type T: {typeof(T)}");
		}

		private ISearchQueryElement<T> GetSearchQueryElementFor<T>(Item item)
			where T : IIndexableEntity, new()
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

		private SearchQueryRule<T> GetSearchQueryElementFromItem<T>(Item item) where T : IIndexableEntity, new()
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
				associatedPropertyName, configuredComparisonOperator, dynamicValueProvidingParameter, defaultValue
			);
		}

		private SearchQueryGrouping<T> GetSearchQueryGroupingFromItem<T>(Item item) where T : IIndexableEntity, new()
		{
			var configuredLogicalOperator =
				item.Fields[Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator].Value;

			return SearchQueryGroupingFactory.Create<T>(configuredLogicalOperator);
		}
	}
}