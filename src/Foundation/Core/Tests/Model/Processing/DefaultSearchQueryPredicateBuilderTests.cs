using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Processing;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using FluentAssertions;
using NSubstitute;
using Sitecore.Data;
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

      var indexableEntity = new TestIndexableEntity();
      var searchQueryGrouping = new SearchQueryGrouping<TestIndexableEntity>(logicalOperator);
      

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchQueryGrouping.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void VisitSearchQueryGrouping_NestedSearchQueryGrouping_ReturnExpression()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, bool>> expected = x => false && true;

      var indexableEntity = new TestIndexableEntity();

      var searchQueryRoot = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.And);
      var searchQueryGrouping = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.Or);
      searchQueryRoot.SearchQueryElements.Add(searchQueryGrouping);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchQueryRoot.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void VisitSearchQueryGrouping_WithRules_ReturnExpression()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, bool>> expected = x => (x.SomeInteger == 1 || x.SomeBoolean == false);

      var indexableEntity = new TestIndexableEntity();

      var integerEqualsRule = new SearchQueryRule<TestIndexableEntity>(x => x.SomeInteger, ComparisonOperator.Equal, null, "1");
      var booleanEqualsRule = new SearchQueryRule<TestIndexableEntity>(x => x.SomeBoolean, ComparisonOperator.Equal, null, "false");

      var searchQueryGrouping = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.Or);
      searchQueryGrouping.SearchQueryElements.Add(integerEqualsRule);
      searchQueryGrouping.SearchQueryElements.Add(booleanEqualsRule);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      valueProviderMock.GetValueForSearchQueryRule(integerEqualsRule).Returns(1);
      valueProviderMock.GetValueForSearchQueryRule(booleanEqualsRule).Returns(false);

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchQueryGrouping.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), expected.Compile().Invoke(indexableEntity));
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
      var value = 1;
      Expression<Func<TestIndexableEntity, bool>> expected = x => (x.SomeInteger == value);

      var indexableEntity = new TestIndexableEntity();
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(x => x.SomeInteger, ComparisonOperator.Equal);
      var searchRootItem = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.And);
      searchRootItem.SearchQueryElements.Add(searchQueryRule);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      valueProviderMock.GetValueForSearchQueryRule(searchQueryRule).Returns(x => value);

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchRootItem.Accept(sut);
      Expression<Func<TestIndexableEntity, bool>> actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void VisitSearchQueryRule_PropertyIsOfTypeString_ReturnsContainsExpression()
    {
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.Name;
      var value = "StringValue";
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.Name.Contains(value);
      var indexableEntity = new TestIndexableEntity { Name = value };

      VisitSearchQueryRule_PropertyOfType_ReturnsExpression(propertySelector, indexableEntity, value, expected);
    }

    [Fact]
    public void VisitSearchQueryRule_PropertyIsOfTypeEnumerable_ReturnsContainsExpression()
    {
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.Paths;
      var itemId = new ID(Guid.NewGuid());
      Expression<Func<TestIndexableEntity, bool>> expected = x => (x.Paths.Contains(itemId));
      var indexableEntity = new TestIndexableEntity { Paths = new List<ID> { itemId } };
      
      VisitSearchQueryRule_PropertyOfType_ReturnsExpression(propertySelector, indexableEntity, itemId, expected);
    }

    [Fact]
    public void VisitSearchQueryRule_PropertyIsOfNonStringType_ReturnsEqualsExpression()
    {
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.ItemId;
      var itemId = new ID(Guid.NewGuid());
      var indexableEntity = new TestIndexableEntity {  ItemId = itemId };
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.ItemId == itemId;

      VisitSearchQueryRule_PropertyOfType_ReturnsExpression(propertySelector, indexableEntity, itemId, expected);
    }

    private void VisitSearchQueryRule_PropertyOfType_ReturnsExpression(Expression<Func<TestIndexableEntity, object>> propertySelector, 
                                                                       TestIndexableEntity indexableEntity, 
                                                                       object value, 
                                                                       Expression<Func<TestIndexableEntity, bool>> expected)
    {
      // Arrange
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(propertySelector, ComparisonOperator.Contains);

      var searchRootItem = new SearchQueryGrouping<TestIndexableEntity>(LogicalOperator.And);
      searchRootItem.SearchQueryElements.Add(searchQueryRule);

      var valueProviderMock = Substitute.For<ISearchQueryValueProvider>();
      valueProviderMock.GetValueForSearchQueryRule(searchQueryRule).Returns(value);

      var sut = new DefaultSearchQueryPredicateBuilder<TestIndexableEntity>(valueProviderMock);

      // Act
      searchRootItem.Accept(sut);
      var actual = sut.GetOutput();

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), expected.Compile().Invoke(indexableEntity));
    }
  }
}
