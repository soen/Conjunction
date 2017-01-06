# Retrieving the Search Results

Now that we have our indexable entity model defined and the search query configured in Sitecore, the final step is to retrieve the search results that matches the configured search query.

## The Three Providers

In order to retrieve the search results, we first need to configure Conjunction to use the correct configuration *providers*:

* Search Query Element Provider
* Search Query Element Value Provider
* Index Name Provider 

The **Search Query Element Provider** is resposible for retrieving a search query root from a given configuration. In this example, we want to retrieve the search query root from the previously configured search query root item in Sitecore, and for this we use the default Sitecore search query element provider implementation that ships with Conjunction.

First we get the search query root item from Sitecore by fetching the search query root item using its ID. Once we have the search query root item, this has to get passed to the Sitecore search query element provider that will transform the configurion of the search query root item into an internal search query element representation that Conjunction can work with:

```csharp
  Item searchQueryRootItem = Sitecore.Context.Database.GetItem("itemId");
  var elementProvider = new SitecoreConfiguredSearchQueryElementProvider(searchQueryRootItem);
```

For retrieving dynamically provided values used by search query rule elements, we use a different provider named a **Search Query Element Value Provider**. In this example, we make use of another provider implementation that comes with Conjunction, which is capable of retrieving values from a query string, named ``QueryStringSearchQueryValueProvider``:

```csharp
  var valueProvider = new QueryStringSearchQueryValueProvider(Request.QueryString);
```

> **Note**: Essentially, the ``QueryStringSearchQueryValueProvider`` works by interpreting the name/value pairs, coming from the query string of a web request as dynamic values, making these ready for the search query rules. Once interpreted, these values gets served to the search query rules needing the value tied together with a given *dynamic value providing parameter*, when the search query is performed.

The last bit of configuration that has to be set up is the **Index Name Provider** that is used to deliver the index name that will be used when performing search queries. In this example, we specify that we want to retrieve results from the default Sitecore master/web index, using the default Sitecore index name provider within Conjunction, named ``SitecoreDefaultIndexNameProvider``:

```csharp
  var indexNameProvider = new SitecoreDefaultIndexNameProvider();
```

> **Note**: For real-life project you should favor using [*domain indexes*](https://soen.ghost.io/tackling-the-challenges-of-architecting-a-search-indexing-infrastructure-in-sitecore-part-2#howshouldthesearchindexesbeorganized), as they give you much smaller, yet concise and cohesive indexes to work with, thus avoiding all of the disadvantages you get by using the more simpler solution of using the default Sitecore indexes. For more information on how you can create your own custom index name provider, see the advanced section [Creating custom providers](../advanced/CreatingCustomProviders.md#a-domain-index-name-provider). 

## The Search Criteria

With the configuration in place, the next step is to pass the configured providers to the **Search Criteria** using the ``SearchCriteria<T>`` type, where the generic type ``T`` is constrained to be a subtype of the ``IndexableEntity`` type:

```csharp
  var criteria = new SearchCriteria<ToyBall>(elementProvider, valueProvider, indexNameProvider);
```

You can think of the search criteria as a object holding information about the search query elements that describes what needs to be queried, how values needed by the search query elements can be retrieved, as well as the search index resposible for delivering the results.

## The Search Results 

Finally, we pass the search criteria to the **Search Result Repository**, using the using the ``SearchResultRepository`` type, that retrieves the search result of type ``SearchResult<T>``. Again, the generic type ``T`` is constrained to be a subtype of the ``IndexableEntity`` type:

```csharp
  var repository = new SearchResultRepository();
  SearchResult<ToyBall> searchResult = repository.GetSearchResult(criteria);
```

When the search result repository has retrieved the search result from the search index, you can then access the number of total hits found, as well as the actual hits, that matches the query performed:

```csharp
  int totalSearchResultCount = searchResult.TotalSearchResults; 
  IEnumerable<ToyBall> searchResults = searchResult.Hits;
```

If you want a more complete example, showing how all the individual pieces fit together in a complete web application, please refer to the [MVC demo example](../introduction/Examples.md#the-mvc-demo-example).

## Next steps

We have now covered the basics of using Conjunction to create an indexable entity model, configure the search query configuration in Sitecore and retrieve the matching search results - however, we have only scratched the surface of what you can do with Conjunction. Therefore, we encourage you to continue exploring how you can achieve even more advanced scenarios, by looking at the different walkthroughs described in the [advanced](../advanced/README.md) section.