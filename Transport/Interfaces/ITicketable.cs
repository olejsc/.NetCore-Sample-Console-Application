using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    /// <summary>
    /// A interface for registering when a <see cref="ITicket"/> enters the vehicle.
    /// </summary>
    public interface ITicketable
    {
        /// <summary>
        /// Registers the entrance of whichever vehicle this ticket entered.
        /// </summary>
        /// <param name="ticket"></param>
        void RegisterEntrance (ITicket ticket);
    }
}