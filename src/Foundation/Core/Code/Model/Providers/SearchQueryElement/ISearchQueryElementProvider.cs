using Conjunction.Foundation.Core.Model.Factories;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryElement
{
  /// <summary>
  /// Provides functionality to retrieve a <see cref="ISearchQueryElement{T}"/> root element
  /// from a given configuration.
  /// </summary>
  public interface ISearchQueryElementProvider
  {
    ISearchQueryRuleFactory SearchQueryRuleFactory { get; }

    ISearchQueryGroupingFactory SearchQueryGroupingFactory { get; }

    ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new();
  }
}
