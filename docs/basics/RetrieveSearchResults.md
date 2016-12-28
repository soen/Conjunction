# Retrieving the search results

Get the search query elements from the Sitecore root item. This can be either a specific item or the data source item provided to this controller rendering. In this case, we simply use a specific item that is fetched using its ID.

```csharp
    Item searchQueryRootItem = Sitecore.Context.Database.GetItem("{D6A82E1F-6687-495E-8243-D865B1BF28BA}");
```

Setup the search criteria using the default Sitecore configured search query element provider. Moreover, if we encounter search query rules that needs dynamically provided values, these will be provided by the default query string value provider. Lastly we specify that we want to retrieve results from the default Sitecore master/web index, using the default Sitecore index name provider.

```csharp
    var searchQueryElementProvider = new SitecoreConfiguredSearchQueryElementProvider(searchQueryRootItem);
    var valueProvider = new QueryStringSearchQueryValueProvider(Request.QueryString);
    var indexNameProvider = new SitecoreDefaultIndexNameProvider();
    var criteria = new SearchCriteria<MyClass>(searchQueryElementProvider, valueProvider, indexNameProvider);
```

Retrieve the search result using the SearchResultRepository

```csharp
    var searchResultRepository = new SearchResultRepository();
    var searchResult = searchResultRepository.GetSearchResult(criteria);

    var totalSearchResultCount = searchResult.TotalSearchResults; 
    var searchResults = searchResult.Hits;
```