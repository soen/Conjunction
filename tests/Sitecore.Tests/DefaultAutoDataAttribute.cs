using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace Conjunction.Sitecore.Tests
{
  public class DefaultAutoDataAttribute : AutoDataAttribute
  {
    public DefaultAutoDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
		}
  }
}
