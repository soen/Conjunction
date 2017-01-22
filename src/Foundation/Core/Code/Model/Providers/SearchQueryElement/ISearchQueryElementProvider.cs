using Conjunction.Foundation.Core.Model.Factories;

namespace Conjunction.Foundation.Core.Model.Providers.SearchQueryElement
{
  /// <summary>
  /// Provides functionality to retrieve a <see cref="ISearchQueryElement{T}"/> root element
  /// from a given configuration.
  /// </summary>
  public interface ISearchQueryElementProvider
  {
    /// <summary>
    /// Gets the <see cref="ISearchQueryRuleFactory"/> that is associated with the given provider.
    /// </summary>
    ISearchQueryRuleFactory SearchQueryRuleFactory { get; }

    /// <summary>
    /// Gets the <see cref="ISearchQueryGroupingFactory"/> that is associated with the given provider.
    /// </summary>
    ISearchQueryGroupingFactory SearchQueryGroupingFactory { get; }

    /// <summary>
    /// Returns the search query element root.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
    /// <returns>The search query element root</returns>
    ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new();
  }
}