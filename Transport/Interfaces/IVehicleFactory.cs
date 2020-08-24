﻿using System;
using System.Collections.Generic;
using System.Text;
using Transport.Abstractions;

namespace Transport
{
    public interface IVehicleFactory
    {
        /// <summary>
        /// Method that created any number of busses.
        /// </summary>
        /// <param name="number">Number of busses to create.</param>
        ABus[] CreateVehicles (Byte number);
    }
}