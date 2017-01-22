<p align="center">
  <img src="conjunction-logo.png">
</p> 

Conjunction is a utility for Sitecore that solves the issue of creating configurable and personalizable queries for either the Lucene or Solr search engine, using Sitecore's ContentSearch API as the backbone.

[![Build status](https://ci.appveyor.com/api/projects/status/bpm85mumoj38gk4h?svg=true)](https://ci.appveyor.com/project/soen/conjunction)

## Features
Conjunction has some interesting parts that are worth mentioning, as it:

- Enables search query configuration without code changes
- Allows specifying nested search query structures for complex queries
- Allows a search query to either react to values provided dynamically, or by using preset default values
- Makes it possible to vary search queries based on personalization
- Uses the ContentSearch API as the underlying interface for querying search results
- Enables extensibility

## Installation instructions
Before installing Conjunction, you'll need a working Sitecore 8.0 (or later) instance running on your machine.

- Install Conjunction binaries. This is as simple as adding the [Conjunction.Foundation.Core](https://www.nuget.org/packages/Conjunction.Foundation.Core/) NuGet package to your project.
- Install Conjunction Sitecore package. You can find the required Sitecore package [here](https://github.com/soen/Conjunction/blob/master/Conjunction.zip), which contains the nessecary Sitecore templates and items required to configure search queries from Sitecore.

## Documentation
* [Introduction](/docs/introduction/README.md)
* [Basics](/docs/basics/README.md)
* [Advanced](/docs/advanced/README.md)
* [Troubleshooting](/docs/Troubleshooting.md)
* [Glossary](/docs/Glossary.md)
* [API Reference](/docs/api/README.md)
* [Change Log](/CHANGELOG.md)

## License
This project is licensed under the MIT License.
