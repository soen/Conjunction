using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Model.Services;
using FluentAssertions;
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
    public void ToLessThanOrEqual_PropertySelectorIsNotNullAndValueIsSet_ReturnsPropertyToLessThanOrEqualsValueExpression()
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
  }
}
