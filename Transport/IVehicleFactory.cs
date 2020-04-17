using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface IVehicleFactory
    {
        /// <summary>
        /// Method that created any number of busses.
        /// </summary>
        /// <param name="number">Number of busses to create.</param>
        Transport.ABus[] CreateVehicles (Byte number);
    }
}