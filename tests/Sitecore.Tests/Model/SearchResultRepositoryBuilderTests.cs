using Conjunction.Core.Model.Providers.SearchQueryElement;
using Conjunction.Core.Model.Providers.SearchQueryValue;
using Conjunction.Sitecore.Model;
using Conjunction.Sitecore.Model.Processing;
using Conjunction.Sitecore.Model.Providers.Indexing;
using FluentAssertions;
using Xunit;

namespace Conjunction.Sitecore.Tests.Model
{
  public class SearchResultRepositoryBuilderTests
  {
    [Theory, DefaultAutoData]
    public void Create_UsingDefaultSettings_ReturnsSearchQueryRepositoryUsingSitecoreMasterOrWebIndexNameProviderAndDefaultPredicateBuilder(
      ISearchQueryElementProvider elementProvider,
      ISearchQueryValueProvider valueProvider)
    {
      // Arrange
      var sut = new SearchResultRepositoryBuilder<TestIndexableEntity>();

      // Act
      var actual = sut.Create(elementProvider, valueProvider);

      // Assert
      actual.IndexNameProvider.Should().BeOfType<MasterOrWebIndexNameProvider>();
      actual.SearchQueryElementVisitor.Should().BeOfType<SearchQueryPredicateBuilder<TestIndexableEntity>>();
    }

    [Theory, DefaultAutoData]
    public void Create_UsingCustomIndexNameProvider_ReturnsSearchQueryRepositoryUsingCustomIndexNameProvider(
      ISearchQueryElementProvider elementProvider,
      ISearchQueryValueProvider valueProvider)
    {
      // Arrange
      var sut = new SearchResultRepositoryBuilder<TestIndexableEntity>();

      // Act
      var actual = sut
        .WithIndexNameProvider<TestIndexNameProvider>()
        .Create(elementProvider, valueProvider);
      
      // Assert
      actual.IndexNameProvider.Should().BeOfType<TestIndexNameProvider>();
    }

    [Theory, DefaultAutoData]
    public void Create_UsingCustomPredicateBuilder_ReturnsSearchQueryRepositoryUsingCustomPredicateBuilder(
      ISearchQueryElementProvider elementProvider,
      ISearchQueryValueProvider valueProvider)
    {
      // Arrange
      var sut = new SearchResultRepositoryBuilder<TestIndexableEntity>();

      // Act
      var actual = sut
        .WithPredicateBuilder<TestPredicateBuilder<TestIndexableEntity>>()
        .Create(elementProvider, valueProvider);

      // Assert
      actual.SearchQueryElementVisitor.Should().BeOfType<TestPredicateBuilder<TestIndexableEntity>>();
    }
  }
}
