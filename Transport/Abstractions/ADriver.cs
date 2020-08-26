using Transport.Abstractions;

namespace Transport
{
    public class ADriver
    {
        public int Age { get; }
        public int FirstName { get; }
        public int LastName { get; }

        public int Energy { set; get; }

        public readonly int EnergyConsumptionPerHour = 15;

        public DrivingAbility GetDrivingAbility { get;}

    }
}