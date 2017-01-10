using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Factories;

namespace Conjunction.Foundation.Core
{
  internal class Locator
  {
    static Locator()
    {
      Current = new MutableDependencyResolver();
      Initialize(Current);
    }

    public static IDependencyResolver Current { get; }

    private static void Initialize(IDependencyResolver dependencyResolver)
    {
      dependencyResolver.Register<IComparisonOperatorFactory>(() => new ComparisonOperatorFactory());
      dependencyResolver.Register<ILogicalOperatorFactory>(() => new LogicalOperatorFactory());
      dependencyResolver.Register<ISearchQueryRuleFactory>(() => new SearchQueryRuleFactory());
      dependencyResolver.Register<ISearchQueryGroupingFactory>(() => new SearchQueryGroupingFactory());
    }
  }
}