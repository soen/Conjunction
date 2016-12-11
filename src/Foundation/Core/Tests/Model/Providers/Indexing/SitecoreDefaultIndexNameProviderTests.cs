using Conjunction.Foundation.Core.Model.Providers.Indexing;
using FluentAssertions;
using Sitecore.Collections;
using Sitecore.FakeDb.Sites;
using Sitecore.Sites;
using Xunit;

namespace Conjunction.Foundation.Core.Tests.Model.Providers.Indexing
{
  public class SitecoreDefaultIndexNameProviderTests
  {
    [RequireLicense]
    [Theory]
    [InlineData("master")]
    [InlineData("web")]
    public void IndexName_UsingSpecificDatabase_ReturnSitecoreDatabaseIndex(string dbName)
    {
      // Arrange
      var fakeSite = new FakeSiteContext(new StringDictionary { {"database", dbName } });
      using (new SiteContextSwitcher(fakeSite))
      {
        var sut = new SitecoreDefaultIndexNameProvider();

        // Act
        var actual = sut.IndexName;

        // Assert
        actual.Should().BeEquivalentTo($"sitecore_{dbName}_index");
      }
    }
  }
}
