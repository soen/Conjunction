# API Reference

This section documents the complete Conjunction API.

## Namespaces

* [Conjunction.Foundation.Core.Infrastructure](#conjunctionfoundationcoreinfrastructure)
* [Conjunction.Foundation.Core.Infrastructure.TypeConverters](#conjunctionfoundationcoreinfrastructuretypeConverters)
* [Conjunction.Foundation.Core.Model](#conjunctionfoundationcoremodel)
* [Conjunction.Foundation.Core.Model.Processing](#conjunctionfoundationcoremodelprocessing)
* [Conjunction.Foundation.Core.Model.Processing.Processors](#conjunctionfoundationcoremodelprocessingprocessors)
* [Conjunction.Foundation.Core.Model.Providers](#conjunctionfoundationcoremodelproviders)
* [Conjunction.Foundation.Core.Model.Providers.Indexing](#conjunctionfoundationcoremodelprovidersindexing)
* [Conjunction.Foundation.Core.Model.Providers.SearchQueryElement](#conjunctionfoundationcoremodelproviderssearchqueryelement)
* [Conjunction.Foundation.Core.Model.Providers.SearchQueryValue](#conjunctionfoundationcoremodelproviderssearchqueryvalue)
* [Conjunction.Foundation.Core.Model.Repositories](#conjunctionfoundationcoremodelrepositories)
* [Conjunction.Foundation.Core.Model.Services](#conjunctionfoundationcoremodelservices)

## Conjunction.Foundation.Core.Infrastructure

### Classes
* [``ExpressionUtils``](#expressionutils)
* [``ItemExtensions``](#itemextensions)
* [``QueryableExtensions``](#queryableextensions)
* [``TemplateExtensions``](#templateextensions)

### ``ExpressionUtils``

Provides functionalities for working with property selector expressions.

#### Methods

* ``string GetPropertyNameFromPropertySelector<T>(Expression<Func<T, object>> propertySelector)``
* ``Expression<Func<TIn, TOut>> GetPropertySelector<TIn, TOut>(string propertyName)``
* ``Type GetPropertyTypeFromPropertySelector<T>(Expression<Func<T, object>> propertySelector)``

### ``ItemExtensions``

Provides extension functionalities for working with Sitecore Item types.

#### Methods

* ``bool IsDerived(this Item item, ID templateId)``

### ``QueryableExtensions``

Provides extension functionalities for the ``IQueryable<T>`` type to be used in the context of working with ``SearchResultItem`` types.

#### Methods

* ``IQueryable<T> IsContentItem<T>(this IQueryable<T> query)``
* ``IQueryable<T> IsContextLanguage<T>(this IQueryable<T> query)``
* ``IQueryable<T> IsLatestVersion<T>(this IQueryable<T> query)``

### ``TemplateExtensions``

Provides extension functionalities for working with Sitecore Template types.

#### Methods

* ``bool IsDerived(this Template template, ID templateId)``

## Conjunction.Foundation.Core.Infrastructure.TypeConverters

### Classes

* [``SitecoreIDConverter``](#sitecoreidconverter)

### ``SitecoreIDConverter``

Provides a type converter to convert ID objects to and from various other representations.

**Parent:** ``TypeConverter``

## Conjunction.Foundation.Core.Model

### Classes

* [``ComparisonOperator``](#comparisonoperator)
* [``IndexableEntity``](#indexableentity)
* [``LogicalOperator``](#logicaloperator)
* [``SearchCriteria<T>``](#searchcriteriat)
* [``SearchQueryGrouping<T>``](#searchquerygroupingt)
* [``SearchQueryRule<T>``](#searchqueryrulet)
* [``SearchResult<T>``](#searchresultt)

### Interfaces

* [``ISearchQueryElement<T>``](#isearchqueryelementt)

### ``ComparisonOperator``

Represents the set of comparison operators a [``SearchQueryRule<T>``](#searchqueryrulet) can be configured to use, when comparing its selected property against its dynamically provided value or default value.

**Parent:** ``Enum``

#### Fields

* ``GreaterThan``  
 *The operator represents a "greater than" comparison.*  
 **return** ``ComparisonOperator``

* ``LessThan``  
 *The operator represents a "less than" comparison.*  
 **return** ``ComparisonOperator``

* ``GreaterThanOrEqual``  
 *The operator represents a "greater than or equal" comparison.*  
 **return** ``ComparisonOperator``

* ``LessThanOrEqual``  
 *The operator represents a "less than or equal" comparison.*  
 **return** ``ComparisonOperator``

* ``Equal``  
 *The operator represents a "equal" comparison.*  
 **return** ``ComparisonOperator``

* ``NotEqual``  
 *The operator represents a "not equal" comparison.*  
 **return** ``ComparisonOperator``

* ``Contains``  
 *The operator represents a "contains" comparison.*  
 **return** ``ComparisonOperator``

* ``NotContains``  
 *The operator represents a "not contains" comparison.*  
 **return** ``ComparisonOperator``

* ``Between``  
 *The operator represents a "set that contains just the specified" comparison.*  
 **return** ``ComparisonOperator``

* ``NotBetween``  
The operator represents a "set that contains everything except the specified" comparison.  
**return** ``ComparisonOperator``

### ``IndexableEntity``
Represents the base class of an indexable entity that is used when querying data from the search index.

**Parent:** ``SearchResultItem``

### ``LogicalOperator``
Represents the set of logical operators a [``SearchQueryGrouping<T>``](#searchquerygroupingt) can be configured to use, in order to determine the logical relationship between its children.

**Parent:** ``Enum``

* ``And``  
 The operator represents a "logical AND".  
 **return** ``LogicalOperator``

* ``Or``  
 The operator represents a "logical OR".  
 **return** ``LogicalOperator``

### ``SearchCriteria<T>``
Represents the search criteria that holds information about the search query elements that describes what needs to be queried, how values needed by the search query elements can be retrived, as well as the search index resposible for delivering the results.

**Remarks**:  The search criteria is used when querying the [``SearchResultRepository``](#searchresultrepository) to retrieve a [``SearchResult<T>``](#searchresultt).

**Generic type T:** The type of [``IndexableEntity``](#indexableentity) implementation to use.

#### Properties

* ``string IndexName get;``
* [``IIndexNameProvider``](#iindexnameprovider) ``IndexNameProvider get;``
* ``string SearchPath get;``
* [``ISearchQueryElementProvider``](#isearchqueryelementprovider) ``SearchQueryElementProvider get;``
* [``ISearchQueryElement<T>``](#isearchqueryelementt) ``SearchQueryElementRoot get;``
* [``ISearchQueryValueProvider``](#isearchqueryvalueprovider) ``SearchQueryValueProvider get;``

### ``SearchQueryGrouping<T>``

Represents a search query grouping that defines a group of one or more search query elements, and their logical relationship to each other.

**Implements:** [``ISearchQueryElement<T>``](#isearchqueryelementt)

**Generic type T:** The type of [``IndexableEntity``](#indexableentity) implementation to use.

#### Properties

* [``LogicalOperator``](#logicaloperator) ``LogicalOperator get;``
* ``ICollection<``[``ISearchQueryElement<T>``](#isearchqueryelementt)``> SearchQueryElements get;``

### ``SearchQueryRule<T>``

Represents a search query rule that defines how a given property of type ``T`` should be compared againts either a dynamically provided value or default value.

**Implements:** [``ISearchQueryElement<T>``](#isearchqueryelementt)

**Generic type T:** The type of [``IndexableEntity``](#indexableentity) implementation to use.

#### Properties

* ``Expression<Func<T, object>> PropertySelector get;``
* [``ComparisonOperator``](#comparisonoperator) ``ComparisonOperator get;``
* ``string DynamicValueProvidingParameter get;``
* ``string DefaultValue get;``

### ``SearchResult<T>``

Represents the search result returned from querying a specific [``SearchCriteria<T>``](#searchcriteriat) using the [``SearchResultRepository``](#searchresultrepository). 

**Generic type T:** The type of [``IndexableEntity``](#indexableentity) implementation to use.

#### Properties

* ``IEnumerable<T> Hits get;``
* ``int TotalSearchResults get;``

### ``ISearchQueryElement<T>``

The base abstraction for all search query elements.

#### Methods

* ``void Accept(ISearchQueryElementVisitor<T> visitor)``  
 *Dispatches to the specific visit method for this search query element type. For example, [``SearchQueryRule<T>``](#searchqueryrulet) will call into ``VisitSearchQueryRule``.*  

 **Parameters:**   
 [``ISearchQueryElementVisitor<T>``](#isearchqueryelementvisitort) visitor: The visitor to visit this search query element with.

## Conjunction.Foundation.Core.Model.Processing

### Interfaces

* [``ISearchQueryElementVisitor<T>``](#isearchqueryelementvisitort)

### ``ISearchQueryElementVisitor<T>``

Represents a visitor for search query elements.

* ``void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping)``  
 *Visits the [``SearchQueryGrouping<T>``](#searchquerygroupingt) before it visits its children.*  

 **Parameters:**   
 [``SearchQueryGrouping<T>``](#searchquerygroupingt) ``searchQueryGrouping``: The search query grouping to visit.

* ``void VisitSearchQueryGroupingEnd()``  
 *Visits the [``SearchQueryGrouping<T>``](#searchquerygroupingt) after it has visited its children.*

* ``void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule)``  
 *Visits the [``SearchQueryRule<T>``](#searchqueryrulet)`.*  

 **Parameters:**  
 [``SearchQueryRule<T>``] ``searchQueryRule``: The search query rule to visit.

## Conjunction.Foundation.Core.Model.Processing.Processors

### Classes

* [``SearchQueryPredicateBuilder<T>``](#searchquerypredicatebuildert)

### ``SearchQueryPredicateBuilder<T>``

Represents a visitor that can build up a predicate of type ``Expression<Func<T, bool>>`` from search query elements.

#### Methods

* ``Expression<Func<T, bool>> GetPredicate()``

## Conjunction.Foundation.Core.Model.Providers

### Interfaces

* [``IIndexNameProvider``](#iindexnameprovider)
* [``ISearchQueryElementProvider``](#isearchqueryelementprovider)
* [``ISearchQueryValueProvider``](#isearchqueryvalueprovider)

### ``IIndexNameProvider``

Provides functionality to deliver the index name that will be used when performing search queries.

#### Properties

* ``string IndexName get;``

### ``ISearchQueryElementProvider``

Provides functionality to retrieve a [``ISearchQueryElement<T>``](#isearchqueryelementt) root element from a given configuration.

#### Methods

* [``ISearchQueryElement<T>``](#isearchqueryelementt) ``GetSearchQueryElementRoot<T>()``

### ``ISearchQueryValueProvider``

Provides functionality for retrieving dynamically provided values used by [``SearchQueryRule<T>``](#searchqueryrulet) elements.

#### Methods

* ``object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule)``

## Conjunction.Foundation.Core.Model.Providers.Indexing

### Classes

* [``SitecoreDefaultIndexNameProvider``](#sitecoredefaultindexnameprovider)

### ``SitecoreDefaultIndexNameProvider``

Represents the default index name provider for Sitecore that, based on the ``Context``, will resolve the either the Master or Web index.

**Implements:** [``IIndexNameProvider``](#iindexnameprovider)

#### Properties

* ``string IndexName get;``

## Conjunction.Foundation.Core.Model.Providers.SearchQueryElement

### Classes

* [``SitecoreConfiguredSearchQueryElementProvider``](#sitecoreconfiguredsearchqueryelementprovider)

### ``SitecoreConfiguredSearchQueryElementProvider``

Represents a Sitecore configured search query element provider, accepting a Item root, which gets transformed into a [``ISearchQueryElement<T>``](#isearchqueryelementt) root.

**Implements:** [``ISearchQueryElementProvider``](#isearchqueryelementprovider)

##  Conjunction.Foundation.Core.Model.Providers.SearchQueryValue

### Classes

* [``QueryStringSearchQueryValueProvider``](#querystringsearchqueryvalueprovider)

### ``QueryStringSearchQueryValueProvider``

Represents a query string based value provider, where the dynamic values required by the [``SearchQueryRule<T>``](#searchqueryrulet) elements are resolved from query string name/value pairs.

**Implements:** [``ISearchQueryValueProvider``](#isearchqueryvalueprovider)

## Conjunction.Foundation.Core.Model.Repositories

### Classes

* [``SearchResultRepository``](#searchresultrepository)

### ``SearchResultRepository``

Represents the main entry point for retrieving a [``SearchResult<T>``](#searchresultt) from a given [``SearchCriteria<T>``](#searchcriteriat).

#### Methods

* ``SearchResult<T> GetSearchResult<T>(SearchCriteria<T> searchCriteria)``  
 *Performs a query using the provided searchCriteria to retrieve a [``SearchResult<T>``](#searchresultt).*  
 **Returns**: [``SearchResult<T>``](#searchresultt)

## Conjunction.Foundation.Core.Model.Services

### Classes

 * [``ExpressionConversionService``](#expressionconversionservice)
 * [``SearchQueryValueConversionService``](#searchqueryvalueconversionservice)

### ``ExpressionConversionService``

Provides functionalities for constructing expression trees, based on a given property selector and value.

#### Methods

* ``Expression<Func<T, bool>> ToBetween<T>(Expression<Func<T, object>> propertySelector, object lowerValue, object upperValue, Inclusion inclusion)``
* ``Expression<Func<T, bool>> ToContains<T>(Expression<Func<T, object>> propertySelector, string value)``
* ``Expression<Func<T, bool>> ToEnumerableContains<T>(Expression<Func<T, object>> propertySelector, object value)``
* ``Expression<Func<T, bool>> ToEquals<T>(Expression<Func<T, object>> propertySelector, object value)``
* ``Expression<Func<T, bool>> ToGreaterThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)``
* ``Expression<Func<T, bool>> ToLessThanOrEqual<T>(Expression<Func<T, object>> propertySelector, object value)``

### ``SearchQueryValueConversionService``

Provides functionalities for converting raw String values into typed values.

#### Methods

* ``object ToTypedValue(Type valueType, string value)``  
 *Converts the value to the specified valueType.*  
 **Parameters:**  
 ``Type valueType``: The value type that the specified value needs to be converted into.  
 ``string value``: The value that needs to be converted.  

* ``bool TryConvertToRangeValueParts(string value, Tuple<string, string> rangeValueParts)``