using System;

namespace Conjunction.Core.Model.Services
{
		public interface ISearchQueryValueConversionService
		{
				object ToTypedValue(Type valueType, string value);
				bool TryConvertToRangeValueParts(string value, out Tuple<string, string> rangeValueParts);
		}
}