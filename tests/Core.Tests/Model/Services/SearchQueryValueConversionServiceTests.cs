using System;
using System.Collections.Generic;
using Conjunction.Core.Model.Services;
using FluentAssertions;
using Xunit;

namespace Conjunction.Core.Tests.Model.Services
{
  public class SearchQueryValueConversionServiceTests
  {
    [Fact]
    public void ToTypedValue_ValueTypeIsNull_ThrowsException()
    {
      // Arrange
      Type valueType = null;
      string rawValue = "1";
	    var sut = new SearchQueryValueConversionService();

			// Acts
			Action act = () => sut.ToTypedValue(valueType, rawValue);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }
    
    [Fact]
    public void ToTypedValue_ValueCannotBeConvertedIntoValueType_ReturnsNull()
    {
      // Arrange
      Type valueType = typeof(int);
      string rawValue = "Foo";
			var sut = new SearchQueryValueConversionService();
			
      // Acts
      var actual = sut.ToTypedValue(valueType, rawValue);

      // Assert
      actual.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(ToTypedValue_ValueCanBeConvertedIntoValueType_ReturnsTypedValue_Data))]
    public void ToTypedValue_ValueCanBeConvertedIntoValueType_ReturnsTypedValue(Type valueType, string rawValue, object expectedTypedValue)
    {
			// Arrange
			var sut = new SearchQueryValueConversionService();

      // Act
      var actual = sut.ToTypedValue(valueType, rawValue);

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
    public void ToTypedValue_ValueCanBeConvertedIntoEnumerableGenericValueType_ReturnsEnumericGenericTypedValue()
    {
      // Arrange
      Type valueType = typeof(IEnumerable<int>);
      string rawValue = "1";
      int expected = 1;
			var sut = new SearchQueryValueConversionService();

      // Act
      var actual = sut.ToTypedValue(valueType, rawValue);

      // Assert
      actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void TryConvertToRangeValueParts_ValueIsNull_ThrowsException()
    {
      // Arrange
      string value = null;
      Tuple<string, string> rangeValueParts;
			var sut = new SearchQueryValueConversionService();

      // Acts
      Action act = () => sut.TryConvertToRangeValueParts(value, out rangeValueParts);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void TryConvertToRangeValueParts_ValueIsNotInValidRangeFormat_ReturnsNull()
    {
      // Arrange
      string value = "1:2";
      Tuple<string, string> actualRangeValueParts;
			var sut = new SearchQueryValueConversionService();

      // Acts
      var actual = sut.TryConvertToRangeValueParts(value, out actualRangeValueParts);

      // Assert
      actual.Should().BeFalse();
      actualRangeValueParts.Should().BeNull();
    }

    [Theory]
    [InlineData("[1:2]", "1", "2")]
    [InlineData("[1;2]", "1", "2")]
    public void TryConvertToRangeValueParts_ValueIsValidRangeFormat_ReturnsRangeValueParts
      (string value, string firstPart, string secondPart)
    {
      // Arrange
      Tuple<string, string> actualRangeValueParts;
      Tuple<string, string> expectedRangeValueParts = new Tuple<string, string>(firstPart, secondPart);
			var sut = new SearchQueryValueConversionService();

      // Acts
      var actual = sut.TryConvertToRangeValueParts(value, out actualRangeValueParts);

      // Assert
      actual.Should().BeTrue();
      actualRangeValueParts.ShouldBeEquivalentTo(expectedRangeValueParts);
    }
  }
}
