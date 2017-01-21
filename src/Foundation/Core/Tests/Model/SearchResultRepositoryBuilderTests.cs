using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using FluentAssertions;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model
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
      actual.IndexNameProvider.Should().BeOfType<SitecoreMasterOrWebIndexNameProvider>();
      actual.SearchQueryPredicateBuilder.Should().BeOfType<SearchQueryPredicateBuilder<TestIndexableEntity>>();
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
      actual.SearchQueryPredicateBuilder.Should().BeOfType<TestPredicateBuilder<TestIndexableEntity>>();
    }
  }
}
