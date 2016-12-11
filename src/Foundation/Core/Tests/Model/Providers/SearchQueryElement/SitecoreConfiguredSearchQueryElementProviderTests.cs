using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using FluentAssertions;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Providers.SearchQueryElement
{
  public class SitecoreConfiguredSearchQueryElementProviderTests
  {
    private const string ValidLogicalOperatorAnd = "And";
    private const string ValidLogicalOperatorOr = "Or";
    private const string ValidComparisonOperatorEquals = "Equal";
    private const string ValidComparisonOperatorGreaterThanOrEqual = "GreaterThanOrEqual";
    private const string ValidComparisonOperatorLessThanOrEqual = "LessThanOrEqual";
    private const string ValidComparisonOperatorContains = "Contains";
    private const string InvalidIndexableEntityTyoe = "InvalidIndexableEntityType";
    private const string InvalidLogicalOperator = "InvalidLogicalOperator";
    private const string InvalidComparisonOperator = "InvalidComparisonOperator";
    private const string ValidPropertyNameForTestIndexableEntityType = "Name";

    [Fact]
    public void Ctor_SearchQueryRootItemIsNull_ThrowsException()
    {
      // Arrange
      Item searchQueryRootItem = null;

      // Act
      Action act = () => new SitecoreConfiguredSearchQueryElementProvider(searchQueryRootItem);

      // Assert
      act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_ItemIsNotSearchQueryRootItem_ThrowsException()
    {
      using (var db = new Db
      {
        // Arrange
        new DbItem("InvalidSearchQueryRootItem")
      })
      {
        var searchQueryRootItem = db.GetItem("/sitecore/content/invalidsearchqueryrootitem");

        // Act
        Action act = () => new SitecoreConfiguredSearchQueryElementProvider(searchQueryRootItem);

        // Assert
        act.ShouldThrow<ArgumentException>();
      }
    }

    [Fact]
    public void GetSearchQueryElementRoot_ConfiguredEntityTypeForSearchQueryRootItemIsNotSet_ThrowsException() /* More specific which exception */
    {
      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperatorAnd }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        Action act = () => sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        act.ShouldThrow<ArgumentException>();
      }
    }

    [Fact]
    public void GetSearchQueryElementRoot_ConfiguredEntityTypeForSearchQueryRootItemIsInvalid_ThrowsException() /* More specific which exception */
    {
      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            { Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, InvalidIndexableEntityTyoe },
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperatorAnd },
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        Action act = () => sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        act.ShouldThrow<ArgumentException>();
      }
    }

    [Fact]
    public void GetSearchQueryElementRoot_LogicalOperatorForSearchQueryGroupingItemIsInvalid_ThrowsException() /* More specific which exception */
    {
      var typeName = GetValidIndexableEntityTypeName();
      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            { Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName },
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, InvalidLogicalOperator }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        Action act = () => sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        act.ShouldThrow<InvalidEnumArgumentException>();
      }
    }

    [Fact]
    public void GetSearchQueryElementRoot_ComparisonOperatorForSearchQueryRuleItemIsInvalid_ThrowsException() /* More specific which exception */
    {
      var typeName = GetValidIndexableEntityTypeName();
      using (var db = new Db
        {
          // Arrange
          new DbItem("SearchQueryRootItem")
          {
            TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
            Fields =
            {
              { Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName },
              { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperatorAnd }
            },

            Children = {
              new DbItem("SearchQueryRuleItem")
              {
                TemplateID = Constants.Templates._SearchQueryRule.TemplateId,
                Fields =
                {
                  { Constants.Fields._SearchQueryRule.ComparisonOperator, InvalidComparisonOperator },
                  { Constants.Fields._SearchQueryRule.AssociatedPropertyName, ValidPropertyNameForTestIndexableEntityType }
                },
              }
            }
          }
        })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        Action act = () => sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        act.ShouldThrow<InvalidEnumArgumentException>();
      }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("PropertyThatDoesNotExistOnTheTestIndexableEntityType")]
    public void GetSearchQueryElementRoot_AssociatedPropertyNameForSearchQueryRuleItemIsInvalid_ThrowsException(string associatedPropertyName) /* More specific which exception */
    {
      var typeName = GetValidIndexableEntityTypeName();
      using (var db = new Db
        {
          // Arrange
          new DbItem("SearchQueryRootItem")
          {
            TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
            Fields =
            {
              { Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName },
              { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperatorAnd }
            },

            Children = {
              new DbItem("SearchQueryRuleItem")
              {
                TemplateID = Constants.Templates._SearchQueryRule.TemplateId,
                Fields =
                {
                  { Constants.Fields._SearchQueryRule.ComparisonOperator, InvalidComparisonOperator }
                },
              }
            }
          }
        })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        Action act = () => sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        act.ShouldThrow<ArgumentException>();
      }
    }

    [Theory]
    [InlineData(ValidLogicalOperatorAnd, LogicalOperator.And)]
    [InlineData(ValidLogicalOperatorOr, LogicalOperator.Or)]
    public void GetSearchQueryElementRoot_NoChildSearchQueryElementItems_ReturnsEmptySearchQueryRoot(string rawlogicalOperator, LogicalOperator logicalOperator)
    {
      var typeName = GetValidIndexableEntityTypeName();

      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            { Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName },
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, rawlogicalOperator }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        var actual = sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        actual.Should().BeOfType(typeof(SearchQueryGrouping<TestIndexableEntity>));
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().LogicalOperator.ShouldBeEquivalentTo(logicalOperator);
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.Count.ShouldBeEquivalentTo(0);
      }
    }

    [Theory]
    [InlineData(ValidComparisonOperatorEquals, ComparisonOperator.Equal, "Name", "Foo", "")]
    [InlineData(ValidComparisonOperatorGreaterThanOrEqual, ComparisonOperator.GreaterThanOrEqual, "CreatedDate", "01-01-0001", "")]
    [InlineData(ValidComparisonOperatorLessThanOrEqual, ComparisonOperator.LessThanOrEqual, "CreatedDate", "30-12-2020", "")]
    [InlineData(ValidComparisonOperatorContains, ComparisonOperator.Contains, "Name", "1", "Bar")]
    public void GetSearchQueryElementRoot_SingleSearchQueryRuleItem_ReturnsSearchQueryRootWithSearchQueryRule(
      string rawComparisonOperator, ComparisonOperator comparisonOperator, string propertyName, string defaultValue, string dynamicValueProvidingParameter)
    {
      var typeName = GetValidIndexableEntityTypeName();
      var propertySelector = GetValidPropertySelectorForTestIndexableEntity(ValidPropertyNameForTestIndexableEntityType);

      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            {Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName},
            {Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperatorAnd}
          },

          Children =
          {
            new DbItem("SearchQueryRuleItem")
            {
              TemplateID = Constants.Templates._SearchQueryRule.TemplateId,
              Fields =
              {
                {Constants.Fields._SearchQueryRule.ComparisonOperator, rawComparisonOperator},
                {Constants.Fields._SearchQueryRule.AssociatedPropertyName, ValidPropertyNameForTestIndexableEntityType},
                {Constants.Fields._SearchQueryRule.DefaultValue, defaultValue},
                {Constants.Fields._SearchQueryRule.DynamicValueProvidingParameter, dynamicValueProvidingParameter}
              },
            }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        var actual = sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.Count.ShouldBeEquivalentTo(1);

        var actualSearchQueryRule = actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.First().As<SearchQueryRule<TestIndexableEntity>>();
        actualSearchQueryRule.ComparisonOperator.ShouldBeEquivalentTo(comparisonOperator);
        actualSearchQueryRule.PropertySelector.ShouldBeEquivalentTo(propertySelector);
        actualSearchQueryRule.DefaultValue.ShouldBeEquivalentTo(defaultValue);
        actualSearchQueryRule.DynamicValueProvidingParameter.ShouldBeEquivalentTo(dynamicValueProvidingParameter);
      }
    }

    [Theory]
    [InlineData(ValidLogicalOperatorAnd, LogicalOperator.And)]
    [InlineData(ValidLogicalOperatorOr, LogicalOperator.Or)]
    public void GetSearchQueryElementRoot_SingleSearchQueryGroupingItem_ReturnsSearchQueryRootWithSearchQueryGrouping(string rawLogicalOperator, LogicalOperator logicalOperator)
    {
      var typeName = GetValidIndexableEntityTypeName();

      using (var db = new Db
      {
        // NOTE: Can't create two items with of different templates but with the same field
        //
        // For more details, see the explaination on issue over at GitHub: 
        // https://github.com/sergeyshushlyapin/Sitecore.FakeDb/issues/132

        // TODO: Look into how one can leverage AutoFixture and customizations to build the
        // item template hierarchy once and for all, such that this can be used across the different
        // unit tests.

        // Arrange
        new DbTemplate("_SearchQueryGrouping", Constants.Templates._SearchQueryGrouping.TemplateId)
        {
          Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator
        },
        new DbTemplate("_IndexableEntityConfigurator", Constants.Templates._IndexableEntityConfigurator.TemplateId)
        {
          Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType
        },
        new DbTemplate("SearchQueryRoot", Constants.Templates.SearchQueryRoot.TemplateId)
        {
          BaseIDs = new[] {
            Constants.Templates._SearchQueryGrouping.TemplateId,
            Constants.Templates._IndexableEntityConfigurator.TemplateId
          }
        },

        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            {Constants.Fields._IndexableEntityConfigurator.ConfiguredIndexableEntityType, typeName},
            {Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, rawLogicalOperator}
          },

          Children =
          {
            new DbItem("SearchQueryGroupingItem")
            {
              TemplateID = Constants.Templates._SearchQueryGrouping.TemplateId,
              Fields =
              {
                {Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, rawLogicalOperator}
              },
            }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        var actual = sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.Count.ShouldBeEquivalentTo(1);

        var actualSearchQueryGrouping = actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.First().As<SearchQueryGrouping<TestIndexableEntity>>();
        actualSearchQueryGrouping.LogicalOperator.ShouldBeEquivalentTo(logicalOperator);
        actualSearchQueryGrouping.SearchQueryElements.Count.ShouldBeEquivalentTo(0);
      }
    }

    private static string GetValidIndexableEntityTypeName()
    {
      return $"{typeof(TestIndexableEntity).FullName},{Assembly.GetExecutingAssembly().GetName().Name}";
    }

    private static Expression<Func<TestIndexableEntity, object>> GetValidPropertySelectorForTestIndexableEntity(string propertyName)
    {
      return ExpressionUtils.GetPropertySelector<TestIndexableEntity, object>(propertyName);
    }
  }
}
