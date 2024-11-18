using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Logic.Resources;

public static class RatesDictionary
{
	public readonly static Dictionary<CarType, Dictionary<SubscriptionEnum, (double HourlyRate, double KmRate, double StartingRate, Dictionary<int, double> LongTripBenefit)>> Rates = new()
	{
		{
			CarType.Budget, new Dictionary<SubscriptionEnum, (double, double, double, Dictionary<int, double>)>
			{
				{ SubscriptionEnum.Soms, (3.51, 0.38, 1, null) },
				{ SubscriptionEnum.Regelmatig, (2.88, 0.34, 0, new Dictionary<int, double> { { 1, 35.10 }, { 2, 58.50 }, { 7, 143.10 } }) },
				{ SubscriptionEnum.Vaak, (2.07, 0.27, 0, new Dictionary<int, double> { { 1, 28.80 }, { 2, 49.50 }, { 7, 134.10 } }) }
			}
		},
		{
			CarType.CityElectric, new Dictionary<SubscriptionEnum, (double, double, double, Dictionary<int, double>)>
			{
				{ SubscriptionEnum.Soms, (3.90, 0.42, 1, null) },
				{ SubscriptionEnum.Regelmatig, (3.20, 0.38, 0, new Dictionary<int, double> { { 1, 39.00 }, { 2, 65.00 }, { 7, 159.00 } }) },
				{ SubscriptionEnum.Vaak, (2.30, 0.30, 0, new Dictionary<int, double> { { 1, 32.00 }, { 2, 55.00 }, { 7, 149.00 } }) }
			}
		},
		{
			CarType.StationVan, new Dictionary<SubscriptionEnum, (double, double, double, Dictionary<int, double>)>
			{
				{ SubscriptionEnum.Soms, (5.40, 0.47, 1, null) },
				{ SubscriptionEnum.Regelmatig, (4.70, 0.43, 0, new Dictionary<int, double> { { 1, 49.00 }, { 2, 85.00 }, { 7, 189.00 } }) },
				{ SubscriptionEnum.Vaak, (3.80, 0.47, 0, new Dictionary<int, double> { { 1, 42.00 }, { 2, 75.00 }, { 7, 179.00 } }) }
			}
		}
	};

	public readonly static Dictionary<SubscriptionEnum, double> SubscriptionCosts = new()
	{
		{ SubscriptionEnum.Soms, 0 },
		{ SubscriptionEnum.Regelmatig, 10 },
		{ SubscriptionEnum.Vaak, 25 }
	};
}