namespace Greenwheels_KostenCalc.Resources;

public class Resultaten
{
	public Resultaten(List<(AutoType AutoType, AbonnementType Abonnement, double Kosten)> kosten)
	{
		Kosten = kosten;
	}
	public List<(AutoType AutoType, AbonnementType Abonnement, double Kosten)> Kosten { get; }

	public string GetCheapestOption()
	{
		var goedkoopste = Kosten.MinBy(r => r.Kosten);
		return $"De goedkoopste optie is: Auto: {goedkoopste.AutoType}, Abonnement: {goedkoopste.Abonnement}, Kosten: €{goedkoopste.Kosten:F2}";
	}
}