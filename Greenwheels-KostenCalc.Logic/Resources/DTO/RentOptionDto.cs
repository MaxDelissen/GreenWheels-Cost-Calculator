using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Logic.Resources.DTO;

public class RentOptionDto(CarType carType, SubscriptionEnum subscriptionEnum, double cost)
{
	public CarType CarType { get; } = carType;
	public SubscriptionEnum SubscriptionEnum { get; } = subscriptionEnum;
	public double Cost { get; } = cost;
}