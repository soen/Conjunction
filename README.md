# Conjunction [![Build status](https://ci.appveyor.com/api/projects/status/bpm85mumoj38gk4h?svg=true)](https://ci.appveyor.com/project/soen/conjunction)
Conjunction is a utility for Sitecore that solves the issue of creating configurable and personalizable queries for either the Lucene or Solr search engine, using Sitecore's ContentSearch API as the backbone.

## Features
Conjunction has some interesting parts that are worth mentioning, including:

- Enables search query configuration without code changes
- Allows specifying nested search query structures for complex queries
- Allows a search query to either react to values provided dynamically, or by using preset default values
- Makes it possible to vary search queries based on personalization
- Uses the ContentSearch API as the underlying interface for querying search results
- Enables extensibility

If you want to know more about the motivation behind creating this utility, check out my presentation [Advanced Search in Sitecore using Solr - Or the story about, how you can leverage the ContentSearch API to create a more generic, configurable way, of working with search queries in Sitecore](https://speakerdeck.com/soen/advanced-search-in-sitecore-using-solr).

## Installation instructions
Before installing Conjunction, you'll need a working Sitecore 8.0 (or later) instance running on you machince.

- Install Conjunction binaries. This is as simple as adding the Conjunction.Foundation.Core NuGet package to your project.
- Install Conjunction Sitecore package. You can find the required Sitecore package [here](Conjunction.zip), which contains the nessecary Sitecore templates and items required to configure search queries from Sitecore.

## Where to get help
If you have questions or bugs, feel free to open an issue. Alternative, you can also reach me on the Sitecore Community Slack (for now, send me a private message at soren.engel) or on Twitter (@soren_engel).

## Contribution guidelines
Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on the code of conduct, and the process for submitting pull requests.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
