using Greenwheels_KostenCalc.Logic.Resources.Enums;

namespace Greenwheels_KostenCalc.Logic.Resources.DTO;

public class UserInputDto
{
	public double Hours { get; set; }
	public double Kilometers { get; set; }
	public SubscriptionEnum CurrentSubscription { get; set; }
}