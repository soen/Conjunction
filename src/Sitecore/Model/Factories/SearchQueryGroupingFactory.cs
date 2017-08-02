using System;
using Conjunction.Core;
using Conjunction.Core.Model;

namespace Conjunction.Sitecore.Model.Factories
{
  /// <summary>
  /// Represents the default factory for building instances of type <see cref="SearchQueryGrouping{T}"/>.
  /// </summary>
  public class SearchQueryGroupingFactory : ISearchQueryGroupingFactory
  {
    public SearchQueryGroupingFactory() 
      : this(Locator.Current.GetInstance<ILogicalOperatorFactory>())
    {
    }

    public SearchQueryGroupingFactory(ILogicalOperatorFactory logicalOperatorFactory)
    {
	    if (logicalOperatorFactory == null) throw new ArgumentNullException(nameof(logicalOperatorFactory));

	    LogicalOperatorFactory = logicalOperatorFactory;
    }

    public ILogicalOperatorFactory LogicalOperatorFactory { get; }

    public SearchQueryGrouping<T> Create<T>(string configuredLogicalOperator) where T : IIndexableEntity, new()
    {
      var logicalOperator = LogicalOperatorFactory.Create(configuredLogicalOperator);
      return new SearchQueryGrouping<T>(logicalOperator);
    }
  }
}