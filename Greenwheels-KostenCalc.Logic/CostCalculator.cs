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
					cost = Math.Min(cost, CalculateLongTripBenefit(subscription.Value.LongTripBenefit, userInput.Hours, subscription.Value.HourlyRate));
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

	private double CalculateLongTripBenefit(Dictionary<int, double> longTripBenefit, double hours, double normalHourlyRate)
	{
		int days = (int)hours / 24;
		double extraHours = hours % 24;

		double dayCosts = days > 0 ? longTripBenefit.GetValueOrDefault(days, double.MaxValue) : 0;
		double extraCosts = extraHours * normalHourlyRate;

		return dayCosts + extraCosts;
	}
}