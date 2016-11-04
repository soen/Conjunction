using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Templates;

namespace Conjunction.Foundation.Core.Infrastructure
{
  public static class TemplateExtensions
  {
    public static bool IsDerived([NotNull] this Template template, [NotNull] ID templateId)
    {
      return template.ID == templateId ||
             template.GetBaseTemplates().Any(baseTemplate => IsDerived(baseTemplate, templateId));
    }
  }
}