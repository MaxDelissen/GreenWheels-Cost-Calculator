namespace Greenwheels_KostenCalc.Resources;

public static class AutoTypeExtensions
{
    public static string GetDescription(this AutoType autoType)
    {
        return autoType switch
        {
            AutoType.BudgetAuto => "VW Up! Extra budget",
            AutoType.StadsAutoEnElektrisch => "VW Up!, E-Up! of ID.3",
            AutoType.StationWagonEnBusje => "VW Golf Variant of Caddy",
            _ => throw new NotImplementedException() //Gives warning in Rider during commit, but this can be ignored
        };
    }
}