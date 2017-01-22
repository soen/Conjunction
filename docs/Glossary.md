# Glossary

This is a glossary of the core terms in Conjunction.

## Indexable Entity

Represents the base class of an indexable entity that is used when querying data from the search index.

## Search Query Element

Represents the base abstraction for all search query elements.

## Search Query Root

Defines the top-level element of the search query element hierarchy.

## Search Query Grouping

Defines a group of one or more search query elements, and their logical relationship to each other.

## Search Query Rule

Defines how a given field in the search index should be queried.

## Index Name Provider

Provides functionality to deliver the index name that will be used when performing search queries.

## Search Query Element Provider

Provides functionality to retrieve a Search Query Root from a given configuration.

## Search Query Element Value Provider

Provides functionality for retrieving dynamically provided values used by Search Query Rule elements.

## Logical Operator

Represents the set of logical operators a Search Query Grouping can be configured to use, in order to determine the logical relationship between its children.

## Comparison Operator

Represents the set of comparison operators a Search Query Rule can be configured to use, when comparing its selected property against its dynamically provided value or default value.