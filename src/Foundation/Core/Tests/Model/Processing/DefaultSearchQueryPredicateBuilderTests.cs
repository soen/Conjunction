using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Processing
{
  public class DefaultSearchQueryPredicateBuilderTests
  {
    [Fact]
    public void Ctor_SearchQueryRootItemFuncIsNull_ThrowsException()
    {
      // Arrange
      ISearchQueryValueProvider valueProvider = null;

      // Act
      Action act = () => new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProvider);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [InlineData(LogicalOperator.And, true)]
    [InlineData(LogicalOperator.Or, false)]
    public void VisitSearchQueryGrouping_EmptySearchQueryGrouping_ReturnExpression(LogicalOperator logicalOperator, bool exceptedBooleanExpressionValue)
    {
      // Arrange
      Expression<Func<TestIndexableEntity, bool>> expected = x => exceptedBooleanExpressionValue;

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      var searchQueryGrouping = new SearchQueryGrouping<TestIndexableEntity>(logicalOperator);
      var indexableEntity = new TestIndexableEntity();

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchQueryGrouping.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void VisitSearchQueryRule_DefaultOrDynamicValueNotFound_ReturnsNullExpression()
    {
      // Arrange
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(x => x.SomeInteger, ComparisonOperator.Equal);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      valueProviderMock.GetValueForSearchQueryRule(searchQueryRule).Returns(x => null);

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchQueryRule.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      actual.Should().BeNull();
    }

    [Fact]
    public void VisitSearchQueryRule_DefaultOrDynamicValueFound_ReturnsExpression()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, bool>> expected = x => (x.SomeInteger == 1);

      var indexableEntity = new TestIndexableEntity();
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(x => x.SomeInteger, ComparisonOperator.Equal);
      var searchRootItem = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.And);
      searchRootItem.SearchQueryElements.Add(searchQueryRule);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      valueProviderMock.GetValueForSearchQueryRule(searchQueryRule).Returns(x => 1);

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchRootItem.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }
  }
}
