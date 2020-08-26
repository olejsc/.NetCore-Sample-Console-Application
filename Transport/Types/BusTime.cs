namespace Transport
{
    public class BusTime
    {
        /// <summary>
        /// The duration of the time.
        /// </summary>
        private readonly ushort _duration;
        /// <summary>
        /// How much time that pass each step.
        /// </summary>
        private readonly ushort _timestep;

        public BusTime (ushort duration, ushort timestep)
        {
            _duration = duration;
            _timestep = timestep;
        }

        public ushort Duration => _duration;

        public ushort Timestep => _timestep;
    }
}