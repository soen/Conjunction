using System;
using System.Collections.Specialized;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using FluentAssertions;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Providers.SearchQueryValue
{
  public class QueryStringSearchQueryValueProviderTests
  {
    private const string TestIndexableEntityPropertyName = "Name";
    private const string TestIndexableEntityPropertySomeInteger = "SomeInteger";
    private const string ParameterName = "$x";

    [Fact]
    public void Ctor_ParameterNameValuePairsIsNull_ThrowsException()
    {
      // Arrange
      NameValueCollection parameterNameValuePairs = null;

      // Act
      Action act = () => new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GetValueForSearchQueryRule_SearchQueryRuleIsNull_ThrowsException()
    {
      // Arrange
      SearchQueryRule<TestIndexableEntity> searchQueryRule = null;
      var parameterNameValuePairs = new NameValueCollection();
      var sut = new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Act
      Action act = () => sut.GetValueForSearchQueryRule(searchQueryRule);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GetValueForSearchQueryRule_SingleDynamicValueProvidedByQueryStringParameter_ReturnsSingleValue()
    {
      // Arrange
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(TestIndexableEntityPropertyName);
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(propertySelector, ComparisonOperator.Equal, ParameterName);
      var parameterValue = "someValue";
      var parameterNameValuePairs = new NameValueCollection { { ParameterName, parameterValue} };
      var sut = new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Act
      var actual = sut.GetValueForSearchQueryRule(searchQueryRule);

      // Assert
      actual.ShouldBeEquivalentTo(parameterValue);
    }

    [Fact]
    public void GetValueForSearchQueryRule_RangeDynamicValueProvidedByQueryStringParameter_ReturnsRangeValue()
    {
      // Arrange            
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(TestIndexableEntityPropertySomeInteger);
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(propertySelector, ComparisonOperator.Between, ParameterName);
      var parameterValue = "[1:2]";
      var parameterNameValuePairs = new NameValueCollection { { ParameterName, parameterValue } };
      var expectedValue = new RangeValue(1, 2);
      var sut = new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Act
      var actual = sut.GetValueForSearchQueryRule(searchQueryRule);

      // Assert
      actual.ShouldBeEquivalentTo(expectedValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("TestDefaultValue")]
    public void GetValueForSearchQueryRule_DynamicValueProvidingParameterNotSetForSearchQueryRule_ReturnsDefaultValue(string defaultValue)
    {
      // Arrange
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(TestIndexableEntityPropertyName);
      var searchQueryRule = new SearchQueryRule<TestIndexableEntity>(propertySelector, ComparisonOperator.Equal, defaultValue: defaultValue);
      var parameterNameValuePairs = new NameValueCollection();
      var sut = new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Act
      var actual = sut.GetValueForSearchQueryRule(searchQueryRule);

      // Assert
      actual.ShouldBeEquivalentTo(defaultValue);
    }
  }
}
