# Creating the Indexable Entity Model

Before configuring the search queries within Sitecore, the first thing we have to do is create our own indexable entity model. The indexable entity model is primarily used for two things:

1. For defining the overall indexable entity model that will be used for enforcing, that only valid properties of the indexable entity model implementation can be selected when configuring search query rules
2. For querying data from the search index, this model is used as the source type for the underlying ContentSearch API 

Assuming that you already have a Visual Studio Web project with Conjunction installed using the [Conjunction.Foundation.Core](https://www.nuget.org/packages/Conjunction.Foundation.Core/) package, create a new type that inherits from the ``IndexableEntity`` type named ``ToyBall``:

```csharp
  public class ToyBall : Conjunction.Foundation.Core.Model.IndexableEntity
  {
    // code omitted for now ...    
  }
```

For now we won't include any custom index field properties on the ``ToyBall`` model type, but instead rely on using the properties that ships with the default ``IndexableEntity`` type (which is a subtype of Sitecore's own ``SearchResultItem`` type). However, you can easily extend the indexable entity model type just like you would do so when [creating your own custom ``SearchResultItem``](https://soen.ghost.io/extending-the-default-contentsearch-functionality-in-sitecore#rollingyourowncustomsearchresultitemimplementation) implementation.

Wrapping up, the last thing for you to do before moving on is to deploy the newly created indexable model code to your local Sitecore website IIS folder.

## Next steps

Next, we'll look into how you can configure the search query from Sitecore, using the newly created indexable model.