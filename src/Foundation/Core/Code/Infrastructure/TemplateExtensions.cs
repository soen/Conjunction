using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Templates;

namespace Conjunction.Foundation.Core.Infrastructure
{
  /// <summary>
  /// Provides extension functionalities for working with Sitecore <see cref="Template"/> types.
  /// </summary>
  public static class TemplateExtensions
  {
    public static bool IsDerived([NotNull] this Template template, [NotNull] ID templateId)
    {
      return template.ID == templateId ||
             template.GetBaseTemplates().Any(baseTemplate => IsDerived(baseTemplate, templateId));
    }
  }
}