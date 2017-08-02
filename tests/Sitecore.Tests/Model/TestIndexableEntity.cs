using System;
using Conjunction.Sitecore.Model;

namespace Conjunction.Sitecore.Tests.Model
{
  public class TestIndexableEntity : IndexableEntity
  {
    public int SomeInteger { get; set; }

    public long SomeLong { get; set; }

    public float SomeFloat { get; set; }

    public double SomeDouble { get; set; }

    public bool SomeBoolean { get; set; }

    public Guid SomeGuid { get; set; }
  }
}