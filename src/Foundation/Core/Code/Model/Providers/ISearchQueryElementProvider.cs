namespace Conjunction.Foundation.Core.Model.Providers
{
  public interface ISearchQueryElementProvider
  {
    ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new();
  }
}
