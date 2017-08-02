using Conjunction.Sitecore.Model.Providers.Indexing;
using FluentAssertions;
using Sitecore.Collections;
using Sitecore.FakeDb.Sites;
using Sitecore.Sites;
using Xunit;

namespace Conjunction.Sitecore.Tests.Model.Providers.Indexing
{
  public class MasterOrWebIndexNameProviderTests
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
        var sut = new MasterOrWebIndexNameProvider();

        // Act
        var actual = sut.IndexName;

        // Assert
        actual.Should().BeEquivalentTo($"sitecore_{dbName}_index");
      }
    }
  }
}
