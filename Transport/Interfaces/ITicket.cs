using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    /// <summary>
    /// A interface for registering entrance to a vehicle.
    /// </summary>
    public interface ITicket
    {
        void RegisterEntrance (ITicketable bus);
    }
}