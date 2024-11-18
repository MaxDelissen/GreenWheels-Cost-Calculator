using Greenwheels_KostenCalc.Logic;
using Greenwheels_KostenCalc.Logic.Resources;
using Greenwheels_KostenCalc.Logic.Resources.DTO;

namespace Greenwheels_KostenCalc;

class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8; // Ensure € symbols are displayed correctly
		var rates = RatesDictionary.Rates;
		var subscriptionCosts = RatesDictionary.SubscriptionCosts;

		var userInteraction = new UserInteraction();
		UserInputDto userInput = userInteraction.GetUserInput();

		var costCalculator = new CostCalculator(rates, subscriptionCosts);
		var resultaten = costCalculator.CalculateCosts(userInput);

		userInteraction.ShowResults(resultaten);
		Console.ReadLine();
	}
}