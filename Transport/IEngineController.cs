using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface IEngineController
    {
        /// <summary>
        /// Fuel is empty.
        /// </summary>
        event System.EventHandler<EngineEventArgs>FuelEmpty;

        /// <summary>
        /// Fuel is full.
        /// </summary>
        event System.EventHandler<EngineEventArgs>FuelFull;
        /// <summary>
        /// Current speed.
        /// </summary>
        Byte Speed { get; set; }
        float FuelConsumption { get; }
        /// <summary>
        /// Maximum fuel capacity.
        /// </summary>
        UInt16 FuelCapacity { get; }
        /// <summary>
        /// Current Fuel.
        /// </summary>
        ushort Fuel { get; set; }

        void Start ();
        /// <summary>
        /// The main method for the engine to call when it's active.
        /// </summary>
        /// <remarks>Consumes fuel each timestep.</remarks>
        /// <param name="time">The time type which we can extract duration and timestep from.</param>
        void Run (object time);
        void Stop ();
        void Refuel (int amount);
    }
}