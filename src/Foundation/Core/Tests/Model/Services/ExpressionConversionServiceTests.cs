using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Services;
using FluentAssertions;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Services
{
  public class ExpressionConversionServiceTests
  {
    [Fact]
    public void ToEquals_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToEquals(propertySelector, "Foo");

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToEquals_PropertySelectorIsNotNullAndValueIsSet_ReturnsPropertyEqualsValueExpression()
    {
      // Arrange
      var value = 1;
      var indexableEntity = new TestIndexableEntity();
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.SomeInteger.Equals(value);

      // Act
      var actual = ExpressionConversionService.ToEquals<TestIndexableEntity>(x => x.SomeInteger, value);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), 
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void ToGreaterThanOrEqual_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToGreaterThanOrEqual(propertySelector, 1);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToGreaterThanOrEqual_ValueIsNull_ThrowsException()
    {
      // Arrange
      object value = null;
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.SomeInteger;

      // Act
      Action act = () => ExpressionConversionService.ToGreaterThanOrEqual(propertySelector, value);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToGreaterThanOrEqual_PropertySelectorIsNotNullAndValueIsSet_ReturnsPropertyGreaterThanOrEqualsValueExpression()
    {
      // Arrange
      var value = 1;
      var indexableEntity = new TestIndexableEntity();
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.SomeInteger >= value;

      // Act
      var actual = ExpressionConversionService.ToGreaterThanOrEqual<TestIndexableEntity>(x => x.SomeInteger, value);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void ToLessThanOrEqual_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToLessThanOrEqual(propertySelector, 1);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToLessThanOrEqual_ValueIsNull_ThrowsException()
    {
      // Arrange
      object value = null;
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.SomeInteger;

      // Act
      Action act = () => ExpressionConversionService.ToLessThanOrEqual(propertySelector, value);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToLessThanOrEqual_PropertySelectorIsNotNullAndValueIsSet_ReturnsPropertyLessThanOrEqualsValueExpression()
    {
      // Arrange
      var value = 1;
      var indexableEntity = new TestIndexableEntity();
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.SomeInteger <= value;

      // Act
      var actual = ExpressionConversionService.ToLessThanOrEqual<TestIndexableEntity>(x => x.SomeInteger, value);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void ToContains_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToContains(propertySelector, string.Empty);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToContains_PropertySelectorIsNotOfTypeString_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = x => x.SomeInteger;

      // Act
      Action act = () => ExpressionConversionService.ToContains(propertySelector, string.Empty);

      // Assert
      act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void ToContains_PropertySelectorIsNotNullAndValueIsSet_ReturnsPropertyContainsValueExpression()
    {
      // Arrange
      var value = "Foo";
      var indexableEntity = new TestIndexableEntity { Name = value };
      Expression<Func<TestIndexableEntity, bool>> expected = x => x.Name.Contains(value);

      // Act
      var actual = ExpressionConversionService.ToContains<TestIndexableEntity>(x => x.Name, value);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void ToEnumerableContains_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToEnumerableContains(propertySelector, string.Empty);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ToEnumerableContains_PropertySelectorIsNotNullAndValueIsSet_ReturnsEnumerablePropertyContainsValueExpression()
    {
      // Arrange
      var value = new ID(Guid.Empty);
      var indexableEntity = new TestIndexableEntity { Paths = new List<ID> { value } };
      Expression<Func<TestIndexableEntity, object>> expected = x => x.Paths.Contains(value);

      // Act
      var actual = ExpressionConversionService.ToEnumerableContains<TestIndexableEntity>(x => x.Paths, value);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void ToBetween_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionConversionService.ToBetween(propertySelector, string.Empty, string.Empty);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(ToBetween_PropertySelectorTypeAndMinAndMaxValuesAreNotOfMatchingType_ThrowsException_Data))]
    public void ToBetween_PropertySelectorTypeAndMinAndMaxValuesAreNotOfMatchingType_ThrowsException(string propertyName, object minValue, object maxValue)
    {
      // Arrange
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(propertyName);

      // Act
      Action act = () => ExpressionConversionService.ToBetween(propertySelector, minValue, maxValue);

      // Assert
      act.ShouldThrow<InvalidOperationException>();
    }

    public static IEnumerable<object[]> ToBetween_PropertySelectorTypeAndMinAndMaxValuesAreNotOfMatchingType_ThrowsException_Data
    {
      get
      {
        yield return new object[] { "SomeInteger", new DateTime(1985, 09, 14), new DateTime(1987, 12, 16) };
        yield return new object[] { "SomeInteger", DateTime.MinValue, 1 };
        yield return new object[] { "SomeInteger", 1, DateTime.MaxValue };
      }
    }

    [Theory]
    [MemberData(nameof(ToBetween_PropertySelectorIsNotNullAndLowerAndUpperValuesAreSet_Data))]
    public void ToBetween_PropertySelectorIsNotNullAndLowerAndUpperValuesAreSet_ReturnsPropertyBetweenLowerAndUpperValuesExpression(string propertyName, object minValue, object maxValue, Inclusion inclusion)
    {
      // Arrange
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(propertyName);
      var indexableEntity = new TestIndexableEntity
      {
        SomeInteger = 1,
        SomeFloat = 1f,
        SomeDouble = 100,
        CreatedDate = DateTime.Now
      };

      Expression<Func<TestIndexableEntity, object>> expected = x => propertySelector.Between(minValue, maxValue, inclusion);

      // Act
      var actual = ExpressionConversionService.ToBetween(propertySelector, minValue, maxValue, inclusion);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity),
                   expected.Compile().Invoke(indexableEntity));
    }

    public static IEnumerable<object[]> ToBetween_PropertySelectorIsNotNullAndLowerAndUpperValuesAreSet_Data
    {
      get
      {
        yield return new object[] { "CreatedDate", new DateTime(1985, 09, 14), new DateTime(1987, 12, 16), Inclusion.Both };
        yield return new object[] { "SomeInteger", 10, int.MaxValue, Inclusion.Upper };
        yield return new object[] { "SomeDouble", 0.1, 3.14, Inclusion.Lower };
        yield return new object[] { "SomeFloat", -10f, 10f, Inclusion.None };
      }
    }
  }
}
