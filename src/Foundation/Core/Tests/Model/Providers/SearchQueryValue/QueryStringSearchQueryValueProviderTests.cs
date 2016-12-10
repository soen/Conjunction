using System;
using System.Collections.Specialized;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;
using FluentAssertions;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Providers.SearchQueryValue
{
  public class QueryStringSearchQueryValueProviderTests
  {
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
      var parameterNameValuePairs = new NameValueCollection();
      var sut = new QueryStringSearchQueryValueProvider(parameterNameValuePairs);

      // Act
      Action act = () => sut.GetValueForSearchQueryRule<TestIndexableEntity>(null);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }
  }
}
