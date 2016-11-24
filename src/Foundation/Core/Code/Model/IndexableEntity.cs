using Sitecore.ContentSearch.SearchTypes;

namespace Conjunction.Foundation.Core.Model
{
  /// <summary>
  /// Represents the base class of an indexable entity that is used when querying data from the search index.
  /// </summary>
  public abstract class IndexableEntity : SearchResultItem
  {
    // TODO: Create computed index field to handle multi-site solution item url's
    //
    // This can be done by From what I've been able to figure out, the most clean way to go about this involves:
    //
    // 1. Getting the item being viewed
    // 2. Getting all sites
    // 3. Finding the first site for which the item falls within the tree path
    //
    // Once the site has been located, you can then use the SiteContextSwitcher to switch over to the site 
    // and work against the correct context. Once the correct context has been identified, use the LinkManager.GetUrl()
    // functionality to get the correct item url in the given context.
    //
    // See: http://firebreaksice.com/sitecore-context-site-resolution/
  }
}