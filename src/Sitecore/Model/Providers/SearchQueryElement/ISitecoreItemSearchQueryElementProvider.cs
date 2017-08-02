using Conjunction.Core.Model;
using Conjunction.Core.Model.Providers.SearchQueryElement;
using Conjunction.Sitecore.Model.Factories;

namespace Conjunction.Sitecore.Model.Providers.SearchQueryElement
{
	/// <summary>
	/// Provides functionality to retrieve a <see cref="ISearchQueryElement{T}"/> tree 
	/// from a given configuration.
	/// </summary>
	public interface ISitecoreItemSearchQueryElementProvider : ISearchQueryElementProvider
	{
		/// <summary>
		/// Gets the <see cref="ISearchQueryRuleFactory"/> that is associated with the given provider.
		/// </summary>
		ISearchQueryRuleFactory SearchQueryRuleFactory { get; }

		/// <summary>
		/// Gets the <see cref="ISearchQueryGroupingFactory"/> that is associated with the given provider.
		/// </summary>
		ISearchQueryGroupingFactory SearchQueryGroupingFactory { get; }
	}
}