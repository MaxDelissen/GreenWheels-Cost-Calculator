using Greenwheels_KostenCalc.Resources;

namespace Greenwheels_KostenCalc;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Zorg ervoor dat de €-tekens goed worden weergegeven
        // Tarieven en abonnementskosten
        var tarieven = TarievenDictionary.Tarieven;
        var abonnementKosten = TarievenDictionary.AbonnementKosten;

        var userInteraction = new UserInteraction();
        UserInputDto userInput = userInteraction.GetUserInput();

        // Resultaten berekenen
        var resultaten = new List<HuurOptie>();
        foreach (var autoType in tarieven) //Ga langs alle 3 autotypes
        {
            foreach (var abonnement in autoType.Value) //Ga langs alle 3 abonnementen
            {
                double kosten = BerekenKosten(abonnement.Value, userInput.Uren, userInput.Kilometers);
                if (abonnement.Value.LangVerhuur != null)
                {
                    kosten = Math.Min(kosten, BerekenLangVerhuur(abonnement.Value.LangVerhuur, userInput.Uren, abonnement.Value.UurPrijs));
                }

                if (abonnement.Key != userInput.HuidigAbonnement)
                {
                    kosten += abonnementKosten[abonnement.Key];
                }

                resultaten.Add(new(autoType.Key, abonnement.Key, kosten));
            }
        }

        // Resultaten tonen
        userInteraction.ShowResults(resultaten);

        Console.ReadLine();
    }

    static double BerekenKosten((double UurPrijs, double KmPrijs, double StartTarief, Dictionary<int, double> LangVerhuur) tarief, int uren, double kilometers)
    {
        return (tarief.UurPrijs * uren) + (tarief.KmPrijs * kilometers) + tarief.StartTarief;
    }

    static double BerekenLangVerhuur(Dictionary<int, double> langVerhuur, int uren, double normaalUurTarief)
    {
        int dagen = uren / 24;
        int extraUren = uren % 24;

        // Kosten voor de volle dagen
        double dagKosten = 0;
        if (dagen > 0)
        {
            dagKosten += langVerhuur.ContainsKey(dagen) ? langVerhuur[dagen] : double.MaxValue;
        }

        double extraKosten = extraUren * normaalUurTarief;

        return dagKosten + extraKosten;
    }
}