namespace Conjunction.Foundation.Core.Model.Providers
{
  /// <summary>
  /// Provides functionality to retrieve a <see cref="ISearchQueryElement{T}"/> root element
  /// from a given configuration.
  /// </summary>
  public interface ISearchQueryElementProvider
  {
    ISearchQueryElement<T> GetSearchQueryElementRoot<T>() where T : IndexableEntity, new();
  }
}
