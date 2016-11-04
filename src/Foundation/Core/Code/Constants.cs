using Sitecore.Data;

namespace Conjunction.Foundation.Core
{
  internal class Constants
  {
    public struct SearchOptions
    {
      public const string DefaultSearchPath = "/sitecore/content";
    }

    public struct Templates
    {
      public struct SearchQueryRoot
      {
        public static ID TemplateId = new ID("{818A0CA7-1388-48BC-9E07-851314C288CB}");
      }

      public struct _SearchQueryGrouping
      {
        public static ID TemplateId = new ID("{B016F73B-11CF-4E43-AEAC-1A52D90C4346}"); 
      }

      public struct _SearchQueryRule
      {
        public static ID TemplateId = new ID("{1FD87DBC-396F-4DBE-8933-234FF8FA45B6}"); 
      }
    }

    public struct Fields
    {
      public struct _IndexableEntityConfigurator
      {
        public static ID ConfiguredIndexableEntityType = new ID("{BEE28558-0A5D-4EE1-B54F-A9D25596A249}");
      }

      public struct _SearchQueryGrouping
      {
        public static ID SearchQueryGroupingLogicalOperator = new ID("{F024EAF0-F10D-4AC9-A7DA-A280B0AE1AF9}");
      }

      public struct _SearchQueryRule
      {
        public static ID AssociatedPropertyName = new ID("{B31481A1-8140-4E1A-B489-3DD21634C22D}");
        public static ID ComparisonOperator = new ID("{20E7EFE4-7460-4CBE-9352-7940FC5013D8}");
        public static ID DynamicValueProvidingParameter = new ID("{46660B55-18C3-4D3B-8F6E-7F3CB3949318}");
        public static ID DefaultValue = new ID("{BD5AABFB-A3DD-425B-8084-AEAA981FD3EE}");
      }
    }
  }
}