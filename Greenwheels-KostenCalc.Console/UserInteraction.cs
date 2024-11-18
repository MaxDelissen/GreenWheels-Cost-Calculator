using Greenwheels_KostenCalc.Resources;

namespace Greenwheels_KostenCalc;

public class UserInteraction
{
	public UserInputDto GetUserInput()
	{
		UserInputDto userInputDto = new();
		while (true)
		{
			Console.WriteLine("Voer het aantal uren in dat je de auto wilt huren:");
			if (int.TryParse(Console.ReadLine(), out int uren))
			{
				userInputDto.Uren = uren;
				break;
			}
			Console.WriteLine("Voer een geldig getal in.");
		}
		while (true)
		{
			Console.WriteLine("Voer het geschatte aantal kilometers in:");
			if (double.TryParse(Console.ReadLine(), out double kilometers))
			{
				userInputDto.Kilometers = kilometers;
				break;
			}
			Console.WriteLine("Voer een geldig getal in.");
		}
		while (true)
		{
			Console.WriteLine("Heb je al een abonnement? (Soms (1), Regelmatig (2), Vaak (3):");
			Console.WriteLine("Als je geen abonnement hebt (Soms), druk op Enter.");
			string? input = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(input))
			{
				userInputDto.HuidigAbonnement = AbonnementType.Soms;
				break;
			}
			if (Enum.TryParse(input.Trim(), out AbonnementType huidigAbonnement))
			{
				userInputDto.HuidigAbonnement = huidigAbonnement;
				break;
			}
			switch (input)
			{
				case "1":
					userInputDto.HuidigAbonnement = AbonnementType.Soms;
					break;
				case "2":
					userInputDto.HuidigAbonnement = AbonnementType.Regelmatig;
					break;
				case "3":
					userInputDto.HuidigAbonnement = AbonnementType.Vaak;
					break;
				default:
					Console.WriteLine("Voer een geldig getal in.");
					break;
			}
		}

		return userInputDto;
	}

	public void ShowResults(List<HuurOptie> resultaten)
	{
		Console.WriteLine("\nKostenoverzicht:");
		Console.WriteLine("Denk ook na over toekomstige huur, want dan kan een abonnement alsnog voordeliger zijn.");
		Console.WriteLine(new string('=', 25));

		// Group results by AutoType
		var groupedResults = resultaten.GroupBy(r => r.AutoType);

		foreach (var group in groupedResults)
		{
			// Print AutoType as header
			Console.WriteLine($"\n*** {group.Key.GetDescription()} ***");

			// Determine the cheapest option for this AutoType
			var cheapestOption = group.MinBy(r => r.Kosten);

			// Print a table of results for this AutoType
			Console.WriteLine($"{"Abonnement",-15} {"Kosten",-10}");
			Console.WriteLine(new string('-', 25));

			foreach (var resultaat in group)
			{
				// Highlight the cheapest option with an asterisk
				string highlight = resultaat == cheapestOption ? "➡️" : "  ";
				Console.WriteLine($"{highlight} {resultaat.AbonnementType,-15} €{resultaat.Kosten,8:F2}");
			}
		}

		Console.WriteLine(new string('=', 25));
	}


}