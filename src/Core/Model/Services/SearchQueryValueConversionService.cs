using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Conjunction.Core.Model.Services
{
	/// <summary>
	/// Provides functionalities for converting raw <see cref="string"/> values into typed values.
	/// </summary>
	public class SearchQueryValueConversionService : ISearchQueryValueConversionService
	{
		/// <summary>
		/// Converts the <paramref name="value"/> to the specified <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">The value type that the specified <paramref name="value"/> needs to be converted into.</param>
		/// <param name="value">The value that needs to be converted.</param>
		/// <returns></returns>
		public object ToTypedValue(Type valueType, string value)
		{
			if (valueType == null) throw new ArgumentNullException(nameof(valueType));

			object retVal = null;
			var type = valueType;

			if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				type = valueType.GenericTypeArguments[0];

			try
			{
				var typeConverter = TypeDescriptor.GetConverter(type);
				if (typeConverter.CanConvertFrom(value.GetType()))
					retVal = typeConverter.ConvertFromInvariantString(value);
			}
			catch
			{
				// If the value can't be converted into the given type, due to parsing issues, ignore it
			}

			return retVal;
		}

		/// <summary>
		/// Tries to convert the specified <paramref name="value"/> into range value parts.
		/// </summary>
		/// <remarks>
		/// The output parameter <paramref name="rangeValueParts"/> is intended to be used within
		/// the <see cref="RangeValue"/> type.
		/// </remarks>
		/// <param name="rangeValueParts">The value that needs to be converted.</param>
		/// <param name="rangeValueParts">The range value parts ressembling the range values, if converted.</param>
		/// <returns></returns>
		public bool TryConvertToRangeValueParts(string value, out Tuple<string, string> rangeValueParts)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			bool retVal;
			const string rangeValuePattern = @"^\[([^\.]+)(;|:)([^\.]+)\]$";

			var match = Regex.Match(value, rangeValuePattern);
			if (match.Success)
			{
				var lowerValue = match.Groups[1].Value;
				var upperValue = match.Groups[3].Value;

				// TODO: Implement this, when needed, as it could be used to determine how the range is used: ':' means both ends included, ';' means lower not included but upper is, etc.
				var inclusion = match.Groups[2].Value;

				rangeValueParts = new Tuple<string, string>(lowerValue, upperValue);
				retVal = true;
			}
			else
			{
				rangeValueParts = null;
				retVal = false;
			}
			return retVal;
		}
	}
}