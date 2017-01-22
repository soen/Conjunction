# API Reference

This section documents the complete Conjunction API.

<table>
<tbody>
<tr>
<td><a href="#iindexnameprovider">IIndexNameProvider</a></td>
<td><a href="#comparisonoperator">ComparisonOperator</a></td>
</tr>
<tr>
<td><a href="#comparisonoperatorfactory">ComparisonOperatorFactory</a></td>
<td><a href="#icomparisonoperatorfactory">IComparisonOperatorFactory</a></td>
</tr>
<tr>
<td><a href="#ilogicaloperatorfactory">ILogicalOperatorFactory</a></td>
<td><a href="#isearchquerygroupingfactory">ISearchQueryGroupingFactory</a></td>
</tr>
<tr>
<td><a href="#isearchqueryrulefactory">ISearchQueryRuleFactory</a></td>
<td><a href="#logicaloperatorfactory">LogicalOperatorFactory</a></td>
</tr>
<tr>
<td><a href="#searchquerygroupingfactory">SearchQueryGroupingFactory</a></td>
<td><a href="#searchqueryrulefactory">SearchQueryRuleFactory</a></td>
</tr>
<tr>
<td><a href="#indexableentity">IndexableEntity</a></td>
<td><a href="#isearchqueryelementt">ISearchQueryElement&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#logicaloperator">LogicalOperator</a></td>
<td><a href="#isearchqueryelementvisitort">ISearchQueryElementVisitor&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#isearchquerypredicatebuildert">ISearchQueryPredicateBuilder&lt;T&gt;</a></td>
<td><a href="#searchquerypredicatebuildert">SearchQueryPredicateBuilder&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#sitecoremasterorwebindexnameprovider">SitecoreMasterOrWebIndexNameProvider</a></td>
<td><a href="#isearchqueryelementprovider">ISearchQueryElementProvider</a></td>
</tr>
<tr>
<td><a href="#sitecoresearchqueryelementprovider">SitecoreSearchQueryElementProvider</a></td>
<td><a href="#isearchqueryvalueprovider">ISearchQueryValueProvider</a></td>
</tr>
<tr>
<td><a href="#namevaluepairsearchqueryvalueprovider">NameValuePairSearchQueryValueProvider</a></td>
<td><a href="#searchqueryvalueproviderbase">SearchQueryValueProviderBase</a></td>
</tr>
<tr>
<td><a href="#isearchresultrepositoryt">ISearchResultRepository&lt;T&gt;</a></td>
<td><a href="#searchresultrepositoryt">SearchResultRepository&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#searchcriteria">SearchCriteria</a></td>
<td><a href="#searchquerygroupingt">SearchQueryGrouping&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#searchqueryrulet">SearchQueryRule&lt;T&gt;</a></td>
<td><a href="#searchresultt">SearchResult&lt;T&gt;</a></td>
</tr>
<tr>
<td><a href="#searchresultrepositorybuildert">SearchResultRepositoryBuilder&lt;T&gt;</a></td>
<td><a href="#expressionconversionservice">ExpressionConversionService</a></td>
</tr>
<tr>
<td><a href="#searchqueryvalueconversionservice">SearchQueryValueConversionService</a></td>
</tr>
</tbody>
</table>

## ``ComparisonOperator``

Represents the set of comparison operators a <a href="#searchqueryrulet">SearchQueryRule<T></a> can be configured to use, when comparing its selected property against its dynamically provided value or default value.

### Between

The operator represents a "set that contains just the specified" comparison.

### Contains

The operator represents a "contains" comparison.

### Equal

The operator represents a "equal" comparison.

### GreaterThan

The operator represents a "greater than" comparison.

### GreaterThanOrEqual

The operator represents a "greater than or equal" comparison.

### LessThan

The operator represents a "less than" comparison.

### LessThanOrEqual

The operator represents a "less than or equal" comparison.

### NotBetween

The operator represents a "set that contains everything except the specified" comparison.

### NotContains

The operator represents a "not contains" comparison.

### NotEqual

The operator represents a "not equal" comparison.


## ``ComparisonOperatorFactory``

Represents the default factory for building types of <a href="#comparisonoperator">ComparisonOperator</a>.


## ``IComparisonOperatorFactory``

Provides functionality to build types of <a href="#comparisonoperator">ComparisonOperator</a>.

### Create(rawComparisonOperator)

Creates a new <a href="#comparisonoperator">ComparisonOperator</a> based on the raw comparison operator value.

| Name | Description |
| ---- | ----------- |
| rawComparisonOperator | *System.String*<br>The raw logical operator value. |

#### Returns

A comparison operator type.


## ``ILogicalOperatorFactory``

Provides functionality to build types of <a href="#logicaloperator">LogicalOperator</a>.

### Create(rawLogicalOperator)

Creates a new <a href="#logicaloperator">LogicalOperator</a> based on the raw logical operator value.

| Name | Description |
| ---- | ----------- |
| rawLogicalOperator | *System.String*<br>The raw logical operator value. |

#### Returns

A logical operator type.


## ``ISearchQueryGroupingFactory``

Provides functionality to build instances of type <a href="#searchquerygroupingt">SearchQueryGrouping<T></a>.

### Create<T>(configuredLogicalOperator)

Creates a new <a href="#searchquerygroupingt">SearchQueryGrouping<T></a> based on the logical operator.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| configuredLogicalOperator | *System.String*<br>The raw logical operator value. |

#### Returns

A search query grouping instance.

### LogicalOperatorFactory

Gets the <a href="#ilogicaloperatorfactory">ILogicalOperatorFactory</a> that is associated with the given search query grouping factory.


## ``ISearchQueryRuleFactory``

Provides functionality to build instances of type <a href="#searchqueryrulet">SearchQueryRule<T></a>.

### ComparisonOperatorFactory

Gets the <a href="#icomparisonoperatorfactory">IComparisonOperatorFactory</a> that is associated with the given search query rule factory.

### Create<T>(associatedPropertyName, configuredComparisonOperator, dynamicValueProvidingParameter, defaultValue)

Creates a new <a href="#searchqueryrulet">SearchQueryRule<T></a> based on the comparison operator.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| associatedPropertyName | *System.String*<br>The associated property name of T. |
| configuredComparisonOperator | *System.String*<br>The configured comparison operator. |
| dynamicValueProvidingParameter | *System.String*<br>The dynamic value providing parameter (optional). |
| defaultValue | *System.String*<br>The default value (optional). |

#### Returns

A search query rule instance.


## ``LogicalOperatorFactory``

Represents the default factory for building types of <a href="#logicaloperator">LogicalOperator</a>.


## ``SearchQueryGroupingFactory``

Represents the default factory for building instances of type <a href="#searchquerygroupingt">SearchQueryGrouping<T></a>.


## ``SearchQueryRuleFactory``

Represents the default factory for building instances of type <a href="#searchqueryrulet">SearchQueryRule<T></a>.


## ``IndexableEntity``

Represents the base class of an indexable entity that is used when querying data from the search index.


## ``ISearchQueryElement<T>``

The base abstraction for all search query elements.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### Accept(visitor)

Dispatches to the specific visit method for this search query element type. For example, <a href="#searchqueryrulet">SearchQueryRule<T></a> will call into Processing.ISearchQueryElementVisitor<T>.VisitSearchQueryRule(SearchQueryRule<T>).

| Name | Description |
| ---- | ----------- |
| visitor | *Processing.ISearchQueryElementVisitor&lt;T&gt;*<br>The visitor to visit this search query element with. |


## ``LogicalOperator``

Represents the set of logical operators a <a href="#searchquerygroupingt">SearchQueryGrouping<T></a> can be configured to use, in order to determine the logical relationship between its children.

### And

The operator represents a "logical AND".

### Or

The operator represents a "logical OR".


## ``ISearchQueryElementVisitor<T>``

Represents a visitor for search query elements.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### VisitSearchQueryGroupingBegin(searchQueryGrouping)

Visits the <a href="#searchquerygroupingt">SearchQueryGrouping<T></a> before it visits its children.

| Name | Description |
| ---- | ----------- |
| searchQueryGrouping | *SearchQueryGrouping&lt;T&gt;*<br>The search query grouping to visit. |

### VisitSearchQueryGroupingEnd

Visits the <a href="#searchquerygroupingt">SearchQueryGrouping<T></a> after it has visited its children.

### VisitSearchQueryRule(searchQueryRule)

Visits the <a href="#searchqueryrulet">SearchQueryRule<T></a>.

| Name | Description |
| ---- | ----------- |
| searchQueryRule | *SearchQueryRule&lt;T&gt;*<br>The search query rule to visit. |


## ``ISearchQueryPredicateBuilder<T>``

Represents a specialized visitor that can build up a predicate of type Expression<Func<T, bool>> from search query elements.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### GetOutput

Returns the aggregated output produced by the predicate builder.

#### Returns

An aggregated predicate expression.

### SearchQueryValueProvider

Gets the <a href="#isearchqueryvalueprovider">ISearchQueryValueProvider</a> that is associated with the given predicate builder.


## ``SearchQueryPredicateBuilder<T>``

Represents the default <a href="#isearchquerypredicatebuildert">ISearchQueryPredicateBuilder<T></a> implementation.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.


## ``IIndexNameProvider``

Provides functionality to deliver the index name that will be used when performing search queries.

### IndexName

Gets the name representing this index to query.


## ``SitecoreMasterOrWebIndexNameProvider``

Represents the index name provider for Sitecore that, based on the Sitecore.Context, will resolve the either the Master or Web index.


## ``ISearchQueryElementProvider``

Provides functionality to retrieve a <a href="#isearchqueryelementt">ISearchQueryElement<T></a> root element from a given configuration.

### GetSearchQueryElementRoot<T>

Returns the search query element root.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

#### Returns

The search query element root

### SearchQueryGroupingFactory

Gets the <a href="#isearchquerygroupingfactory">ISearchQueryGroupingFactory</a> that is associated with the given provider.

### SearchQueryRuleFactory

Gets the <a href="#isearchqueryrulefactory">ISearchQueryRuleFactory</a> that is associated with the given provider.


## ``SitecoreSearchQueryElementProvider``

Represents a Sitecore configured search query element provider, accepting a Sitecore.Data.Items.Item root, which gets transformed into a <a href="#isearchqueryelementt">ISearchQueryElement<T></a> root.


## ``ISearchQueryValueProvider``

Provides functionality for retrieving dynamically provided values used by <a href="#searchqueryrulet">SearchQueryRule<T></a> elements.

### GetValueForSearchQueryRule<T>(searchQueryRule)

Returns the value needed by the search query rule, which can either be a default or dynamically provided value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| searchQueryRule | *SearchQueryRule&lt;T&gt;*<br>The specified search query rule. |

#### Returns

A typed value.


## ``NameValuePairSearchQueryValueProvider``

Represents a name/value pair based value provider, where the dynamic values required by the <a href="#searchqueryrulet">SearchQueryRule<T></a> elements are resolved from name/value pairs, like query strings etc.


## ``SearchQueryValueProviderBase``

Represents the base class of an search query value provider.

### GetRawDefaultOrDynamicValueProvidedByParameter<T>(searchQueryRule)

Retruns the raw value of either the default or dynamically provided value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| searchQueryRule | *SearchQueryRule&lt;T&gt;*<br>The specifed search query rule |

#### Returns

A raw string value of either the default or dynamically provided value

### GetValueForSearchQueryRule<T>(searchQueryRule)

Returns the value needed by the search query value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| searchQueryRule | *SearchQueryRule&lt;T&gt;*<br>The specifed search query rule |

#### Returns

A typed value


## ``ISearchResultRepository<T>``

Provides functionality to retrievin a <a href="#searchresultt">SearchResult<T></a> from a given <a href="#searchcriteria">SearchCriteria</a>.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### GetSearchResult(searchCriteria)

Performs a query using the provided searchCriteria to retrieve a <a href="#searchresultr">SearchResult<T></a>.

| Name | Description |
| ---- | ----------- |
| searchCriteria | *SearchCriteria*<br> |

#### Returns

A <a href="#searchresultt">SearchResult<T></a> containing the search result

### IndexNameProvider

Gets the <a href="#iindexnameprovider">IIndexNameProvider</a> that is associated with the given search result repository.

### SearchQueryElementProvider

Gets the <a href="#isearchqueryelementprovider">ISearchQueryElementProvider</a> that is associated with the given search result repository.

### SearchQueryPredicateBuilder

Gets the <a href="#isearchquerypredicatebuildert">ISearchQueryPredicateBuilder<T></a> that is associated with the given search result repository.


## ``SearchResultRepository<T>``

Represents the main entry point for retrieving a <a href="#searchresultt">SearchResult<T></a> from a given <a href="#searchcriteria">SearchCriteria</a>.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.


## ``SearchCriteria``

Represents the search criteria used by <a href="#isearchresultrepositoryt">ISearchResultRepository<T></a> implementations

### SearchPath

Gets the search path used to constraint where to look for documents in the search index.


## ``SearchQueryGrouping<T>``

Represents a search query grouping that defines a group of one or more search query elements, and their logical relationship to each other.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### LogicalOperator

Gets the <a href="#logicaloperator">LogicalOperator</a> that is associated with the given search query grouping.

### SearchQueryElements

Gets the search query elements children.


## ``SearchQueryRule<T>``

Represents a search query rule that defines how a given property of type T should be compared againts either a dynamically provided value or default value.

#### Remarks

This class could eventually also include whether to use fuzzy search or not, and if so, how much fuzzyness to use (number).

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### ComparisonOperator

Gets the <a href="#comparisonoperator">ComparisonOperator</a> that is associated with the given search query rule.

### DefaultValue

Gets the default value that is associated with the given search query rule.

### DynamicValueProvidingParameter

Gets the dynamic value providing parameter that is associated with the given search query rule.

### PropertySelector

Gets the property selector that is associated with the given search query rule.


## ``SearchResult<T>``

Represents the search result returned from querying a specific <a href="#searchcriteria">SearchCriteria</a> using the <a href="#searchresultrepositoryt">SearchResultRepository<T></a>.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### Hits

Gets the hits found.

### TotalSearchResults

Gets the total number of search results found.


## ``SearchResultRepositoryBuilder<T>``

Provides functionalities to build new <a href="#isearchresultrepositoryt">ISearchResultRepository<T></a> instances using a specified configuration.


#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

### Create(searchQueryElementProvider, searchQueryValueProvider)

Creates a new <a href="#isearchresultrepositoryt">ISearchResultRepository<T></a> instance using the specified builder configuration.

| Name | Description |
| ---- | ----------- |
| searchQueryElementProvider | *Providers.SearchQueryElement.ISearchQueryElementProvider*<br> |
| searchQueryValueProvider | *Providers.SearchQueryValue.ISearchQueryValueProvider*<br> |


### WithIndexNameProvider<TIndexNameProvider>

Configures the builder to use a given <a href="#iindexnameprovider">IIndexNameProvider</a> type.

#### Type Parameters

- TIndexNameProvider - The type of <a href="#iindexnameprovider">IIndexNameProvider</a> to use.


### WithPredicateBuilder<TSearchQueryPredicateBuilder>

Configures the builder to use a given <a href="#isearchquerypredicatebuildert">ISearchQueryPredicateBuilder<T></a> type.

#### Type Parameters

- TSearchQueryPredicateBuilder - The type of <a href="#isearchquerypredicatebuildert">ISearchQueryPredicateBuilder<T></a> to use.


## ``ExpressionConversionService``

Provides functionalities for constructing expression trees, based on a given property selector and value.

### ToBetween<T>(propertySelector, lowerValue, upperValue, inclusion)

Converts the specified propertySelector to an 'between' expression using the given lowerValue and upperValue.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector |
| lowerValue | *System.Object*<br>The lower-bound value |
| upperValue | *System.Object*<br>The upper-bound value |
| inclusion | *Sitecore.ContentSearch.Linq.Inclusion*<br>The state of how the bounds are included |


### ToContains<T>(propertySelector, value)

Converts the specified propertySelector to an 'contains' expression using the given value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector |
| value | *System.String*<br>The value |

#### Returns

An expression on the form 'nameOfPropertySelector contains value'

### ToEnumerableContains<T>(propertySelector, value)

Converts the specified propertySelector of type System.Collections.IEnumerable to an 'contains' expression using the given value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector (must be of type System.Collections.IEnumerable) |
| value | *System.Object*<br>The value |

#### Returns

An expression on the form 'nameOfPropertySelector contains value'

### ToEquals<T>(propertySelector, value)

Converts the specified propertySelector to an 'equals' expression using the given value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector |
| value | *System.Object*<br>The value |

#### Returns

An expression on the form 'nameOfPropertySelector equals value'

### ToGreaterThanOrEqual<T>(propertySelector, value)

Converts the specified propertySelector to an 'greater-than-or-equals' expression using the given value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector |
| value | *System.Object*<br>The value |

#### Returns

An expression on the form 'nameOfPropertySelector greater-than-or-equals value'

### ToLessThanOrEqual<T>(propertySelector, value)

Converts the specified propertySelector to an 'less-than-or-equals' expression using the given value.

#### Type Parameters

- T - The type of <a href="#indexableentity">IndexableEntity</a> implementation to use.

| Name | Description |
| ---- | ----------- |
| propertySelector | *System.Linq.Expressions.Expression{System.Func{T,System.Object}}*<br>The property selector |
| value | *System.Object*<br>The value |

#### Returns

An expression on the form 'nameOfPropertySelector less-than-or-equals value'


## ``SearchQueryValueConversionService``

Provides functionalities for converting raw System.String values into typed values.

### ToTypedValue(valueType, value)

Converts the value to the specified valueType.

| Name | Description |
| ---- | ----------- |
| valueType | *System.Type*<br>The value type that the specified value needs to be converted into. |
| value | *System.String*<br>The value that needs to be converted. |


### TryConvertToRangeValueParts(value, rangeValueParts)

Tries to convert the specified value into range value parts.

#### Remarks

The output parameter rangeValueParts is intended to be used within the RangeValue type.

| Name | Description |
| ---- | ----------- |
| value | *System.String*<br>The value that needs to be converted. |
| rangeValueParts | *System.Tuple{System.String,System.String}*<br>The range value parts ressembling the range values, if converted. |