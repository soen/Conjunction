using System.Linq;
using Sitecore;
using Sitecore.ContentSearch.SearchTypes;

namespace Conjunction.Foundation.Core.Infrastructure
{
  /// <summary>
  /// Provides extension functionalities for the <see cref="IQueryable{T}"/> type to be used
  /// in the context of working with <see cref="SearchResultItem"/> types.
  /// </summary>
  public static class QueryableExtensions
  {
    public static IQueryable<T> IsContentItem<T>(this IQueryable<T> query) where T : SearchResultItem
    {
      return query.Where(x => x.Paths.Contains(ItemIDs.ContentRoot));
    }

    public static IQueryable<T> IsContextLanguage<T>(this IQueryable<T> query) where T : SearchResultItem
    {
      return query.Where(searchResultItem => searchResultItem.Language.Equals(Context.Language.Name));
    }

    public static IQueryable<T> IsLatestVersion<T>(this IQueryable<T> query) where T : SearchResultItem
    {
      return query.Where(searchResultItem => searchResultItem["_latestversion"].Equals("1"));
    }
  }
}