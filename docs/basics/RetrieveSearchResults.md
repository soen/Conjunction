# Retrieving the Search Results

Now that we have our indexable entity model defined and the search query configured in Sitecore, the final step is to retrieve the search results that matches the configured search query.

## The Three Providers

In order to retrieve the search results, we first need to configure Conjunction to use the correct configuration *providers*:

* Search Query Element Provider
* Search Query Element Value Provider
* Index Name Provider 

The *Search Query Element Provider* is resposible for retrieving a Search Query Root from a given configuration. In this example, we want to retrieve the Search Query Root from Sitecore, and for this we use the default Sitecore search query element provider implementation that ships with Conjunction.

First we need to get the search query root item from Sitecore. In this case, we simply retrieve the previously configured search query root item, by fetching the item using its ID. Once we have the search query root item, this gets passed to the Sitecore search query element provider that will transform the configuration into an internal representation of the search query elements that Conjunction can work with:

```csharp
    Item searchQueryRootItem = Sitecore.Context.Database.GetItem("itemId");
    var elementProvider = new SitecoreConfiguredSearchQueryElementProvider(searchQueryRootItem);
```

Next, we have the *Search Query Element Value Provider*, which is resposible for retrieving dynamically provided values used by Search Query Rule elements. For the search query rules that needs dynamically provided values, these will be provided by the using the default query string value provider that comes with Conjunction:

```csharp
    var valueProvider = new QueryStringSearchQueryValueProvider(Request.QueryString);
```

The last bit of configuration we have to consider is the *Index Name Provider* that is used to deliver the index name that will be used when performing search queries. In this example, we specify that we want to retrieve results from the default Sitecore master/web index, using the default Sitecore index name provider:

```csharp
    var indexNameProvider = new SitecoreDefaultIndexNameProvider();
```

## The Search Criteria

With the configuration in place, the next step is to pass the configured providers to the search criteria:

```csharp
    var criteria = new SearchCriteria<ToyBall>(elementProvider, valueProvider, indexNameProvider);
```

The search criteria holds information about the search query elements that describes what needs to be queried, how values needed by the search query elements can be retrieved, as well as the search index resposible for delivering the results.

## The Search Results 

Finally, we pass the search criteria to the *Search Result Repository* that retrieves the search result:

```csharp
    var repository = new SearchResultRepository();
    SearchResult<ToyBall> searchResult = repository.GetSearchResult(criteria);
```

When the search result repository has retrieved the search result, you can access the number of total hits found, as well as the actual hits:

```cs
    int totalSearchResultCount = searchResult.TotalSearchResults; 
    IEnumerable<ToyBall> searchResults = searchResult.Hits;
```

## Next steps

We have now covered the basics of using Conjunction to create an indexable entity model, configure the search query configuration in Sitecore and retrieve the matching search results - however, we have only scratched the surface of what you can do with Conjunction. Therefore, we encourage you to continue exploring how you can achieve even more advanced scenarios, by looking at the different walkthroughs described in the [advanced](../advanced/README.md) section.