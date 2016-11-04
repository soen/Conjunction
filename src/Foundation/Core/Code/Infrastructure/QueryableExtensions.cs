using System.Linq;
using Sitecore;
using Sitecore.ContentSearch.SearchTypes;

namespace Conjunction.Foundation.Core.Infrastructure
{
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