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
      Initialize();
    }
    
    public static IDependencyResolver Current => MutableDependencyResolver;

    private static void Initialize()
    {
      MutableDependencyResolver.Register<IComparisonOperatorFactory>(() => new ComparisonOperatorFactory());
      MutableDependencyResolver.Register<ILogicalOperatorFactory>(() => new LogicalOperatorFactory());
      MutableDependencyResolver.Register<ISearchQueryRuleFactory>(() => new SearchQueryRuleFactory());
      MutableDependencyResolver.Register<ISearchQueryGroupingFactory>(() => new SearchQueryGroupingFactory());
    }
  }
}