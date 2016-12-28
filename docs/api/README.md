# API Reference

This section documents the complete Conjunction API.

## Namespaces

* ``Conjunction.Foundation.Core.Infrastructure``
* ``Conjunction.Foundation.Core.Infrastructure.TypeConverters``
* ``Conjunction.Foundation.Core.Model``
* ``Conjunction.Foundation.Core.Model.Processing``
* ``Conjunction.Foundation.Core.Model.Processing.Processors``
* ``Conjunction.Foundation.Core.Model.Providers``
* ``Conjunction.Foundation.Core.Model.Providers.Indexing``
* ``Conjunction.Foundation.Core.Model.Providers.SearchQueryElement``
* ``Conjunction.Foundation.Core.Model.Providers.SearchQueryValue``
* ``Conjunction.Foundation.Core.Model.Repositories``
* ``Conjunction.Foundation.Core.Model.Services``

## ``Conjunction.Foundation.Core.Infrastructure``

### Classes
* ``ExpressionUtils``
* ``ItemExtensions``
* ``QueryableExtensions``
* ``TemplateExtensions``

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

## ``Conjunction.Foundation.Core.Infrastructure.TypeConverters``

### Classes

* ``SitecoreIDConverter``

### ``SitecoreIDConverter``

Provides a type converter to convert ID objects to and from various other representations.

**Parent:** ``TypeConverter``

## ``Conjunction.Foundation.Core.Model``

### Classes

* ``ComparisonOperator``
* ``IndexableEntity``
* ``LogicalOperator``
* ``RangeValue``
* ``SearchCriteria<T>``
* ``SearchQueryGrouping<T>``
* ``SearchQueryRule<T>``
* ``SearchResult<T>``

### Interfaces

* ``ISearchQueryElement<T>``

### ``ComparisonOperator``

Represents the set of comparison operators a ``SearchQueryRule<T>`` can be configured to use, when comparing its selected property against its dynamically provided value or default value.

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
Represents the set of logical operators a ``SearchQueryGrouping<T>`` can be configured to use, in order to determine the logical relationship between its children.

**Parent:** ``Enum``

* ``And``  
 The operator represents a "logical AND".  
 **return** ``LogicalOperator``

* ``Or``  
 The operator represents a "logical OR".  
 **return** ``LogicalOperator``

### ``SearchCriteria<T>``
Represents the search criteria that holds information about the search query elements that describes what needs to be queried, how values needed by the search query elements can be retrived, as well as the search index resposible for delivering the results.

**Remarks**:  The search criteria is used when querying the ``SearchResultRepository`` to retrieve a ``SearchResult<T>``.

**Generic type T:** The type of ``IndexableEntity`` implementation to use.

#### Properties

* ``string IndexName get;``
* ``IIndexNameProvider IndexNameProvider get;``
* ``string SearchPath get;``
* ``ISearchQueryElementProvider SearchQueryElementProvider get;``
* ``ISearchQueryElement<T> SearchQueryElementRoot get;``
* ``ISearchQueryValueProvider SearchQueryValueProvider get;``

### ``SearchQueryGrouping<T>``

Represents a search query grouping that defines a group of one or more search query elements, and their logical relationship to each other.

**Implements:** ``ISearchQueryElement<T>``

**Generic type T:** The type of ``IndexableEntity`` implementation to use.

#### Properties

* ``LogicalOperator LogicalOperator get;``
* ``ICollection<ISearchQueryElement<T>> SearchQueryElements get;``

### ``SearchQueryRule<T>``

Represents a search query rule that defines how a given property of type ``T`` should be compared againts either a dynamically provided value or default value.

**Implements:** ``ISearchQueryElement<T>``

**Generic type T:** The type of ``IndexableEntity`` implementation to use.

#### Properties

* ``LogicalOperator LogicalOperator get;``
* ``ICollection<ISearchQueryElement<T>> SearchQueryElements get;``

### ``SearchResult<T>``

Represents the search result returned from querying a specific ``SearchCriteria<T>`` using the ``SearchResultRepository``. 

**Generic type T:** The type of ``IndexableEntity`` implementation to use.

#### Properties

* ``IEnumerable<T> Hits get;``
* ``int TotalSearchResults get;``

### ``ISearchQueryElement<T>``

The base abstraction for all search query elements.

#### Methods

* ``void Accept(ISearchQueryElementVisitor<T> visitor)``  
 *Dispatches to the specific visit method for this search query element type. For example, ``SearchQueryRule<T>`` will call into ``VisitSearchQueryRule``.*  

 **Parameters:** ``ISearchQueryElementVisitor<T> visitor``  
 The visitor to visit this search query element with.

## ``Conjunction.Foundation.Core.Model.Processing``

### Interfaces

* ``ISearchQueryElementVisitor<T>``

### ``ISearchQueryElementVisitor<T>``

Represents a visitor for search query elements.

* ``void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping)``  
 *Visits the SearchQueryGrouping<T> before it visits its children.*  

 **Parameters:** ``SearchQueryGrouping<T> searchQueryGrouping``  
 The search query grouping to visit.

* ``void VisitSearchQueryGroupingEnd()``  
 *Visits the ``SearchQueryGrouping<T>`` after it has visited its children.*

* ``void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule)``  
 *Visits the ``SearchQueryRule<T>``.*  

 **Parameters:** ``SearchQueryRule<T> searchQueryRule``  
 The search query rule to visit.

## ``Conjunction.Foundation.Core.Model.Processing.Processors``

### Classes

* ``SearchQueryPredicateBuilder<T>``

### ``SearchQueryPredicateBuilder<T>``

Represents a visitor that can build up a predicate of type ``Expression<Func<T, bool>>`` from search query elements.

#### Methods

* ``Expression<Func<T, bool>> GetPredicate()``

## ``Conjunction.Foundation.Core.Model.Providers``

### Interfaces
* ``IIndexNameProvider``
* ``ISearchQueryElementProvider``
* ``ISearchQueryValueProvider``

### ``IIndexNameProvider``

Provides functionality to deliver the index name that will be used when performing search queries.

#### Properties

* ``string IndexName get;``

### ``ISearchQueryElementProvider``

Provides functionality to retrieve a ``ISearchQueryElement<T>`` root element from a given configuration.

#### Methods

* ``ISearchQueryElement<T> GetSearchQueryElementRoot<T>()``

### ``ISearchQueryValueProvider``

Provides functionality for retrieving dynamically provided values used by ``SearchQueryRule<T>`` elements.

#### Methods

* ``object GetValueForSearchQueryRule<T>(SearchQueryRule<T> searchQueryRule)``