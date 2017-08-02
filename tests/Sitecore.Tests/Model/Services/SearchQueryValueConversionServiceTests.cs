using System;
using Conjunction.Sitecore.Model.Services;
using FluentAssertions;
using Sitecore.Data;
using Xunit;

namespace Conjunction.Sitecore.Tests.Model.Services
{
	public class SearchQueryValueConversionServiceTests
	{
		[Fact]
		public void ToTypedValue_ValueCanBeConvertedIntoSitecoreIDValueType_ReturnsTypedSitecoreIDValue()
		{
			// Arrange
			Type valueType = typeof(ID);
			string rawValue = "{818A0CA7-1388-48BC-9E07-851314C288CB}";
			ID expected = new ID(rawValue);

			var sut = new SearchQueryValueConversionService();

			// Act
			var actual = sut.ToTypedValue(valueType, rawValue);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}