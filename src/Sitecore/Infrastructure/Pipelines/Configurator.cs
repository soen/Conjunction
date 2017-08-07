using Conjunction.Core;
using Conjunction.Core.Model.Services;
using Conjunction.Sitecore.Model.Factories;
using Sitecore.Pipelines;
using SearchQueryValueConversionService = Conjunction.Sitecore.Model.Services.SearchQueryValueConversionService;

namespace Conjunction.Sitecore.Infrastructure.Pipelines
{
	public class Configurator : IConfigurator
		{
		public void Process(PipelineArgs args)
		{
			RegisterDependencies();
		}

		public virtual void RegisterDependencies()
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
	}
}