namespace Conjunction.Foundation.Core.Model.Providers
{
  public interface ISearchQueryValueProvider
  {
    object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new();
  }
}