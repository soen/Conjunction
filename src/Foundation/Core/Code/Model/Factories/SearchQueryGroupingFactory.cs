using Sitecore.Diagnostics;

namespace Conjunction.Foundation.Core.Model.Factories
{
  public class SearchQueryGroupingFactory : ISearchQueryGroupingFactory
  {
    private readonly ILogicalOperatorFactory _logicalOperatorFactory;

    public SearchQueryGroupingFactory() 
      : this(new LogicalOperatorFactory())
    {
    }

    public SearchQueryGroupingFactory(ILogicalOperatorFactory logicalOperatorFactory)
    {
      Assert.ArgumentNotNull(logicalOperatorFactory, "logicalOperatorFactory");

      _logicalOperatorFactory = logicalOperatorFactory;
    }

    public SearchQueryGrouping<T> Create<T>(string configuredLogicalOperator) where T : IndexableEntity, new()
    {
      var logicalOperator = _logicalOperatorFactory.Create(configuredLogicalOperator);
      return new SearchQueryGrouping<T>(logicalOperator);
    }
  }
}