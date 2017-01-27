using System;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Tests.Model;
using FluentAssertions;
using Sitecore.Data;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Infrastructure
{
  public class ExpressionUtilsTests
  {
    [Fact]
    public void GetPropertySelector_PropertyNameIsEmpty_ThrowsException()
    {
      // Arrange
      string propertyName = "";

      // Act
      Action act = () => ExpressionUtils.GetPropertySelector<string, object>(propertyName);

      // Assert
      act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void GetPropertySelector_PropertyNameIsNull_ThrowsException()
    {
      // Arrange
      string propertyName = null;

      // Act
      Action act = () => ExpressionUtils.GetPropertySelector<string, object>(propertyName);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GetPropertySelector_PropertyNameIsValid_ReturnsPropertySelectorExpression()
    {
      // Arrange
      string propertyName = "Name";
      TestIndexableEntity indexableEntity = new TestIndexableEntity();
      Expression<Func<TestIndexableEntity, object>> expectedPropertySelector = (x => x.Name);

      // Act
      var actual = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(propertyName);

      // Assert
      Assert.Equal(actual.Compile().Invoke(indexableEntity), 
                   expectedPropertySelector.Compile().Invoke(indexableEntity));
    }

    [Fact]
    public void GetPropertyNameFromPropertySelector_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GetPropertyNameFromPropertySelector_PropertySelectorIsSet_ReturnsPropertyName()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = (x => x.Name);

      // Act
      var actual = ExpressionUtils.GetPropertyNameFromPropertySelector(propertySelector);

      // Assert
      actual.ShouldBeEquivalentTo("Name");
    }

    [Fact]
    public void GetPropertyTypeFromPropertySelector_PropertySelectorIsNull_ThrowsException()
    {
      // Arrange
      Expression<Func<TestIndexableEntity, object>> propertySelector = null;

      // Act
      Action act = () => ExpressionUtils.GetPropertyTypeFromPropertySelector(propertySelector);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [InlineData("Name", typeof(string))]
    [InlineData("CreatedDate", typeof(DateTime))]
    [InlineData("ItemID", typeof(ID))]
    [InlineData("SomeInteger", typeof(int))]
    [InlineData("SomeLong", typeof(long))]
    [InlineData("SomeFloat", typeof(float))]
    [InlineData("SomeDouble", typeof(double))]
    [InlineData("SomeGuid", typeof(Guid))]
    public void GetPropertyTypeFromPropertySelector_PropertySelectorIsSet_ReturnsPropertyNameType(string propertyName, Type expectedType)
    {
      // Arrange
      var propertySelector = ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(propertyName);

      // Act
      var actual = ExpressionUtils.GetPropertyTypeFromPropertySelector(propertySelector);

      // Assert
      Assert.Equal(actual, expectedType);
    }
  }
}
