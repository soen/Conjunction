using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Factories;

namespace Conjunction.Foundation.Core
{
  internal class Locator
  {
    private static readonly IMutableDependencyResolver MutableDependencyResolver;

    static Locator()
    {
      MutableDependencyResolver = new MutableDependencyResolver();
      Initialize(MutableDependencyResolver);
    }
    
    public static IDependencyResolver Current => MutableDependencyResolver;

    private static void Initialize(IMutableDependencyResolver dependencyResolver)
    {
      dependencyResolver.Register<IComparisonOperatorFactory>(() => new ComparisonOperatorFactory());
      dependencyResolver.Register<ILogicalOperatorFactory>(() => new LogicalOperatorFactory());
      dependencyResolver.Register<ISearchQueryRuleFactory>(() => new SearchQueryRuleFactory());
      dependencyResolver.Register<ISearchQueryGroupingFactory>(() => new SearchQueryGroupingFactory());
    }
  }
}