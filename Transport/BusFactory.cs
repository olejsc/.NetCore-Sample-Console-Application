using System;

namespace Transport
{
    public class BusFactory : IVehicleFactory
    {
        ///<summary> A factory method which creates a list of busses.></summary>
        /// <param name="number">Number of busses to create.</param>
        public ABus[] CreateVehicles (Byte number)
        {
            ABus[] busses = new ABus[number];
            for (Byte i = 0; i < number; i++)
            {
                DieselBus dieselBus = new DieselBus();
                busses[i] = dieselBus;
            }
            return busses;
        }
    }
}