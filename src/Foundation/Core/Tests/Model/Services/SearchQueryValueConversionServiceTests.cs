using System;
using System.Collections.Generic;
using Conjunction.Foundation.Core.Model.Services;
using FluentAssertions;
using Sitecore.Data;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Services
{
  public class SearchQueryValueConversionServiceTests
  {
    [Fact]
    public void ToTypedValue_ValueTypeIsNull_ThrowsException()
    {
      // Arrange
      Type valueType = null;
      string rawValue = "1";

      // Acts
      Action act = () => SearchQueryValueConversionService.ToTypedValue(valueType, rawValue);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }
    
    [Fact]
    public void ToTypedValue_ValueCannotBeConvertedIntoValueType_ReturnsNull()
    {
      // Arrange
      Type valueType = typeof(int);
      string rawValue = "Foo";

      // Acts
      var actual = SearchQueryValueConversionService.ToTypedValue(valueType, rawValue);

      // Assert
      actual.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(ToTypedValue_ValueCanBeConvertedIntoValueType_ReturnsTypedValue_Data))]
    public void ToTypedValue_ValueCanBeConvertedIntoValueType_ReturnsTypedValue(Type valueType, string rawValue, object expectedTypedValue)
    {
      // Act
      var actual = SearchQueryValueConversionService.ToTypedValue(valueType, rawValue);

      // Assert
      actual.ShouldBeEquivalentTo(expectedTypedValue);
    }

    public static IEnumerable<object[]> ToTypedValue_ValueCanBeConvertedIntoValueType_ReturnsTypedValue_Data
    {
      get
      {
        yield return new object[] { typeof(int), "1", 1 };
        yield return new object[] { typeof(long), "1", 1 };
        yield return new object[] { typeof(float), "1", 1f };
        yield return new object[] { typeof(double), "1", 1.0 };
        yield return new object[] { typeof(bool), "true", true };
        yield return new object[] { typeof(DateTime), "1985-09-14", new DateTime(1985, 09, 14) };
      }
    }

    [Fact]
    public void ToTypedValue_ValueCanBeConvertedIntoSitecoreIDValueType_ReturnsTypedSitecoreIDValue()
    {
      // Arrange
      Type valueType = typeof(ID);
      string rawValue = "{818A0CA7-1388-48BC-9E07-851314C288CB}";
      ID expected = new ID(rawValue);

      // Act
      var actual = SearchQueryValueConversionService.ToTypedValue(valueType, rawValue);

      // Assert
      actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ToTypedValue_ValueCanBeConvertedIntoEnumerableGenericValueType_ReturnsEnumericGenericTypedValue()
    {
      // Arrange
      Type valueType = typeof(IEnumerable<int>);
      string rawValue = "1";
      int expected = 1;

      // Act
      var actual = SearchQueryValueConversionService.ToTypedValue(valueType, rawValue);

      // Assert
      actual.ShouldBeEquivalentTo(expected);
    }

  }
}
