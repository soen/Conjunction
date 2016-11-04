using System.Collections.Generic;
using Conjunction.Foundation.Core.Model;
using Sitecore.Data;

namespace Demo.Model
{
  public class MyClass : IndexableEntity
  {
    public IEnumerable<ID> TemplateIds { get; set; }

    public int Size { get; set; }

    public bool HasSize { get; set; }
  }
}