# Core Concepts

Conjunction consist of a few core concepts:

## Configurable search

![The concept "Configurable Search"](images/conjunction_concept.png)

## Search query root, grouping and rule

When you are building a configurable search query, you are actually building a hierarchy consisting of small search query element parts - or short, search query elements. A search query element can either be a search query root, a search query grouping or a search query rule. 

The search query root is the top-level element of the search query element hierarchy, which defines the most general characteristics of the entire search query configuration. A search query grouping defines a group of one or more search query elements, and their logical relationship to each other. Lastly, the search query rule defines how a given field in the search index should be queried, like *the title should contain the value 'x'* or *the created date should be between the 1st of January and 15th of September*.

## Everything is an item

Like everything else in Sitecore, every part of the search query configuration from within Sitecore is an item.

The idea behind this is to facilite the use of default functionalities found in Sitecore, like setting publishing restrictions on specific parts of a search query (like enabling specific parts of a configured search query on a certain date, or even disabling it) and creation of general search query element item structures that can easily be reused, or cloned and amended, for different purposes. 

## Extensibility as a first-class citizen

By default, Conjunction is delivered with a set of functionalities that allows you to use it directly with your Sitecore instance. However, when Conjunction was being sketched out, the main idea was to make it as extensible as possible, allowing you as a developer to swap out different parts of the API with your own implementations.

For instance, the way search query elements are retrived does not need to come from Sitecore, in fact, you could have another external system that delivers the search query configuration. Another example could be the part of the API that let's you retrieve the search results which is also extendable, meaning that you can provide your own layer around the default implementation to get better handling of caching and alike. These are just some examples of how you can extend the default API that comes with Conjunction, there are a great number of extention points within the API that allows you to tweak and utilize Conjunction to fit your needs.