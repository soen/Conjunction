using System;
using Conjunction.Core.Model;

namespace Conjunction.Core.Tests.Model
{
  public class TestIndexableEntity : IIndexableEntity
  {
		public string SomeString { get; set; }

    public int SomeInteger { get; set; }

    public long SomeLong { get; set; }

    public float SomeFloat { get; set; }

    public double SomeDouble { get; set; }

    public bool SomeBoolean { get; set; }

		public DateTime SomeDateTime { get; set; }

    public Guid SomeGuid { get; set; }
  }
}