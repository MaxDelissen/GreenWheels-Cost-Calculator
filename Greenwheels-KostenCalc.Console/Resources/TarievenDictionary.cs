namespace Greenwheels_KostenCalc.Resources;

public static class TarievenDictionary
{
	public readonly static Dictionary<AutoType, Dictionary<AbonnementType, (double UurPrijs, double KmPrijs, double StartTarief, Dictionary<int, double> LangVerhuur)>> Tarieven = new()
	{
		{
			AutoType.BudgetAuto, new Dictionary<AbonnementType, (double, double, double, Dictionary<int, double>)>
			{
				{ AbonnementType.Soms, (3.51, 0.38, 1, null) },
				{ AbonnementType.Regelmatig, (2.88, 0.34, 0, new Dictionary<int, double> { { 1, 35.10 }, { 2, 58.50 }, { 7, 143.10 } }) },
				{ AbonnementType.Vaak, (2.07, 0.27, 0, new Dictionary<int, double> { { 1, 28.80 }, { 2, 49.50 }, { 7, 134.10 } }) }
			}
		},
		{
			AutoType.StadsAutoEnElektrisch, new Dictionary<AbonnementType, (double, double, double, Dictionary<int, double>)>
			{
				{ AbonnementType.Soms, (3.90, 0.42, 1, null) },
				{ AbonnementType.Regelmatig, (3.20, 0.38, 0, new Dictionary<int, double> { { 1, 39.00 }, { 2, 65.00 }, { 7, 159.00 } }) },
				{ AbonnementType.Vaak, (2.30, 0.30, 0, new Dictionary<int, double> { { 1, 32.00 }, { 2, 55.00 }, { 7, 149.00 } }) }
			}
		},
		{
			AutoType.StationWagonEnBusje, new Dictionary<AbonnementType, (double, double, double, Dictionary<int, double>)>
			{
				{ AbonnementType.Soms, (5.40, 0.47, 1, null) },
				{ AbonnementType.Regelmatig, (4.70, 0.43, 0, new Dictionary<int, double> { { 1, 49.00 }, { 2, 85.00 }, { 7, 189.00 } }) },
				{ AbonnementType.Vaak, (3.80, 0.47, 0, new Dictionary<int, double> { { 1, 42.00 }, { 2, 75.00 }, { 7, 179.00 } }) }
			}
		}
	};

	public readonly static Dictionary<AbonnementType, double> AbonnementKosten = new()
	{
		{ AbonnementType.Soms, 0 },
		{ AbonnementType.Regelmatig, 10 },
		{ AbonnementType.Vaak, 25 }
	};
}