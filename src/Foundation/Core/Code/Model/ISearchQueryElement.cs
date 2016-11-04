using Conjunction.Foundation.Core.Model.Processing;

namespace Conjunction.Foundation.Core.Model
{
  public interface ISearchQueryElement<T> where T : IndexableEntity, new()
  {
    void Accept(ISearchQueryElementVisitor<T> visitor);
  }
}
