using System.Collections.Generic;
using Demo.Model;

namespace Demo.ViewModels
{
  /// <summary>
  /// A simple ViewModel that contains information about the total number of 
  /// search result hits found, as well as the hits.
  /// </summary>
  public class DemoViewModel
  {
    public DemoViewModel(int totalCount, IEnumerable<MyClass> hits)
    {
      TotalCount = totalCount;
      Hits = hits;
    }

    public int TotalCount { get; set; }

    public IEnumerable<MyClass> Hits { get; set; }
  }
}