using Conjunction.Core.Model;
using Sitecore.ContentSearch.SearchTypes;

namespace Conjunction.Sitecore.Model
{
  /// <summary>
  /// Represents the base class of an indexable entity that is used when querying data from the search index.
  /// </summary>
  public abstract class IndexableEntity : SearchResultItem, IIndexableEntity
  {
  }
}