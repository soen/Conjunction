﻿namespace Conjunction.Foundation.Core.Model.Providers.Indexing
{
  /// <summary>
  /// Provides functionality to deliver the index name that will be used when performing search queries.
  /// </summary>
  public interface IIndexNameProvider
  {
    string IndexName { get; } 
  }
}