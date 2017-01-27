using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Conjunction.Foundation.Core.Tests
{
  // Adapted from: https://github.com/xunit/samples.xunit/tree/master/TraitExtensibility
  public class RequireLicenseDiscoverer : ITraitDiscoverer
  {
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
      yield return new KeyValuePair<string, string>("Category", "RequireLicense");
    }
  }
}
