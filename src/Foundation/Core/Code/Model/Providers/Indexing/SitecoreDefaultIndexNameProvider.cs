using Sitecore.Data;

namespace Conjunction.Foundation.Core.Model.Providers.Indexing
{
  /// <summary>
  /// Represents the default index name provider for Sitecore that, based on the <see cref="Sitecore.Context"/>,
  /// will resolve the either the Master or Web index. 
  /// </summary>
  public class SitecoreDefaultIndexNameProvider : IIndexNameProvider
  {
    private readonly Database _contentOrContextDatabase;

    public SitecoreDefaultIndexNameProvider()
    {
      _contentOrContextDatabase = Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
    }

    public string IndexName
    {
      get
      {
        string databaseName = _contentOrContextDatabase.Name.ToLowerInvariant();
        return $"sitecore_{databaseName}_index";
      }
    }
  }
}