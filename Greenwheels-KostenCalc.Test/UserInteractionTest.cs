using Greenwheels_KostenCalc.Logic.Resources.DTO;
using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Test;

public class UserInteractionTest
{
	[Fact]
	public void GetUserInput_ReturnsValidUserInputDto()
	{
		// Arrange
		var userInteraction = new UserInteraction();
		var input = new StringReader("2,5\n100\n1\n");
		Console.SetIn(input);

		// Act
		var result = userInteraction.GetUserInput();

		// Assert
		Assert.Equal(2.5, result.Hours);
		Assert.Equal(100, result.Kilometers);
		Assert.Equal(SubscriptionEnum.Soms, result.CurrentSubscription);
	}

	[Fact]
	public void GetUserInput_InvalidHoursInput_ShowsErrorMessage()
	{
		// Arrange
		var userInteraction = new UserInteraction();
		var input = new StringReader("invalid\n2,5\n100\n1\n");
		Console.SetIn(input);
		var output = new StringWriter();
		Console.SetOut(output);

		// Act
		userInteraction.GetUserInput();

		// Assert
		var consoleOutput = output.ToString();
		Assert.Contains("Voer een geldig getal in.", consoleOutput);
	}

	[Fact]
	public void GetUserInput_InvalidKilometersInput_ShowsErrorMessage()
	{
		// Arrange
		var userInteraction = new UserInteraction();
		var input = new StringReader("2,5\ninvalid\n100\n1\n");
		Console.SetIn(input);
		var output = new StringWriter();
		Console.SetOut(output);

		// Act
		var result = userInteraction.GetUserInput();

		// Assert
		var consoleOutput = output.ToString();
		Assert.Equal(2.5, result.Hours);
		Assert.Contains("Voer een geldig getal in.", consoleOutput);
	}

	[Fact]
	public void GetUserInput_InvalidSubscriptionInput_ShowsErrorMessage()
	{
		// Arrange
		var userInteraction = new UserInteraction();
		var input = new StringReader("2,5\n100\ninvalid\n1\n");
		Console.SetIn(input);
		var output = new StringWriter();
		Console.SetOut(output);

		// Act
		userInteraction.GetUserInput();

		// Assert
		var consoleOutput = output.ToString();
		Assert.Contains("Voer een geldig getal in.", consoleOutput);
	}

	[Fact]
	public void ShowResults_DisplaysCorrectResults()
	{
		// Arrange
		var userInteraction = new UserInteraction();
		var results = new List<RentOptionDto>
		{
			new(CarType.Budget, SubscriptionEnum.Soms, 10 ),
			new(CarType.Budget, SubscriptionEnum.Regelmatig, 8),
			new(CarType.StationVan, SubscriptionEnum.Vaak, 15 )
		};
		var output = new StringWriter();
		Console.SetOut(output);

		// Act
		userInteraction.ShowResults(results);

		// Assert
		var consoleOutput = output.ToString();
		Assert.Contains("Kostenoverzicht:", consoleOutput);
		Assert.Contains("*** VW Up! Extra budget ***", consoleOutput);
		Assert.Contains("*** VW Golf Variant of Caddy ***", consoleOutput);
		Assert.DoesNotContain("*** VW Up!, E-Up! of ID.3 ***", consoleOutput);
	}
}