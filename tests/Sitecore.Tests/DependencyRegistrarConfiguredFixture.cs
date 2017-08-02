using System;
using Conjunction.Core;
using Conjunction.Core.Model.Services;
using Conjunction.Sitecore.Model.Factories;
using SearchQueryValueConversionService = Conjunction.Sitecore.Model.Services.SearchQueryValueConversionService;

namespace Conjunction.Sitecore.Tests
{
	public class DependencyRegistrarConfiguredFixture : IDisposable
	{
		public DependencyRegistrarConfiguredFixture()
		{
			RegisterDependencies();
		}

		private void RegisterDependencies()
		{
			Locator.DependencyRegistrar = (resolver) =>
			{
				resolver.Register<IComparisonOperatorFactory>(() => new ComparisonOperatorFactory());
				resolver.Register<ILogicalOperatorFactory>(() => new LogicalOperatorFactory());
				resolver.Register<ISearchQueryRuleFactory>(() => new SearchQueryRuleFactory());
				resolver.Register<ISearchQueryGroupingFactory>(() => new SearchQueryGroupingFactory());
				resolver.Register<ISearchQueryValueConversionService>(() => new SearchQueryValueConversionService());
			};
		}

		public void Dispose()
		{
			Locator.DependencyRegistrar = null;
		}
	}
}
