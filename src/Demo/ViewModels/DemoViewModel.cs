using System.Collections.Generic;
using Demo.Model;

namespace Demo.ViewModels
{
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