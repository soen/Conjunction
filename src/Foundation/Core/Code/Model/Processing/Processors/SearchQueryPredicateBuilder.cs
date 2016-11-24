using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Conjunction.Foundation.Core.Infrastructure;
using Conjunction.Foundation.Core.Model.Providers;
using Conjunction.Foundation.Core.Model.Services;
using Sitecore.ContentSearch.Linq.Utilities;

namespace Conjunction.Foundation.Core.Model.Processing.Processors
{
  /// <summary>
  /// Represents a visitor that can build up a predicate of type <see cref="Expression{T}" /> 
  /// of <see cref="Func{T, Boolean}" /> from search query elements.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
  public class SearchQueryPredicateBuilder<T> : ISearchQueryElementVisitor<T> where T : IndexableEntity, new()
  {
    private readonly Stack<PredicateBuilderContext> _predicateBuilderContext;
    private readonly ISearchQueryValueProvider _searchQueryValueProvider;

    private Expression<Func<T, bool>> _outputPredicate;
    
    public SearchQueryPredicateBuilder(ISearchQueryValueProvider searchQueryValueProvider)
    {
      _predicateBuilderContext = new Stack<PredicateBuilderContext>();
      _searchQueryValueProvider = searchQueryValueProvider;
    }

    public void VisitSearchQueryGroupingBegin(SearchQueryGrouping<T> searchQueryGrouping)
    {
      Expression<Func<T, bool>> predicate;

      switch (searchQueryGrouping.LogicalOperator)
      {
        case LogicalOperator.And:
          predicate = PredicateBuilder.True<T>();
          break;

        case LogicalOperator.Or:
          predicate = PredicateBuilder.False<T>();
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }

      var builderContext = new PredicateBuilderContext(predicate, searchQueryGrouping.LogicalOperator);
      _predicateBuilderContext.Push(builderContext);
    }

    public void VisitSearchQueryGroupingEnd()
    {
      var builderContext = _predicateBuilderContext.Pop();

      if (_outputPredicate == null)
        _outputPredicate = builderContext.Predicate;
      else
        _outputPredicate = _outputPredicate.And(builderContext.Predicate);
    }

    public void VisitSearchQueryRule(SearchQueryRule<T> searchQueryRule)
    {
      var value = _searchQueryValueProvider.GetValueForSearchQueryRule(searchQueryRule);
      if (value == null)
        return;

      var predicate = GetPredicateFromSearchQueryRule(searchQueryRule, value);
      if (predicate == null)
        return;

      var builderContext = _predicateBuilderContext.Peek();

      switch (builderContext.LogicalOperator)
      {
        case LogicalOperator.And:
          builderContext.Predicate = builderContext.Predicate.And(predicate);          
          break;

        case LogicalOperator.Or:
          builderContext.Predicate = builderContext.Predicate.Or(predicate);
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static Expression<Func<T, bool>> GetPredicateFromSearchQueryRule(SearchQueryRule<T> searchQueryRule, object value)
    {
      Expression<Func<T, bool>> predicate = null;
            
      switch (searchQueryRule.ComparisonOperator)
      {
        case ComparisonOperator.GreaterThanOrEqual:
          predicate = ExpressionConversionService.ToGreaterThanOrEqual(searchQueryRule.PropertySelector, value);
          break;

        case ComparisonOperator.LessThanOrEqual:
          predicate = ExpressionConversionService.ToLessThanOrEqual(searchQueryRule.PropertySelector, value);
          break;

        case ComparisonOperator.Equal:
          predicate = ExpressionConversionService.ToEquals(searchQueryRule.PropertySelector, value);
          break;

        case ComparisonOperator.Contains:
          predicate = GetContainsPredicateFromSearchQueryRule(searchQueryRule, value);
          break;

        case ComparisonOperator.Between:
          var rangeValue = value as RangeValue;
          if (rangeValue != null)
            predicate = ExpressionConversionService.ToBetween(searchQueryRule.PropertySelector, rangeValue.LowerValue, rangeValue.UpperValue);
          break;

        case ComparisonOperator.GreaterThan:
        case ComparisonOperator.LessThan:
        case ComparisonOperator.NotEqual:
        case ComparisonOperator.NotContains:
        case ComparisonOperator.NotBetween:
        default:
          throw new NotSupportedException();
      }
      return predicate;
    }

    private static Expression<Func<T, bool>> GetContainsPredicateFromSearchQueryRule(SearchQueryRule<T> searchQueryRule, object value)
    {
      Expression<Func<T, bool>> predicate;
      
      // There are 3 cases that needs to be handled: 
      //   1) The property is of type String: Perform a 'Contains' operation on the string property
      //   2) The property is of type IEnumerable<>: Perform a 'Contains' operation on the IEnumerable<> property
      //   3) The property is a general type, like ID or Guid: Perform an 'Equals' operation on the property

      Type propertyTypeFromPropertySelector =
        ExpressionUtils.GetPropertyTypeFromPropertySelector(searchQueryRule.PropertySelector);

      if (propertyTypeFromPropertySelector == typeof (string))
      {
        predicate = ExpressionConversionService.ToContains(searchQueryRule.PropertySelector, (string) value);
      }        
      else if (typeof (IEnumerable).IsAssignableFrom(propertyTypeFromPropertySelector))
      {
        predicate = ExpressionConversionService.ToEnumerableContains(searchQueryRule.PropertySelector, value);
      }
      else
        predicate = ExpressionConversionService.ToEquals(searchQueryRule.PropertySelector, value);

      return predicate;
    }

    public Expression<Func<T, bool>> GetPredicate()
    {
      return _outputPredicate;
    }

    /// <summary>
    /// Represents the context being used within the <see cref="SearchQueryPredicateBuilder{T}"/>
    /// </summary>
    private class PredicateBuilderContext
    {
      public PredicateBuilderContext(Expression<Func<T, bool>> predicate, LogicalOperator logicalOperator)
      {
        Predicate = predicate;
        LogicalOperator = logicalOperator;
      }

      public Expression<Func<T, bool>> Predicate { get; set; }

      public LogicalOperator LogicalOperator { get; }
    }
  }
}