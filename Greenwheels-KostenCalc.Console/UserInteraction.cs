

using Greenwheels_KostenCalc.Logic;
using Greenwheels_KostenCalc.Logic.Resources.DTO;
using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc;

public class UserInteraction
{
    public UserInputDto GetUserInput()
    {
        var userInputDto = new UserInputDto();

        userInputDto.Hours = GetValidInput<double>("Voer het aantal uren in dat je de auto wilt huren:", double.TryParse);
        userInputDto.Kilometers = GetValidInput<double>("Voer het geschatte aantal kilometers in:", double.TryParse);
        userInputDto.CurrentSubscription = GetSubscriptionType();

        return userInputDto;
    }

    private T GetValidInput<T>(string prompt, TryParseHandler<T> tryParse)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            //Replace dot with comma for decimal numbers
            var decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            input = input?.Replace(",", decimalSeparator).Replace(".", decimalSeparator);
            if (tryParse(input, out var result))
                return result;
            Console.WriteLine("Voer een geldig getal in.");
        }
    }

    private SubscriptionEnum GetSubscriptionType()
    {
        while (true)
        {
            Console.WriteLine("Heb je al een abonnement? (Soms (1), Regelmatig (2), Vaak (3):");
            Console.WriteLine("Als je geen abonnement hebt (Soms), druk op Enter.");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return SubscriptionEnum.Soms; //Default to 'Soms'

            //First try to parse the input as an Enum (For fully typed input)
            if (Enum.TryParse(input.Trim(), true, out SubscriptionEnum subscription))
                return subscription;

            switch (input)
            {
                case "1": return SubscriptionEnum.Soms;
                case "2": return SubscriptionEnum.Regelmatig;
                case "3": return SubscriptionEnum.Vaak;
                default: Console.WriteLine("Voer een geldig getal in."); break;
            }
        }
    }

    public void ShowResults(List<RentOptionDto> results)
    {
        Console.WriteLine("\nKostenoverzicht:");
        Console.WriteLine("Denk ook na over toekomstige huur, want dan kan een abonnement alsnog voordeliger zijn.");
        Console.WriteLine(new string('=', 25));

        var groupedResults = results.GroupBy(r => r.CarType);

        foreach (var group in groupedResults)
        {
            Console.WriteLine($"\n*** {group.Key.GetDescription()} ***");

            var cheapestOption = group.MinBy(r => r.Cost);

            Console.WriteLine($"{"Abonnement",-15} {"Kosten",-10}");
            Console.WriteLine(new string('-', 25));

            foreach (var result in group)
            {
                var highlight = result == cheapestOption ? "➡️" : "  ";
                Console.WriteLine($"{highlight} {result.SubscriptionEnum,-15} €{result.Cost,8:F2}");
            }
        }

        Console.WriteLine(new string('=', 25));
    }

    private delegate bool TryParseHandler<T>(string input, out T result);
}