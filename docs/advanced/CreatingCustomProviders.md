# Creating Custom Providers

## Custom search query element provider

## Custom search query element value provider

```csharp
  public class CustomDatabaseSearchQueryValueProvider : ISearchQueryValueProvider
  {
    public object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule) where T : IndexableEntity, new()
    {
      throw new System.NotImplementedException();
    }
  }
```

## A Domain Index Name Provider

In the [basics](../basics/RetrieveSearchResults.md#the-three-providers) walkthrough there is a note saying, that you should favor using [*domain indexes*](https://soen.ghost.io/tackling-the-challenges-of-architecting-a-search-indexing-infrastructure-in-sitecore-part-2#howshouldthesearchindexesbeorganized), as they give you much smaller, yet concise and cohesive indexes to work with.

The following code snippet shows an example on how you can create your own custom index name provider, which follows the domain index pattern for the toy domain that we have seen in the previous examples:

```csharp
  public class ToyIndexNameProvider : Conjunction.Foundation.Core.Model.Providers.IIndexNameProvider
  {
    private readonly Sitecore.Data.Database _contentOrContextDatabase;

    public ToyIndexNameProvider()
    {
      _contentOrContextDatabase = Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
    }

    public string IndexName
    {
      get
      {
        string databaseName = _contentOrContextDatabase.Name.ToLowerInvariant();
        return $"toy_{databaseName}_index";
      }
    }
  }
```

In essence, when creating a custom index name provider, the only thing you have to worry about implementing is the property named *IndexName*. In this code snippet, we simply say that we want to return an index name which follows the following naming convention *toy_databaseName_index*. If we are running in the context of the Web database, the name of the index will be *toy_web_index*, etc. 

If all of your domain indexes follows the same naming convention, you could extend this code snippet, such that the constructor takes in the name of a given index, making a more generic domain index name provider - that is up to you.