using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sitecore.ContentSearch.Linq;

namespace Conjunction.Sitecore.Tests
{
  /// <summary>
  /// Solution to the wretched "There is no method 'GetResults' on type 'Sitecore.ContentSearch.Linq.QueryableExtensions' 
  /// that matches the specified arguments" error when unit testing search. Sitecore is tightly coupled to their own 
  /// implemention of IQueryProvider which contains an 'Execute' method not found on standard .Net implementions.
  /// </summary>
  /// <remarks>
  /// Courtesy of https://gist.github.com/vivianroberts/1a632d8bfb8fa62e23c4679b086728ce
  /// </remarks>
  public class QueryProviderStub<TElement> : IOrderedQueryable<TElement>, IOrderedQueryable, IQueryProvider
  {
    private readonly EnumerableQuery<TElement> _innerQueryable;

    public Type ElementType => ((IQueryable)_innerQueryable).ElementType;

    public Expression Expression => ((IQueryable)_innerQueryable).Expression;

    public IQueryProvider Provider => this;

    public QueryProviderStub(IEnumerable<TElement> enumerable)
    {
      _innerQueryable = new EnumerableQuery<TElement>(enumerable);
    }

    public QueryProviderStub(Expression expression)
    {
      _innerQueryable = new EnumerableQuery<TElement>(expression);
    }

    public IQueryable CreateQuery(Expression expression)
    {
      return new QueryProviderStub<TElement>(
        (IEnumerable<TElement>)((IQueryProvider)_innerQueryable).CreateQuery(expression)
      );
    }

    public IQueryable<TElement1> CreateQuery<TElement1>(Expression expression)
    {
      return (IQueryable<TElement1>)new QueryProviderStub<TElement>(
        (IEnumerable<TElement>)((IQueryProvider)_innerQueryable).CreateQuery(expression)
      );
    }

    public object Execute(Expression expression)
    {
      throw new NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
      var items = this.ToArray();
      object results = new SearchResults<TElement>(items.Select(s => new SearchHit<TElement>(0, s)), items.Length);
      return (TResult)results;
    }

    public IEnumerator<TElement> GetEnumerator()
    {
      return ((IEnumerable<TElement>)_innerQueryable).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}