using System;
using Xunit.Sdk;

namespace Conjunction.Sitecore.Tests
{
  // Adapted from: https://github.com/xunit/samples.xunit/tree/master/TraitExtensibility
  [TraitDiscoverer("Conjunction.Sitecore.Tests.RequireLicenseDiscoverer", "Conjunction.Sitecore.Tests")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  public class RequireLicenseAttribute : Attribute, ITraitAttribute
  {
  }
}