# Creating Custom Providers

In this section, we'll be looking at how to create your own custom provider implementations for retrieval of search query elements, dynamic values and index names.

## Custom search query element provider

By default, Conjunction ships with a search query element provider that allows Conjunction to consume a Sitecore configured search query root, along with all its children.

If you want to consume other search query root configurations, you can implement your own custom search query element provider by creating a new type that implements the [``ISearchQueryElementProvider``](../api/README.md#isearchqueryelementprovider) interface. 

> **Note**: You can find more inspiration on creating your own implementation by refering to the [``SitecoreSearchQueryElementProvider``](../api/README.md#SitecoreSearchQueryElementProvider) implementation.

## Custom search query element value provider

So far we've seen that Conjunction ships with a default value provider that allows search query rules to fetch dynamically provided values from the query string. However, there may be situations where you don't want to use the query string for providing the dynamic values, whereas you can instead create your own implementation.

There are two ways you can do this:

1. Create a new implementation that implements the [``ISearchQueryValueProvider``](../api/README.md#isearchqueryvalueprovider) interface
2. Extend the [``SearchQueryValueProviderBase``](../api/README.md#searchqueryvalueproviderbase) type

Choosing option one will give you full control over how the value provider works, and whether you want it to react to single or range values, or something completely new. Option two gives you a bit more functionality out-of-the-box, such as converting the dynamic value found from some given source, into a typed single, or ranged, value that the search query rule can use.

In this example, let's assume that we want to create our own custom value provider, capable of delivering values from fields in a custom database. In the database we have a table with two columns, *name* and *value*. Furthermore, assuming that we already have a repository that enables retrieving the value of given row, where the name matches some input. 

Assuming that we want to leverage as much of the built-in functionality as possible, we'll choose to go with option number two for the implementation. This could look something like this: 

```csharp
  using Conjunction.Foundation.Core.Model.Providers.SearchQueryValue;

  public class DatabaseSearchQueryValueProvider : SearchQueryValueProviderBase
  {
    // NOTE: The custom database repository would be some custom logic implemented in your solution
    private readonly ICustomDatabaseRepository _customDatabaseRepository;

    public DatabaseSearchQueryValueProvider(ICustomDatabaseRepository customDatabaseRepository)
    {
      _customDatabaseRepository = customDatabaseRepository;
    }

    // NOTE: We'll need to implement this method responsible for extracting the value required by a given dynamic value providing parameter
    protected override string GetDefaultValueOrDynamicValueProvidedByParameter<T>(SearchQueryRule<T> searchQueryRule) 
      where T : IndexableEntity, new()
    {
      string value;

      if (string.IsNullOrWhiteSpace(searchQueryRule.DynamicValueProvidingParameter))
        value = searchQueryRule.DefaultValue;
      else
      {
        var valueProviderParameterName = searchQueryRule.DynamicValueProvidingParameter;
        value = _customDatabaseRepository.GetValueForName(valueProviderParameterName);
      }

      return value;
    }
  }
}
```

If you want to go with option number one, we recommend that you take a closer look on how the [``SearchQueryValueProviderBase``](../api/README.md#searchqueryvalueproviderbase) and [``NameValuePairSearchQueryValueProvider``](../api/README.md#NameValuePairSearchQueryValueProvider) types are implemented.

## A Domain Index Name Provider

In the [basics](../basics/RetrieveSearchResults.md#the-three-providers) walkthrough there is a note saying, that you should favor using [*domain indexes*](https://soen.ghost.io/tackling-the-challenges-of-architecting-a-search-indexing-infrastructure-in-sitecore-part-2#howshouldthesearchindexesbeorganized), as they give you much smaller, yet concise and cohesive indexes to work with.

The following code snippet shows an example on how you can create your own custom index name provider using the [``IIndexNameProvider``](../api/README.md#iindexnameprovider) interface, which follows the domain index pattern for the toy domain that we have seen in the previous examples:

```csharp
  using Conjunction.Foundation.Core.Model.Providers;

  public class ToyIndexNameProvider : IIndexNameProvider
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

> **Tip**: If all of your domain indexes follows the same naming convention, you could extend this code snippet, such that the constructor takes in the name of a given index, making a more generic domain index name provider - that is up to you.