using Greenwheels_KostenCalc.Logic.Resources.DTO;
using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Logic;

public class CostCalculator
{
	private readonly Dictionary
	<CarType,
	Dictionary
		<SubscriptionEnum,
		(
			double HourlyRate,
			double KmRate,
			double StartingRate,
			Dictionary<int, double> LongTripBenefit
		)>
	> _rates;
	private readonly Dictionary<SubscriptionEnum, double> _subscriptionCosts;
	public CostCalculator(Dictionary<CarType, Dictionary<SubscriptionEnum, (double HourlyRate, double KmRate, double StartingRate, Dictionary<int, double> LongTripBenefit)>> rates, Dictionary<SubscriptionEnum, double> subscriptionCosts)
	{
		_rates = rates;
		_subscriptionCosts = subscriptionCosts;
	}

	// Calculate the costs based on user input and return a list of rent options
	public List<RentOptionDto> CalculateCosts(UserInputDto userInput)
	{
		var results = new List<RentOptionDto>();
		foreach (var carType in _rates)
		{
			foreach (var subscription in carType.Value)
			{
				double cost = CalculateCost(subscription.Value, userInput.Hours, userInput.Kilometers);
				if (subscription.Value.LongTripBenefit != null)
				{
					cost = Math.Min(cost, CalculateLongTripBenefit(subscription.Value, userInput.Hours, userInput.Kilometers));
				}

				if (subscription.Key != userInput.CurrentSubscription)
				{
					cost += _subscriptionCosts[subscription.Key];
				}

				results.Add(new RentOptionDto(carType.Key, subscription.Key, cost));
			}
		}

		return results;
	}

	private double CalculateCost((double HourlyRate, double KmRate, double StartingRate, Dictionary<int, double> LongTripBenefit) rate, double hours, double kilometers) =>
		rate.HourlyRate * hours + rate.KmRate * kilometers + rate.StartingRate;

	private double CalculateLongTripBenefit((double HourlyRate, double KmRate, double StartingRate, Dictionary<int, double> LongTripBenefit) rate, double hours, double kilometers)
	{
		// Convert hours to full days and extra hours
		int days = (int)(hours / 24); // Full days
		double extraHours = hours % 24; // Extra hours that don't form a full day

		double totalCost = 0;

		// Apply long trip benefits for complete days
		if (days > 0)
		{
			// For each complete day, apply the appropriate long trip benefit if available
			foreach (var entry in rate.LongTripBenefit.OrderByDescending(x => x.Key)) // Order by largest day count first
			{
				if (days >= entry.Key)
				{
					totalCost += entry.Value; // Apply the benefit for this section of days
					days -= entry.Key; // Reduce the number of remaining days
					break;
				}
			}
		}

		// For the remaining days (if any), calculate the cost for those
		totalCost += days * 24 * rate.HourlyRate; // For leftover days, calculate cost without benefit

		// Calculate the cost for extra hours (those that don't form a full day)
		totalCost += extraHours * rate.HourlyRate;

		// Calculate the cost for the kilometers
		totalCost += kilometers * rate.KmRate;

		return totalCost;
	}
}