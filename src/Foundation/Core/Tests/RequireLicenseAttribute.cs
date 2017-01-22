using System;
using Xunit.Sdk;

namespace Conjunction.Foundation.Core.Tests
{
  // Adapted from: https://github.com/xunit/samples.xunit/tree/master/TraitExtensibility
  [TraitDiscoverer("Conjunction.Foundation.Core.Tests.RequireLicenseDiscoverer", "Conjunction.Foundation.Core.Tests")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  public class RequireLicenseAttribute : Attribute, ITraitAttribute
  {
  }
}