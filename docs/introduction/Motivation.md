# Motivation

Most of the content within this section is based on the presentation, [Advanced Search in Sitecore using Solr - Or the story about, how you can leverage the ContentSearch API to create a more generic, configurable way, of working with search queries in Sitecore](https://speakerdeck.com/soen/advanced-search-in-sitecore-using-solr), that was presented at a Sitecore User Group meeting, late 2016, where Conjunction was first presented to the public.

## How we would normally use the search capabilities in Sitecore

When working with search features in Sitecore, we as developers normally use the ContentSearch API. The ContentSearch API makes our life easier, since it provides an abstraction over the low level details of working with native search technologies, such as Lucene, Solr or most recently Azure Search.

> If you haven't worked with the ContentSearch API before, you can get a basic introduction by following my previous two part series [A re-introduction to the ContentSearch API in Sitecore](https://soen.ghost.io/a-re-introduction-to-the-contentsearch-api-in-sitecore-part-1/).

At a very high-level perspective, when implementing a search query, you'll be going through the following steps:

1. Find out what you should query
2. Create your custom search query logic
3. Present the search results to the end-user

For most cases, this might be good enough, and often you might consider the task of presenting the end-user with a search result, as being completed.

## The (hidden) problems that comes along

While we are able to build search queries in Sitecore using the methods just described, there are some hidden issues you'll be discovering, as you use the ContentSearch API.

The first issue is the fact that **your queries only exists in code**, whereas **changes to search queries requires both a developer and a deployment**. As a direct consequence, **the cost of making even small changes are considerable**. Looking at this from the perspective of the client's business the **time to market is slowed down**, and they can't easily adjust to new requirements to gain the competitive advantage. Lastly, the client will also **have a hard time trying to fit personalizations into the picture**, whereas a lot of business value can be lost by not presenting the right search results to the users visiting the site.

## Towards a better way of working with search in Sitecore

With the problems laid out, let's look at what a better way of working with search in Sitecore could look like. First, we would like to **enable search query configuration that requires no code changes**, meaning that pre-configured search queries can be changed at any given time without the need of a developer and deployment of these changes. The search query configuration should **allow specifying nested search query structures for complex queries**, such that the solution can handle various kinds of search query scenarios. Additionally, the search query configuration should **allow a search query to either react to values provided dynamically (from a query string or an external system), or by using preset default values**. The search query configuration should also make it possible to **vary search queries based on personalization**.

Using the existing ContentSearch API functionalites as its backbone, Conjunction attemps to make this better way of working with search in Sitecore possible, through a feature rich and extensible API.