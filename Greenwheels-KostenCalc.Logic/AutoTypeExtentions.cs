using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Logic;

public static class AutoTypeExtensions
{
    public static string GetDescription(this CarType carType)
    {
        return carType switch
        {
            CarType.Budget => "VW Up! Extra budget",
            CarType.CityElectric => "VW Up!, E-Up! of ID.3",
            CarType.StationVan => "VW Golf Variant of Caddy",
            _ => throw new NotImplementedException() //Gives warning in Rider during commit, but this can be ignored
        };
    }
}