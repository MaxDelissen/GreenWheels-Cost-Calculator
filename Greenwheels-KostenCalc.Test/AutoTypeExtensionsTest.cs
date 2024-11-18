using Greenwheels_KostenCalc.Logic;
using Greenwheels_KostenCalc.Logic.Resources.Enums;
using JetBrains.Annotations;

namespace Greenwheels_KostenCalc.Test;

[TestSubject(typeof(AutoTypeExtensions))]
public class AutoTypeExtensionsTest
{

	[Fact]
	public void GetDescription_ReturnsCorrectDescription()
	{
		// Arrange
		var carType = CarType.Budget;

		// Act
		var result = carType.GetDescription();

		// Assert
		Assert.Equal("VW Up! Extra budget", result);
	}

	[Fact]
	public void GetDescription_ReturnsCorrectDescription2()
	{
		// Arrange
		var carType = CarType.CityElectric;

		// Act
		var result = carType.GetDescription();

		// Assert
		Assert.Equal("VW Up!, E-Up! of ID.3", result);
	}

	[Fact]
	public void GetDescription_ReturnsCorrectDescription3()
	{
		// Arrange
		var carType = CarType.StationVan;

		// Act
		var result = carType.GetDescription();

		// Assert
		Assert.Equal("VW Golf Variant of Caddy", result);
	}

	[Fact]
	public void GetDescription_ThrowsExceptionWhenInvalidCarType()
	{
		// Arrange
		var carType = (CarType) 3; //Cast to invalid value

		// Assert
		Assert.Throws<NotImplementedException>(Act);

		// Act
		void Act() => carType.GetDescription();
	}
}