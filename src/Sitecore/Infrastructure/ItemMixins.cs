using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace Conjunction.Sitecore.Infrastructure
{
  /// <summary>
  /// Provides extension functionalities for working with Sitecore <see cref="Item"/> types.
  /// </summary>
  internal static class ItemMixins
  {
    public static bool IsDerived([NotNull] this Item item, [NotNull] ID templateId)
    {
      return TemplateManager.GetTemplate(item).IsDerived(templateId);
    }
  }
}