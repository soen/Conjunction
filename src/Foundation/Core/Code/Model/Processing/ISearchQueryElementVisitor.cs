namespace Conjunction.Foundation.Core.Model.Processing
{
  public interface ISearchQueryElementVisitor<T> where T : IndexableEntity, new()
  {
    void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping);

    void VisitSearchQueryGroupingEnd();

    void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule);
  }
}
