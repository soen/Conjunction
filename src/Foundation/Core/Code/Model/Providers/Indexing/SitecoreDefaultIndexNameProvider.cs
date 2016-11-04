using Sitecore.Data;

namespace Conjunction.Foundation.Core.Model.Providers.Indexing
{
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