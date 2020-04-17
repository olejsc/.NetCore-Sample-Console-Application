using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public class BusFactory : IVehicleFactory
    {
        /// <param name="number">Number of busses to create.</param>
        public Transport.ABus[] CreateVehicles (Byte number)
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