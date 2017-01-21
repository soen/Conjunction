using System;
using System.Collections.Generic;
using System.Linq;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.Indexing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using Conjunction.Foundation.Core.Model.Repositories;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Sitecore;
using Sitecore.Common;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Repositories
{
  public class SearchResultRepositoryTests
  {
    [Theory, DefaultAutoData]
    public void Ctor_SearchQueryElementProviderIsNull_ThrowsException(
      IIndexNameProvider indexNameProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder)
    {
      // Arrange
      ISearchQueryElementProvider elementProvider = null;
      
      // Act
      Action act = () => new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory, DefaultAutoData]
    public void Ctor_IndexNameProviderIsNull_ThrowsException(
      ISearchQueryElementProvider elementProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder
      )
    {
      // Arrange
      IIndexNameProvider indexNameProvider = null;

      // Act
      Action act = () => new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory, DefaultAutoData]
    public void Ctor_SearchQueryPredicateBuilderIsNull_ThrowsException(
      ISearchQueryElementProvider elementProvider,
      IIndexNameProvider indexNameProvider)
    {
      // Arrange
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder = null;

      // Act
      Action act = () => new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory, DefaultAutoData]
    public void GetSearchResult_NoFilter_ReturnContentItems(
      ISearchQueryElementProvider elementProvider,
      IIndexNameProvider indexNameProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder,
      ISearchIndex searchIndex,
      [Frozen]SearchProvider provider,
      Switcher<SearchProvider> switcher)
    {
      // Arrange
      indexNameProvider
        .IndexName
        .Returns("contentItemTestIndex");

      predicateBuilder
        .GetOutput()
        .Returns(x => true);

      var queryable = new QueryProviderStub<TestIndexableEntity>(new[]
      {
        CreateTestIndexableEntity("Faucet", paths: new List<ID> { ItemIDs.ContentRoot }),
        CreateTestIndexableEntity("Fence")
      }.AsQueryable());

      searchIndex
        .CreateSearchContext()
        .GetQueryable<TestIndexableEntity>()
        .Returns(queryable);

      ContentSearchManager.SearchConfiguration.Indexes[indexNameProvider.IndexName] = searchIndex;

      var sut = new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Act
      var actual = sut.GetSearchResult(new SearchCriteria());

      // Assert
      actual.TotalSearchResults.ShouldBeEquivalentTo(1);
      actual.Hits.First().Paths.Should().Contain(ItemIDs.ContentRoot);
    }

    [Theory, DefaultAutoData]
    public void GetSearchResult_NoFilter_ReturnItemsWithCurrentLanguage(
      ISearchQueryElementProvider elementProvider,
      IIndexNameProvider indexNameProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder,
      ISearchIndex searchIndex,
      [Frozen]SearchProvider provider,
      Switcher<SearchProvider> switcher)
    {
      // Arrange
      indexNameProvider
        .IndexName
        .Returns("languageTestIndex");

      predicateBuilder
        .GetOutput()
        .Returns(x => true);

      var queryable = new QueryProviderStub<TestIndexableEntity>(new[]
      {
        CreateTestIndexableEntity("Faucet", paths: new List<ID> { ItemIDs.ContentRoot }, language: "en"),
        CreateTestIndexableEntity("Fence", paths: new List<ID> { ItemIDs.ContentRoot }, language: "de"),
      }.AsQueryable());

      searchIndex
        .CreateSearchContext()
        .GetQueryable<TestIndexableEntity>()
        .Returns(queryable);

      ContentSearchManager.SearchConfiguration.Indexes[indexNameProvider.IndexName] = searchIndex;

      var sut = new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Act
      var actual = sut.GetSearchResult(new SearchCriteria());

      // Assert
      actual.TotalSearchResults.ShouldBeEquivalentTo(1);
      actual.Hits.First().Language.Should().Contain("en");
    }

    [Theory, DefaultAutoData]
    public void GetSearchResult_NoFilter_ReturnItemsWithLatestVersion(
      ISearchQueryElementProvider elementProvider,
      IIndexNameProvider indexNameProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder,
      ISearchIndex searchIndex,
      [Frozen]SearchProvider provider,
      Switcher<SearchProvider> switcher)
    {
      // Arrange
      indexNameProvider
        .IndexName
        .Returns("languageTestIndex");

      predicateBuilder
        .GetOutput()
        .Returns(x => true);

      var queryable = new QueryProviderStub<TestIndexableEntity>(new[]
      {
        CreateTestIndexableEntity("Faucet", paths: new List<ID> { ItemIDs.ContentRoot }, latestVersion: true),
        CreateTestIndexableEntity("Fence", paths: new List<ID> { ItemIDs.ContentRoot }, latestVersion: false),
      }.AsQueryable());

      searchIndex
        .CreateSearchContext()
        .GetQueryable<TestIndexableEntity>()
        .Returns(queryable);

      ContentSearchManager.SearchConfiguration.Indexes[indexNameProvider.IndexName] = searchIndex;

      var sut = new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Act
      var actual = sut.GetSearchResult(new SearchCriteria());

      // Assert
      actual.TotalSearchResults.ShouldBeEquivalentTo(1);
      actual.Hits.First().Fields["_latestversion"].Should().Be("1");
    }

    [Theory, DefaultAutoData]
    public void GetSearchResult_FilterByPredicate_ReturnItemsFulfillingPredicate(
      ISearchQueryElementProvider elementProvider,
      IIndexNameProvider indexNameProvider,
      ISearchQueryPredicateBuilder<TestIndexableEntity> predicateBuilder,
      ISearchIndex searchIndex,
      [Frozen]SearchProvider provider,
      Switcher<SearchProvider> switcher)
    {
      // Arrange
      elementProvider
        .GetSearchQueryElementRoot<TestIndexableEntity>()
        .Returns(new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.And));

      indexNameProvider
        .IndexName
        .Returns("testIndex");
      
      predicateBuilder
        .GetOutput()
        .Returns(x => x.Name.Contains("F"));

      var queryable = new QueryProviderStub<TestIndexableEntity>(new[]
      {
        CreateTestIndexableEntity("Faucet", paths: new List<ID> {ItemIDs.ContentRoot}),
        CreateTestIndexableEntity("Clamp", paths: new List<ID> {ItemIDs.ContentRoot})
      }.AsQueryable());

      searchIndex
        .CreateSearchContext()
        .GetQueryable<TestIndexableEntity>()
        .Returns(queryable);
      
      ContentSearchManager.SearchConfiguration.Indexes[indexNameProvider.IndexName] = searchIndex;

      var sut = new SearchResultRepository<TestIndexableEntity>(elementProvider, indexNameProvider, predicateBuilder);

      // Act
      var actual = sut.GetSearchResult(new SearchCriteria());

      // Assert
      actual.TotalSearchResults.ShouldBeEquivalentTo(1);
      actual.Hits.First().Name.Should().Contain("F");
    }
    
    private TestIndexableEntity CreateTestIndexableEntity(
      string name,
      string path = Constants.SearchOptions.DefaultSearchPath,
      List<ID> paths = null,
      string language = "en", 
      bool latestVersion = true)
    {
      var entity = new TestIndexableEntity
      {
        Name = name,
        Path = path,
        Paths = paths ?? new List<ID>(),
        Language = language,
        ["_latestversion"] = latestVersion ? "1" : "0"
      };

      return entity;
    }
  }
}
