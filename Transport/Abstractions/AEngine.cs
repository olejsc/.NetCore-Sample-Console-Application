using System;
using System.Threading;

namespace Transport
{
    public abstract class AEngine : IEngineController
    {
        private readonly CancellationToken _cancellationToken;
        /// <summary>
        /// The horsepower of the engine.
        /// </summary>
        protected ushort _horsePower;
        protected byte _speed;
        protected float _fuelConsumption;
        protected ushort _fuel;
        protected ushort _fuelCapacity;

        /// <summary>
        /// The horsepower of the engine.
        /// </summary>
        public abstract ushort HorsePower { get; protected set; }

        public abstract byte Speed { get; set; }
        public abstract float FuelConsumption { get; protected set; }
        public abstract ushort FuelCapacity { get; protected set; }
        public abstract ushort Fuel { get; set; }


        public abstract event EventHandler<EngineEventArgs> FuelEmpty;
        public abstract event EventHandler<EngineEventArgs> FuelFull;

        public abstract void Refuel (int amount);
        public abstract void Run (object time);
        public abstract void Start ();
        public abstract void Stop ();

        protected abstract void CalculateFuelConsumption ();
    }
}