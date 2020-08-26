namespace Transport.Abstractions
{
    public class DrivingAbility
    {
        private int attention = 100;
        private int experience = 0;

        public int attentionDegredationPerHour = -10;

        public int workDayLength = 3120; // 8 hours of work in minutes
        public int timeDrivenToday = 0; // in minutes
        public bool CheckStopButtonChance (int energy)
        {
            return (attention * energy / 100 + experience) > 0.5f;
        }
    }
}
