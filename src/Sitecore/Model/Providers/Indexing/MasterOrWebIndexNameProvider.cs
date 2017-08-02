using Conjunction.Core.Model.Providers.Indexing;
using Sitecore;
using Sitecore.Data;

namespace Conjunction.Sitecore.Model.Providers.Indexing
{
  /// <summary>
  /// Represents the index name provider for Sitecore that, based on the <see cref="Context"/>,
  /// will resolve the either the Master or Web index. 
  /// </summary>
  public class MasterOrWebIndexNameProvider : IIndexNameProvider
  {
    private readonly Database _contentOrContextDatabase;

    public MasterOrWebIndexNameProvider()
    {
      _contentOrContextDatabase = Context.ContentDatabase ?? Context.Database;
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