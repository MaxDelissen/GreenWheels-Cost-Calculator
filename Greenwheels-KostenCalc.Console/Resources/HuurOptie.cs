namespace Greenwheels_KostenCalc.Resources;

public class HuurOptie(AutoType autoType, AbonnementType abonnementType, double kosten)
{
	public AutoType AutoType { get; } = autoType;
	public AbonnementType AbonnementType { get; } = abonnementType;
	public double Kosten { get; } = kosten;
}