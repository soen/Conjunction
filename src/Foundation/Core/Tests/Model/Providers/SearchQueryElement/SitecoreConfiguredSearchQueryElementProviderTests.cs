using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Conjunction.Foundation.Core.Model;
using Conjunction.Foundation.Core.Model.Providers.SearchQueryElement;
using FluentAssertions;
using Sitecore.FakeDb;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Providers.SearchQueryElement
{
  public class SitecoreConfiguredSearchQueryElementProviderTests
  {
    private const string ValidLogicalOperator = "And";
    private const string ValidComparisonOperator = "Equal";
    private const string InvalidIndexableEntityTyoe = "InvalidIndexableEntityType";
    private const string InvalidLogicalOperator = "InvalidLogicalOperator";

    [Fact]
    public void GetSearchQueryElementRoot_ShouldReturnAnEmptySearchQueryRoot_WhenNoChildSearchQueryElementsAreDefined()
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
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperator }
          }
        }
      })
      {
        var item = db.GetItem("/sitecore/content/searchqueryrootitem");
        var sut = new SitecoreConfiguredSearchQueryElementProvider(item);

        // Act
        var actual = sut.GetSearchQueryElementRoot<TestIndexableEntity>();

        // Assert
        actual.Should().BeOfType(typeof (SearchQueryGrouping<TestIndexableEntity>));
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().LogicalOperator.ShouldBeEquivalentTo(LogicalOperator.And);
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.Count.ShouldBeEquivalentTo(0);
      }
    }

    [Fact]
    public void GetSearchQueryElementRoot_ShouldReturnSearchQueryRootWithChildren_WhenChildrenAreDefined()
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
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperator }
          },

          Children = {
            new DbItem("SearchQueryRuleItem1")
            {
              TemplateID = Constants.Templates._SearchQueryRule.TemplateId,
              Fields =
              {
                { Constants.Fields._SearchQueryRule.ComparisonOperator, ValidComparisonOperator },
                { Constants.Fields._SearchQueryRule.AssociatedPropertyName, "Name" },
                { Constants.Fields._SearchQueryRule.DynamicValueProvidingParameter, "$x" }
              },
            },
            new DbItem("SearchQueryRuleItem2")
            {
              TemplateID = Constants.Templates._SearchQueryRule.TemplateId,
              Fields =
              {
                { Constants.Fields._SearchQueryRule.ComparisonOperator, ValidComparisonOperator },
                { Constants.Fields._SearchQueryRule.AssociatedPropertyName, "CreatedDate" },
                { Constants.Fields._SearchQueryRule.DefaultValue, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) }
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
        actual.Should().BeOfType(typeof(SearchQueryGrouping<TestIndexableEntity>));
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().LogicalOperator.ShouldBeEquivalentTo(LogicalOperator.And);
        actual.As<SearchQueryGrouping<TestIndexableEntity>>().SearchQueryElements.Count.ShouldBeEquivalentTo(2);
      }
    }

    public void GetSearchQueryElementRoot_ShouldReturnSearchQueryRootWithSearchQueryRule_WhenValidSeachQueryRuleIsDefined()
    {
      // TODO: Implement 
    }

    public void GetSearchQueryElementRoot_ShouldFail_WhenInvalidSeachQueryRuleIsDefined()
    {
      // TODO: Implement 
    }

    public void GetSearchQueryElementRoot_ShouldReturnSearchQueryRootWithNestedSearchQueryGroupings_WhenSeachQueryGroupingHierarchyIsDefined()
    {
      // TODO: Implement 
    }

    [Fact]
    public void GetSearchQueryElementRoot_ShouldFail_WhenTheConfiguredEntityTypeIsNotSet()
    {
      using (var db = new Db
      {
        // Arrange
        new DbItem("SearchQueryRootItem")
        {
          TemplateID = Constants.Templates.SearchQueryRoot.TemplateId,
          Fields =
          {
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperator }
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
    public void GetSearchQueryElementRoot_ShouldFail_WhenTheConfiguredEntityTypeIsNotFound()
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
            { Constants.Fields._SearchQueryGrouping.SearchQueryGroupingLogicalOperator, ValidLogicalOperator },
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
    public void GetSearchQueryElementRoot_ShouldFail_WhenTheLogicalOperatorIsInvalid()
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

    private static string GetValidIndexableEntityTypeName()
    {
      return $"{typeof (TestIndexableEntity).FullName},{Assembly.GetExecutingAssembly().GetName().Name}";
    }
  }
}
